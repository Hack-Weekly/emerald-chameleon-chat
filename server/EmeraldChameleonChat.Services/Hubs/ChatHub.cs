using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.Model.Entity;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using EmeraldChameleonChat.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using EmeraldChameleonChat.Services.Hubs;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EmeraldChameleonChat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IChatRoomMessageRepository _chatMessageRepository;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private static readonly ConcurrentDictionary<string, HubUserSession> _users = new ConcurrentDictionary<string, HubUserSession>();

        public ChatHub(ILogger<ChatHub> logger, IChatRoomMessageRepository context, IMapper mapper, IUserRepository userRepository, IChatRoomRepository chatRoomRepository)
        {
            _logger = logger;
            _chatMessageRepository = context;
            _chatRoomRepository = chatRoomRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public override async Task<Task> OnConnectedAsync()
        {
            //Get connected user details
            var userId = Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            User currentUser = await _userRepository.GetUserById(userId);
            var userName = currentUser.Username;
            //store in dictionary item
            var hubUser = new HubUserSession()
            {
                UserId= Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                UserName = _userRepository.GetUserById(userId).Result.Username,
                ConnectedAt = DateTime.UtcNow
            };
            _users.TryAdd(Context.ConnectionId, hubUser);

            await JoinRoom("Dumping Grounds");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            HubUserSession user;
            if (_users.TryGetValue(Context.ConnectionId, out user))
            {
                if (user.Roomname != "") 
                {
                    Clients.Group(user.Roomname).SendAsync("ReceiveMessage", user.UserName, " left the chatroom " + user.Roomname).ConfigureAwait(true);
                } 
            }
            _users.TryRemove(Context.ConnectionId, out _);
            return base.OnDisconnectedAsync(exception);
        }

        //Get Chat history
        public async Task GetMessageHistory(string roomName)
        {
            List<ChatRoomMessage> chatHistory = await _chatMessageRepository.GetChatHistory(roomName);
            foreach (var message in chatHistory)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", message.User.Name, message.MessageBody);
            }
        }

        //Send Message in chatroom
        public async Task SendMessage(string message)
        {
            //Get connected user details and send joined chat message
            HubUserSession? user = _users.TryGetValue(Context.ConnectionId, out user) ? user : null; ;

            //Send message
            await Clients.Group(user.Roomname).SendAsync("ReceiveMessage", user.UserName, message).ConfigureAwait(false);

            //Store sent message in db
            CancellationToken token = new();
            var result = new ChatRoomMessage(default, user.RoomId, user.UserId, message, DateTime.UtcNow);
            await _chatMessageRepository.CreateAsync(result, token, true);
        }

        //Get all active chatrooms
        public async Task GetActiveChatRooms()
        {
            var response = _chatMessageRepository.GetActiveGroups();
            await response.ConfigureAwait(true);
        }

        //Join new chatroom
        public async Task JoinRoom(string roomName)
        {
            //Get connected user details and send joined chat message
            HubUserSession user;
            if (_users.TryGetValue(Context.ConnectionId, out user))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                user.Roomname = roomName;
                user.RoomId = Guid.Parse(_chatMessageRepository.GetChatRoomId(roomName).Result.ToString());
                _users[Context.ConnectionId] = user;
                await Clients.Group(roomName).SendAsync("ReceiveMessage", user.UserName, " joined the chatroom " + roomName).ConfigureAwait(true);
            }
        }

        //Leave chatroom
        public async Task LeaveRoom(string roomName)
        {
            //Get connected user details and send left chat message
            HubUserSession user;
            if (_users.TryGetValue(Context.ConnectionId, out user))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
                user.Roomname = "";
                user.RoomId = Guid.Empty;
                _users[Context.ConnectionId] = user;
                await Clients.Group(roomName).SendAsync("ReceiveMessage", user.UserName, " left the chatroom " + roomName).ConfigureAwait(true);
            }
        }

        //Create new chatroom
        public async Task CreateChatroom(string roomName, string roomDescription) 
        {
            //Get connected user details
            HubUserSession user;
            if (_users.TryGetValue(Context.ConnectionId, out user))
            {
                CancellationToken token = new();
                var result = new ChatRoom(roomName, roomDescription, user.UserId);
                await _chatRoomRepository.CreateAsync(result, token, true);
            }
        }

        public Task BroadcastMessage(string name, string message) =>
            Clients.All.SendAsync("broadcastMessage", name, message);

        public Task Echo(string name, string message) =>
            Clients.Client(Context.ConnectionId)
                   .SendAsync("echo", name, $"{message} (echo from server)");
    }
}

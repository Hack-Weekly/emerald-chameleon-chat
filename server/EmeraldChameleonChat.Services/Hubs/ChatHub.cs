using EmeraldChameleonChat.Services.AutoMapperProfiles;
using EmeraldChameleonChat.Services.DAL.DbContexts;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.Model.Entity;
using EmeraldChameleonChat.Services.Model.DTO;
using Microsoft.AspNetCore.SignalR;
using EmeraldChameleonChat.Services.DAL.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using EmeraldChameleonChat.Services.Services.Users;
using System.Security.Claims;
using EmeraldChameleonChat.Services.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using EmeraldChameleonChat.Services.Hubs;
using System.Linq;
using EmeraldChameleonChat.Services.Model.Entity.Users;

namespace EmeraldChameleonChat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IChatRoomMessageRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private static Dictionary<Guid, UserData> _users = new Dictionary<Guid, UserData>();

        public ChatHub(ILogger<ChatHub> logger, IChatRoomMessageRepository context, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _chatRepository = context;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public override async Task<Task> OnConnectedAsync()
        {
            //Get connected user details
            Guid userId = Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = new UserData()
            {
                ConnectedAt = DateTime.Now,
                ConnectionId = Context.ConnectionId,
                User = await _userRepository.GetUserById(userId)
            };
            _users.Add(userId, user);

            //Get Chat history
            List<ChatRoomMessage> chatHistory = await _chatRepository.GetChatHistory("DevRoom");
            foreach (var message in chatHistory)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", message.User.Name, message.MessageBody);
            }

            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            //Need to move this somewhere to handle messaging when user joins chat
            //var room = "DevChat";
            //await JoinRoom(room).ConfigureAwait(false);
            //await Clients.Group(room).SendAsync("ReceiveMessage", user, " joined the chatroom " + room).ConfigureAwait(true);

            Guid ChatroomID = Guid.Parse("8fabe1f0-c77a-11ed-81cd-0242ac120002");
            Guid userId = _users.Keys.FirstOrDefault();
            string userName = _users.FirstOrDefault().Value.User.Username;
            CancellationToken token = new();


            await Clients.All.SendAsync("ReceiveMessage", userName, message).ConfigureAwait(true);
            var result = new ChatRoomMessage(default, ChatroomID, userId, message, DateTime.UtcNow);
            await _chatRepository.CreateAsync(result, token, true);
        }
        public async Task GetActiveChatRooms()
        {
            var response = _chatRepository.GetActiveGroups();
            await response.ConfigureAwait(true);
        }

        public Task JoinRoom(string roomName)
        {
            //await JoinRoom(room).ConfigureAwait(false);
            var userName = _users.FirstOrDefault().Value.User.Username;
            Clients.Group(roomName).SendAsync("ReceiveMessage",userName , " joined the chatroom " + roomName).ConfigureAwait(true);
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}

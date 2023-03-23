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
using System.Linq;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using Microsoft.EntityFrameworkCore;
using EmeraldChameleonChat.Services.Model.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

            //await JoinRoom("Dumping Grounds");

            return base.OnConnectedAsync();
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
        public async Task SendMessage(string roomName, string message)
        {
            var userId = Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            User currentUser = await _userRepository.GetUserById(userId);
            var userName = currentUser.Username;
            var chatroomID = Guid.Parse(_chatMessageRepository.GetChatRoomId(roomName).Result.ToString());

            CancellationToken token = new();
            await Clients.Group(roomName).SendAsync("ReceiveMessage", userName, message).ConfigureAwait(false);
            var result = new ChatRoomMessage(default, chatroomID, userId, message, DateTime.UtcNow);
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
            //Get connected user details
            var userId = Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            User currentUser = await _userRepository.GetUserById(userId);
            var userName = currentUser.Username;

            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveMessage",userName , " joined the chatroom " + roomName).ConfigureAwait(true);
        }

        //Leave chatroom
        public async Task LeaveRoom(string roomName)
        {
            //Get connected user details
            var userId = Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            User currentUser = await _userRepository.GetUserById(userId);
            var userName = currentUser.Username;

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", userName, " joined the chatroom " + roomName).ConfigureAwait(true);
        }

        //Create new chatroom
        public async Task CreateChatroom(string roomName, string roomDescription) 
        {
            //Get connected user details
            var userId = Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            User currentUser = await _userRepository.GetUserById(userId);
            var userName = currentUser.Username;

            CancellationToken token = new();
            var result = new ChatRoom(roomName,roomDescription,userId);
            await _chatRoomRepository.CreateAsync(result, token, true);
        }

        public Task BroadcastMessage(string name, string message) =>
            Clients.All.SendAsync("broadcastMessage", name, message);

        public Task Echo(string name, string message) =>
            Clients.Client(Context.ConnectionId)
                   .SendAsync("echo", name, $"{message} (echo from server)");
    }
}

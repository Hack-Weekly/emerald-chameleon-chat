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
        private readonly IChatRoomMessageRepository _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private static Dictionary<Guid, UserData> _users = new Dictionary<Guid, UserData>();

        public ChatHub(ILogger<ChatHub> logger, IChatRoomMessageRepository context, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task JoinChatRoom(string chatRoomName)
        {
            await this.Groups.AddToGroupAsync(_users.FirstOrDefault().Value.ConnectionId,chatRoomName,default).ConfigureAwait(false);

            //Dictionary<string, string> messages = await this.DatabaseManager.GetChatHistory(chatRoomName).ConfigureAwait(false);

            //await this.Clients.Group(chatRoomName).BroadcastMessageAsync(messages); 
        }

        public override async Task<Task> OnConnectedAsync()
        {
            Guid userId = Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var user = new UserData()
            {
                ConnectedAt = DateTime.Now,
                ConnectionId = Context.ConnectionId,
                User = await _userRepository.GetUserById(userId)
            };

            List<ChatRoomMessage> chatHistory = await _context.GetChatHistory("DevRoom");
            //Dictionary<string, string> messages = new Dictionary<string, string>();
            foreach (var message in chatHistory)
            {
                
                //messages.Add(message.Id.ToString(), message.MessageBody);
                await Clients.Caller.SendAsync("ReceiveMessage", message.User.Name, message.MessageBody);
            }
            //await Clients.Caller.SendAsync("ReceiveMessage", messages.Keys, messages.Values);

            _users.Add(userId, user);

            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string message)
        {
            //await JoinRoom(room).ConfigureAwait(false);
            //await Clients.Group(room).SendAsync("ReceiveMessage", user, " joined the chatroom " + room).ConfigureAwait(true);

            Guid ChatroomID = Guid.Parse("8fabe1f0-c77a-11ed-81cd-0242ac120002");
            Guid userId = _users.Keys.FirstOrDefault();
            string userName = _users.FirstOrDefault().Value.User.Username;
            CancellationToken token = new();


            await Clients.All.SendAsync("ReceiveMessage", userName, message).ConfigureAwait(true);
            var result = new ChatRoomMessage(default, ChatroomID, userId, message, DateTime.UtcNow);
            await _context.CreateAsync(result, token, true);
        }


        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}

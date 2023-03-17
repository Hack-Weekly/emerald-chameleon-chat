using EmeraldChameleonChat.Services.AutoMapperProfiles;
using EmeraldChameleonChat.Services.DAL.DbContexts;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.Model.Entity;
using EmeraldChameleonChat.Services.Model.DTO;
using Microsoft.AspNetCore.SignalR;
using EmeraldChameleonChat.Services.DAL.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using EmeraldChameleonChat.Services.Migrations;
using System.Text.RegularExpressions;

namespace EmeraldChameleonChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IChatMessageRepository _context;
        private readonly IMapper _mapper;
        public ChatHub(ILogger<ChatHub> logger, IChatMessageRepository context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        //public async Task JoinChatRoom(string chatRoomName)
        //{
        //    await this.Groups.AddToGroupAsync(chatRoomName).ConfigureAwait(false);

        //    Dictionary<string, string> messages = await this.DatabaseManager.GetChatHistory(chatRoomName).ConfigureAwait(false);

        //    await this.Clients.Group(chatRoomName).BroadcastMessageAsync(messages);
        //}

        public async Task SendMessage(string user, string message, string room)
        {
            await JoinRoom(room).ConfigureAwait(false);
            await Clients.Group(room).SendAsync("ReceiveMessage", user, " joined to " + room).ConfigureAwait(true);

            CancellationToken token = new();

            var newMessage = new Services.Model.Entity.ChatMessage(id: default, chatId: room, userName: user, message: message, dateTime: default); 

            await Clients.Group(room).SendAsync("ReceiveMessage", user, message).ConfigureAwait(true);
            await _context.CreateAsync(  newMessage, token, true);
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

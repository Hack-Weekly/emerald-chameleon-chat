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
using EmeraldChameleonChat.Services.Migrations;

namespace EmeraldChameleonChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IChatRoomMessageRepository _context;
        private readonly IMapper _mapper;
        public ChatHub(ILogger<ChatHub> logger, IChatRoomMessageRepository context, IMapper mapper)
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
            await Clients.Group(room).SendAsync("ReceiveMessage", user, " joined the chatroom " + room).ConfigureAwait(true);

            CancellationToken token = new();

            var result = new ChatRoomMessage(default,room,user,message,DateTime.UtcNow);

            await Clients.Group(room).SendAsync("ReceiveMessage", user, message).ConfigureAwait(true);
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

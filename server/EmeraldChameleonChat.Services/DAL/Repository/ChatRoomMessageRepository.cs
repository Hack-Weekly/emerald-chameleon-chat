using EmeraldChameleonChat.Services.DAL.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmeraldChameleonChat.Services.Model.Entity;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using Microsoft.AspNetCore.Mvc;
using EmeraldChameleonChat.Services.Model.DTO;
using EmeraldChameleonChat.Services.AutoMapperProfiles;

namespace EmeraldChameleonChat.Services.DAL.Repository
{
    public class ChatRoomMessageRepository : BaseRepository<ChatRoomMessage>, IChatRoomMessageRepository
    {
        private readonly EmeraldChameleonChatContext _context;

        public ChatRoomMessageRepository(EmeraldChameleonChatContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<ChatRoomMessageDto>> GetAllMessagesByChatIdAsync(Guid chatId)
        {
            var response = new List<ChatRoomMessageDto>();

            var allGroupMessages = _context.ChatRoomMessage.AsNoTracking().Where(mess => mess.ChatRoomId == chatId).ToListAsync();
            var allGroupMessagesDto = _context.ChatRoomMessage.MapToDTO<List<ChatRoomMessageDto>>();

            response = allGroupMessagesDto;
            return response;
        }

        public async Task<List<ChatRoomMessage>> GetChatHistory(string chatRoomName)
        {
            var chatRoomId = GetChatRoomId(chatRoomName).Result;
            return await _context.Set<ChatRoomMessage>()
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<Guid?> GetChatRoomId(string chatRoomName)
        {
            var chatRoom = await _context.Set<ChatRoom>()
                .SingleOrDefaultAsync(c => c.Name == chatRoomName);

            return chatRoom?.Id;
        }
    }
}

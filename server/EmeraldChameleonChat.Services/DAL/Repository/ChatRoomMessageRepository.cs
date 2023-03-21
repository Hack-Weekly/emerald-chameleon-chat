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

            var allUsers = _context.ChatRoomMessage.AsNoTracking().Where(mess => mess.ChatRoomId == chatId).ToList();
            var allUsersDto = _context.ChatRoomMessage.MapToDTO<List<ChatRoomMessageDto>>();

            //var allUsers = _dbContext.Users.AsNoTracking().Where(U => U.UserId != userId).ToList();
            //var allUsersDto = _mapper.Map<List<GetUserDTO.Response>>(allUsers);

            response = allUsersDto;
            return response;
        }
    }
}

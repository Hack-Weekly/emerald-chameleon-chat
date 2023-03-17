using EmeraldChameleonChat.Services.DAL.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmeraldChameleonChat.Services.Model.Entity;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;

namespace EmeraldChameleonChat.Services.DAL.Repository
{
    public class ChatMessageRepository : BaseRepository<ChatMessage>, IChatMessageRepository
    {
        private readonly EmeraldChameleonChatContext _context;

        public ChatMessageRepository(EmeraldChameleonChatContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}

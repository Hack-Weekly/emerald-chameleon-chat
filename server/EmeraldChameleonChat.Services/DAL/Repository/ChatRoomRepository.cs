using EmeraldChameleonChat.Services.DAL.DbContexts;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldChameleonChat.Services.DAL.Repository
{
    public class ChatRoomRepository : BaseRepository<ChatRoom>, IChatRoomRepository
    {
        private readonly EmeraldChameleonChatContext _context;

        public ChatRoomRepository(EmeraldChameleonChatContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

    }
}

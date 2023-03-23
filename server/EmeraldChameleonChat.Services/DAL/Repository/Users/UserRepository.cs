using EmeraldChameleonChat.Services.DAL.DbContexts;
using EmeraldChameleonChat.Services.DAL.Repository;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldChameleonChat.Services.Repository
{ 
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly EmeraldChameleonChatContext _context;

        public UserRepository(EmeraldChameleonChatContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string username, string email)
        {            
            return await _context.Users.AnyAsync(x => x.Username.ToUpper() == username.ToUpper() || (!string.IsNullOrWhiteSpace(x.Email) && x.Email == email));
        }
        public async Task<User> GetUserByUserName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x=> x.Username.ToUpper().Equals(username.ToUpper()));
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.ToUpper().Equals(email.ToUpper()));
        }
        public async Task<User> GetUserById(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
        }
    }
}

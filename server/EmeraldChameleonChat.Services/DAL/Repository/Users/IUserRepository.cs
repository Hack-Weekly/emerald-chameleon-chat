using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldChameleonChat.Services.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> UserExists(string username, string email);
        Task<User> GetUserByUserName(string username);
        Task<User> GetUserByEmail(string email);

    }
}

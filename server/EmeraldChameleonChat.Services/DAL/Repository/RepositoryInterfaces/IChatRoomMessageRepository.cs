using EmeraldChameleonChat.Services.Model.Entity;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces
{
    public interface IChatRoomMessageRepository : IRepository<Model.Entity.ChatRoomMessage>
    {
    }
}

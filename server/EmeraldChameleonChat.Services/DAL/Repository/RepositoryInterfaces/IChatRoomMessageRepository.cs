using EmeraldChameleonChat.Services.Model.DTO;
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
        Task<List<ChatRoomMessage>> GetChatHistory(string chatRoomName);
        Task<Guid?> GetChatRoomId(string chatRoomName);
    }
}

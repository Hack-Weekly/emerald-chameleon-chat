using EmeraldChameleonChat.Services.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldChameleonChat.Services.Hubs
{
    internal class UserData
    {
        public string ConnectionId { get; set; }
        public DateTime ConnectedAt { get; set; }
        public User User { get; set; } = new User();
    }
}

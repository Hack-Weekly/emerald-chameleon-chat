using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldChameleonChat.Services.Hubs
{
    public class HubUserSession
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime ConnectedAt { get; set; }
        public string Roomname { get; set; } = string.Empty;
        public Guid RoomId { get; set; } = Guid.Empty;
    }
}

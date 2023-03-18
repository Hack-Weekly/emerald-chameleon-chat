using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmeraldChameleonChat.Services.Model.DTO 
{
    public class ChatRoomMessageDTO : IDTO
    {
        public class Request
        {
            public string Name { get; set; }
            public string UserId { get; set; }
            public string MessageBody { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class Response
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string UserId { get; set; }
            public string MessageBody { get; set; }
            public DateTime CreatedDate { get; set; }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmeraldChameleonChat.Services.Model.DTO 
{
    public class ChatMessageDTO : IDTO
    {
        public class Request
        {
            public string ChatName { get; set; }
            public string UserName { get; set; }
            public string Message { get; set; }
        }

        public class Response
        {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string ChatName { get; set; }
            public string UserName { get; set; }
            public string Message { get; set; }
            public DateTime CreatedDate { get; set; } = DateTime.Now;
        }
        
    }
}

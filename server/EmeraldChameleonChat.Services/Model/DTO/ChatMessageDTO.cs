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
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ChatId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
       
    }
}

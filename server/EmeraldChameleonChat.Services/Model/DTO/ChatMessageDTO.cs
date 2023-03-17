using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmeraldChameleonChat.Services.Model.DTO 
{
    internal class ChatMessageDTO : IDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
    }
}

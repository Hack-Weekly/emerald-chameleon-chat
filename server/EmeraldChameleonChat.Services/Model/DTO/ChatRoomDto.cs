using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldChameleonChat.Services.Model.DTO
{
    public class ChatRoomDto : IDTO
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastConnection { get; set; }
        public Guid CreatorId { get; set; }
        public bool isActive { get; set; }
    }
}

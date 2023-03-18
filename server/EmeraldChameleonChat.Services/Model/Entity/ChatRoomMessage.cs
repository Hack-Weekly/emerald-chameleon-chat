using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace EmeraldChameleonChat.Services.Model.Entity
{
    public class ChatRoomMessage : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey(nameof(ChatRoom.Id))]
        public virtual Guid ChatRoomId { get; set; }
        public string UserId { get; set; }
        public string MessageBody { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ChatRoomMessage(Guid id, Guid chatRoomId, string userId, string messageBody, DateTime dateTime) 
        {
            Id = id;
            ChatRoomId = ChatRoomId;
            UserId = userId;
            MessageBody = messageBody;
            CreatedDate = dateTime;
        }

        public ChatRoomMessage()
        {
        }
    }
}

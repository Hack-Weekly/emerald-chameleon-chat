using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using EmeraldChameleonChat.Services.Model.Entity.Users;

namespace EmeraldChameleonChat.Services.Model.Entity
{
    public class ChatRoomMessage : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ChatRoomId { get; set; }
        [ForeignKey(nameof(ChatRoomId))]
        public virtual ChatRoom ChatRoom { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public string MessageBody { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ChatRoomMessage(Guid id, Guid chatRoomId, Guid userId, string messageBody, DateTime dateTime) 
        {
            Id = id;
            ChatRoomId = chatRoomId;
            UserId = userId;
            MessageBody = messageBody;
            CreatedDate = dateTime;
        }

        public ChatRoomMessage()
        {
        }
    }
}

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

        public string Name { get; set; }

        [Required(ErrorMessage = "User Required.")]
        [MaxLength(250)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Message cannot be blank.")]
        [MaxLength(250)]
        public string MessageBody { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ChatRoomMessage(Guid id, string chatName, string userId, string messageBody, DateTime dateTime) 
        {
            Id = id;
            Name = chatName;
            UserId = userId;
            MessageBody = messageBody;
            CreatedDate = dateTime;
        }

        public ChatRoomMessage()
        {
        }
    }
}

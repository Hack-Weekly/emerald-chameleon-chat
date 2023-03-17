using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmeraldChameleonChat.Services.Model.Entity
{
    public class ChatMessage : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string ChatId { get; set; }

        [Required(ErrorMessage = "User Required.")]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Message cannot be blank.")]
        [MaxLength(250)]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ChatMessage(Guid id, string chatId, string userName, string message, DateTime dateTime) 
        {
            Id = id;
            ChatId = chatId;
            UserName = userName;
            Message = message;
            CreatedDate = dateTime;
        }
    }
}

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
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User Required.")]
        [MaxLength(250)]
        public string UserName { get; set; }

        [MaxLength(250)]
        public string Message { get; set; }

        public ChatMessage(string userName, string message) 
        {
            UserName = userName;
            Message = message;
        }
    }
}

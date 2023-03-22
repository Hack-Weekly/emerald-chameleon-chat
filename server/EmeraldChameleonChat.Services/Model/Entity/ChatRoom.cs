using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmeraldChameleonChat.Services.Model.Entity.Users;
using Microsoft.EntityFrameworkCore;

namespace EmeraldChameleonChat.Services.Model.Entity
{
    public class ChatRoom : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey(nameof(ChatRoom.Name))]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime LastConnection { get; set; } 
        public Guid CreatorId { get; set; }
        [ForeignKey(nameof(CreatorId))]
        public virtual User User { get; set; }
        public bool isActive { get; set; }

        public ChatRoom(string roomName, string roomDescription, Guid userId)
        {
            Id= new Guid();
            Name= roomName;
            Description= roomDescription;
            CreatedDate= DateTime.UtcNow;
            LastConnection= DateTime.MinValue;
            CreatorId= userId;
            isActive= true;
        }

        public ChatRoom()
        {

        }
    }
}

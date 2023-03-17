using EmeraldChameleonChat.Services.DAL.DbContexts;
using EmeraldChameleonChat.Services.DAL.Repository.RepositoryInterfaces;
using EmeraldChameleonChat.Services.Model.Entity;
using Microsoft.AspNetCore.SignalR;

namespace EmeraldChameleonChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> logger;
        private readonly IChatMessageRepository context;
        public ChatHub(ILogger<ChatHub> logger, IChatMessageRepository context)
        {
            this.logger = logger;
            this.context = context;
        }
    
        public async Task SendMessage(string user, string message)
        {
            this.logger.LogInformation(message);
            var token = new CancellationToken();
            ChatMessage newMessage = new ChatMessage(user, message);
            newMessage.UserName = user;
            newMessage.Message = message;   
            await context.CreateAsync(newMessage,token,true);
            await Clients.All.SendAsync("ReceiveMessage", user, message);   
        }

    }
}

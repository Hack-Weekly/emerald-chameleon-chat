using Microsoft.AspNetCore.SignalR;

namespace EmeraldChameleonChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> logger;
        public ChatHub(ILogger<ChatHub> logger)
        {
            this.logger = logger;
        }
    
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);   
        }

    }
}

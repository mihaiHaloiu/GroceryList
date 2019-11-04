using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{    
    public class ChatHub : Hub
    {        
        public override async Task  OnConnectedAsync()
        {

            var groceryListID = Context.GetHttpContext().Request.Query["groceryListID"].ToString();

            await Groups.AddToGroupAsync(Context.ConnectionId, groceryListID);
            await base.OnConnectedAsync();
        }

        
        public async Task SendMessage(string user, string message, string date)
        {
            var groceryListID = Context.GetHttpContext().Request.Query["groceryListID"].ToString();

            await Clients.GroupExcept(groceryListID, Context.ConnectionId).SendAsync("ReceiveMessage", user, message, date);
        }   
    }
}
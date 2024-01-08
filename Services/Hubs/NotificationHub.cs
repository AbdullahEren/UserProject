using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Services.Hubs
{
    
    public sealed class NotificationHub : Hub<INotificationHub>
    {   
        [Authorize]
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        [Authorize]
        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        [Authorize]
        public override async Task OnConnectedAsync()
        {
            if (Context.User.IsInRole("Admin"))
            {
                 await JoinGroup("Admins");
            }
            await base.OnConnectedAsync();
        }
        [Authorize]
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.IsInRole("Admin"))
            {
                await LeaveGroup("Admins");
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotify(string userName)
        {

            await Clients.Group("Admins").ReciveNotify( $"{userName} is registered.");
        }

    }
}

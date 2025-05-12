using Microsoft.AspNetCore.SignalR;

namespace IdentityManagerAPI
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userType = Context.GetHttpContext().Request.Query["type"];
            var userId = Context.UserIdentifier;

            if (userType == "worker")
                await Groups.AddToGroupAsync(Context.ConnectionId, "Workers");

            await base.OnConnectedAsync();
        }
    }
}
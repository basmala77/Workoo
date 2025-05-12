using IdentityManagerAPI;
using Microsoft.AspNetCore.SignalR;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyUser(string userId, string message)
    {
        await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
    }

    public async Task SendNotification(string message)
    {
        await _hubContext.Clients.Group("Workers").SendAsync("ReceiveMessage", message);
    }
}

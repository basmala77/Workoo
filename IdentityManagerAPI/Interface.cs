using Microsoft.AspNetCore.SignalR;

namespace IdentityManagerAPI
{
    public interface INotificationService
    {
        Task NotifyUser(string userId, string message);


        Task SendNotification(string message);
    }
       
}

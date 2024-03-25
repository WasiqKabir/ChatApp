using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hub
{
    public class UserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
           return connection.User?.Identity?.Name;
        }
    }
}

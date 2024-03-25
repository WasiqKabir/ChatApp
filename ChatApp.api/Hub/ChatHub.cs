using BusinessEntities.BindingModels;
using BusinessServices.Services.MessagesService;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ChatApp.Hub
{
    public class ChatHub : Hub <IChatHub>
    {
        private readonly ConcurrentDictionary<string, string> _userConnections;
        private readonly IMessageService _messageService;
        public ChatHub(ConcurrentDictionary<string, string> userConnections, IMessageService messageService)
        {
            _userConnections = userConnections;
            _messageService = messageService;
        }
        public async override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
            var connectionId = Context.ConnectionId;

            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections[userId] = connectionId;
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId) && _userConnections.ContainsKey(userId))
            {
                _userConnections.TryRemove(userId, out _);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageToAll(Notification notification)
        {
            await Clients.All.RecieveMessage(notification);
        }

        public async Task SendDirectMessage(Notification notification)
        {
            var userId = notification.ReceiverId;
            if (_userConnections.TryGetValue(userId, out var connectionId))
            {
                await Clients.Client(connectionId).RecieveMessage(notification);
                await _messageService.SaveMessage(notification);
            }
            else
            {
                Console.WriteLine($"User with ID '{notification.SenderId}' is not connected.");
            }
        }

    }
}

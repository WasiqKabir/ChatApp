using BusinessEntities.Exceptions;
using BusinessEntities.BindingModels;
using BusinessEntities.ViewModels;
using BusinessServices.Services.Chat;
using Common;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessServices.Services.MessagesService
{
    public class MessageService : IMessageService
    {
        private readonly DataContext _dbcontext;
        private readonly IChatService _chatService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context for accessing message-related data.</param>
        /// <param name="chatService">The chat service for managing chat operations.</param>
        public MessageService(DataContext dbcontext, IChatService chatService)
        {
            _dbcontext = dbcontext;
            _chatService = chatService;
        }

        /// <inheritdoc/>
        public async Task SaveMessage(Notification notification)
        {
            if (notification.Message == null || notification.SenderId == null || notification.ReceiverId == null)
                throw new BusinessException(ErrorMessages.Common.InvalidRequest);

            var message = new Messages() 
            {
                Message = notification.Message,
                ReceiverId = Guid.Parse(notification.ReceiverId),
                SenderId = Guid.Parse(notification.SenderId),
                CreatedOn = DateTime.UtcNow,
            };

            await HandleChatRecord(notification);

            await _dbcontext.Messages.AddAsync(message);
            await _dbcontext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<List<MessageViewModel>> GetMessages(string senderId, string receiverId)
        {
            var senderIdGuid = Guid.Parse(senderId);
            var receiverIdGuid = Guid.Parse(receiverId);

            var messagesQuery = _dbcontext.Messages
           .Where(x => (x.SenderId == senderIdGuid && x.ReceiverId == receiverIdGuid)
                    || (x.SenderId == receiverIdGuid && x.ReceiverId == senderIdGuid))
           .OrderBy(x => x.CreatedOn);    

            var messages = await messagesQuery.ToListAsync();
            return MessageMapper(messages);
        }

        // Maps message entities to view models
        private List<MessageViewModel> MessageMapper(List<Messages> messages)
        {
            return messages.Select(message => new MessageViewModel 
            { 
                Id = message.Id, 
                Message = message.Message, 
                SenderId = message.SenderId.ToString(),
                ReceiverId = message.ReceiverId.ToString(),
                CreatedOn = message.CreatedOn,
            }).ToList();
        }

        // Updates chat record if exists, otherwise creates a new one
        private async Task HandleChatRecord(Notification notification)
        {
            var chatHistory = await _chatService.GetChat(notification.SenderId, notification.ReceiverId);
            if (chatHistory != null)
                chatHistory.LastChatted = DateTime.UtcNow;
            else
                await _chatService.CreateChatConversation(notification.SenderId, notification.ReceiverId);

        }
    }
}

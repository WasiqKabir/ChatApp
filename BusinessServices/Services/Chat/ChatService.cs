using BusinessEntities.Exceptions;
using BusinessEntities.BindingModels;
using Common;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace BusinessServices.Services.Chat
{
    /// <summary>
    /// Represents a service for managing chat operations.
    /// </summary>
    public class ChatService : IChatService
    {
        private readonly DataContext _dbContext;

        public ChatService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task CreateChatConversation(string senderId, string receiverId)
        {
            Chats newChat1 = new()
            {
                SenderId = Guid.Parse(senderId),
                ReceiverId = Guid.Parse(receiverId),
                LastChatted = DateTime.UtcNow
            };
            Chats newChat2 = new()
            {
                ReceiverId = Guid.Parse(senderId),
                SenderId = Guid.Parse(receiverId),
                LastChatted = DateTime.UtcNow
            };
            await _dbContext.Chats.AddAsync(newChat1);
            await _dbContext.Chats.AddAsync(newChat2);
        }

        /// <inheritdoc/>
        public async Task<Chats?> GetChat(string senderId, string receiverId)
        {
            Guid sender_Id = Guid.Parse(senderId);
            Guid receiver_Id = Guid.Parse(receiverId);
            Chats? chatHistory = null;
            try
            {
               chatHistory = await _dbContext.Chats.SingleOrDefaultAsync(c => c.SenderId == sender_Id && c.ReceiverId == receiver_Id);
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessException(ErrorMessages.Validation.MoreThanOneChatRecord);
            }
            return chatHistory;
        }

    }
}

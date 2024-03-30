using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services.Chat
{
    public interface IChatService
    {
        /// <summary>
        /// Creates a new chat conversation between the specified sender and receiver.
        /// </summary>
        /// <param name="senderId">The unique identifier of the sender.</param>
        /// <param name="receiverId">The unique identifier of the receiver.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateChatConversation(string senderId, string receiverId);

        /// <summary>
        /// Retrieves the chat history between the specified sender and receiver.
        /// </summary>
        /// <param name="senderId">The unique identifier of the sender.</param>
        /// <param name="receiverId">The unique identifier of the receiver.</param>
        /// <returns>The chat history between the specified sender and receiver, or null if no chat history is found.</returns>
        Task<Chats?> GetChat(string senderId, string receiverId);
    }
}

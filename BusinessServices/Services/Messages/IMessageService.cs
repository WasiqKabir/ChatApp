using BusinessEntities.BindingModels;
using BusinessEntities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services.MessagesService
{
    /// <summary>
    /// Represents a service for managing message-related operations.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Saves a message notification.
        /// </summary>
        /// <param name="notification">The notification containing message details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveMessage(Notification notification);

        /// <summary>
        /// Retrieves messages between the specified sender and receiver.
        /// </summary>
        /// <param name="senderId">The unique identifier of the sender.</param>
        /// <param name="receiverId">The unique identifier of the receiver.</param>
        /// <returns>A list of message view models.</returns>
        Task<List<MessageViewModel>> GetMessages(string senderId, string receiverId);
    }
}

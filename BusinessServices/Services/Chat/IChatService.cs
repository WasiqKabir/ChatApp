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
        Task CreateChatConversation(string senderId, string receiverId);
        Task<Chats?> GetChat(string senderId, string receiverId);
    }
}

using BusinessEntities.BindingModels;
using BusinessEntities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services.MessagesService
{
    public interface IMessageService
    {
        public Task SaveMessage(Notification notification);
        public Task<List<MessageViewModel>> GetMessages(string senderId, string receiverId);
    }
}

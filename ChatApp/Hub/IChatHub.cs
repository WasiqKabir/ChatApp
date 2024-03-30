using BusinessEntities.BindingModels;

namespace ChatApp.Hub
{
    public interface IChatHub
    {
        public Task RecieveMessage(Notification notification);

    }
}

using ChatApp.Hub;
using BusinessEntities.BindingModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using BusinessServices.Services.MessagesService;

namespace ChatApp.Controllers
{
    [Route("api/Communication")]
    public class CommunicationController : Controller
    {
        private readonly IMessageService _messageService;

        public CommunicationController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Route("get-messages")]
        public async Task<IActionResult> GetMessages(string senderId, string receiverId)
        {
            var messages = await _messageService.GetMessages(senderId, receiverId);
            return Ok(messages);

        }
    }
}

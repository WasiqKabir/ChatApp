using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserViewModel Sender { get; set; } = new UserViewModel();
        public UserViewModel Receiver { get; set; } = new UserViewModel();

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Chats
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }
        [ForeignKey("Receiver")]
        public Guid ReceiverId { get; set; }
        public DateTime LastChatted { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_06_Session.Domain.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public int MemberId { get; set; }
    }
}

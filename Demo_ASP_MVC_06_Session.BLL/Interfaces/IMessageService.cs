using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo_ASP_MVC_06_Session.Domain.Entities;

namespace Demo_ASP_MVC_06_Session.BLL.Interfaces
{
    public interface IMessageService
    {
        public Message Write(Message message);
        public IEnumerable<Message> Read();
        public bool Delete(int id);
        public Message GetById(int id);
    }
}

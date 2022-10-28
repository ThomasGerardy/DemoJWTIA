using Demo_ASP_MVC_06_Session.BLL.Interfaces;
using Demo_ASP_MVC_06_Session.DAL.Interfaces;
using Demo_ASP_MVC_06_Session.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ASP_MVC_06_Session.BLL.Services
{
    internal class MessageServices : IMessageService
    {
        private IMessageRepository _messageRepository;

        public MessageServices(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IEnumerable<Message> Read()
        {
            throw new NotImplementedException();
        }

        public Message? Write(Message message)
        {
            if (string.IsNullOrWhiteSpace(message.Content))
                throw new ArgumentException();



            return null;
        }
    }
}

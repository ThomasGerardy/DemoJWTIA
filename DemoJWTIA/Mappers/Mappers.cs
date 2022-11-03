using Demo_ASP_MVC_06_Session.Domain.Entities;
using DemoJWTIA.Models;
using System.Runtime.CompilerServices;

namespace DemoJWTIA.Mappers
{
    public static class Mappers
    {
        public static Member ToBLL(this AuthRegisterViewModel f)
        {
            return new Member
            { 
                Pseudo = f.Pseudo,
                Email = f.Email,
                Password = f.Password
            };
        }
        public static ConnectedUserDTO ToDTO(this Member member)
        {
            return new ConnectedUserDTO
            { 
                Email = member.Email,
                Pseudo = member.Pseudo,
                MemberId = member.MemberId,
            };
        }
        public static Message MessageToBll(this MessageViewModel m)
        {
            return new Message
            {
                Content = m.Content,
            };
        }
    }
}

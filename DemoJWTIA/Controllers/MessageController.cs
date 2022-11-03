using Demo_ASP_MVC_06_Session.BLL.Interfaces;
using Demo_ASP_MVC_06_Session.Domain.Entities;
using DemoJWTIA.Mappers;
using DemoJWTIA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DemoJWTIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("writeMessage")]
        [Authorize("connected")]
        public IActionResult Write(MessageViewModel m)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;

            int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                Message? newMessage = new Message();
                newMessage.Content = m.Content;
                newMessage.MemberId = id;


                _messageService.Write(newMessage);
                return Ok(newMessage);

            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }    
        }
        [HttpGet("readMessage")]
        public IActionResult Read()
        {
            
            return Ok(_messageService.Read());
        }

        [HttpDelete("{id}")]
        // TODO : gérrer le delete
        public IActionResult Delete(int id)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            int connectedId = -1;
            if(identity != null)
                 connectedId = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);
            if(_messageService.GetById(id).MemberId != connectedId)
                return BadRequest();
            
            _messageService.Delete(id);
            return Ok();
        }
    }
}

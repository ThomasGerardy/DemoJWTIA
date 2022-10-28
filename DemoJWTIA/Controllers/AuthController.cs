﻿using Demo_ASP_MVC_06_Session.BLL.Interfaces;
using Demo_ASP_MVC_06_Session.Domain.Entities;
using DemoJWTIA.Mappers;
using DemoJWTIA.Models;
using DemoJWTIA.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoJWTIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly TokenManager _tokenManager;
        public AuthController(IMemberService memberService, TokenManager tokenManager)
        {
            _memberService = memberService;
            _tokenManager = tokenManager;
        }
        [HttpPost("register")]
        public IActionResult Register (AuthRegisterViewModel form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
            _memberService.Register(form.ToBLL());
            return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("login")]
        public IActionResult Login(AuthLoginViewModel form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                Member? connectedUser = _memberService.Login(form.Idenfifiant, form.Password);
                if (connectedUser == null) return BadRequest("Utilisateur inexistant");

                ConnectedUserDTO user = connectedUser.ToDTO();
                user.Token = _tokenManager.GenerateToken(connectedUser);

                return Ok(user);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Authorize("connected")]
        public IActionResult Get()
        {
            return Ok("ça fonctionne");
        }
    }
}

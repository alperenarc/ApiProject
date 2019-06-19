using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnewAPIproject.Models;
using AnewAPIproject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnewAPIproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JwtController : ControllerBase
    {
        private IUserService _userService;

        public JwtController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Name, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnewAPIproject.Models;
using AnewAPIproject.DTOs;
using AnewAPIproject.Helper;

namespace AnewAPIproject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserContext _context;

        public AccountController(UserContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<LoginUser>> Login(LoginUser user)
        {
            var Check = await _context.Users.Where(x => x.Email == user.Email).FirstOrDefaultAsync();

            var PasswordCheck = PasswordHelper.ValidatePassword(user.Password, Check.Password);


            if (PasswordCheck == true)
            {
                return Ok(Check);
            }
            else
            {
                return NotFound("Invalid User");
            }

        }


    }
}

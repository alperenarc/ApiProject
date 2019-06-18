using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnewAPIproject.Models;
using Microsoft.AspNetCore.Authorization;

namespace AnewAPIproject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        // GET: api/User
        [Route("")]
        [Route("User")]
        [Route("User/Getusers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        
        public async Task<ActionResult<User>> PostUser()
        {

            User adduser = new User();
            adduser.Name = "Ercüment";
            adduser.Email = "Ercu@gmail.com";

            var pass = Helper.PasswordHelper.HashPassword("alperen123");
            adduser.Password = pass;


            _context.Users.Add(adduser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = adduser.Id }, adduser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        [HttpGet]
        public async Task<List<User>> GetUserByQuery()
        {
            string email = "Ercu@gmail.com";
            string password = "alperen123";

            var user = await _context.Users
                .Where(x => x.Email == email).ToListAsync();

            string correctHash = user.Select(x=>x.Password).FirstOrDefault();

            bool Varmi = Helper.PasswordHelper.ValidatePassword(password, correctHash);
            if (Varmi == true)
            {
                return user;
            }
            else
            {
                user = null;
            }
            
            return user;
        }

        [HttpPost]
        public async Task<List<User>> GetUserPost(string email,string password)
        {

            var user = await _context.Users.Where(x => x.Email == email).ToListAsync();
            string correctHash = _context.Users.Select(x => x.Password).FirstOrDefault();

            bool Varmi = Helper.PasswordHelper.ValidatePassword(password, correctHash);
            if (Varmi == true)
            {
                return user;
            }
            else
            {
                user = null;
            }

            return user;
        }


        public async Task<bool> IsAccept(string ApiKey)
        {

            var check = await _context.Users.Select(x => x.ApiKey).FirstOrDefaultAsync();
            if (String.IsNullOrEmpty(check))
            {
                return true;
            }
            return false;

        }
    }
}

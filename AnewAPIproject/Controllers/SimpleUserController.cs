using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AnewAPIproject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnewAPIproject.Controllers
{
    public class SimpleUserController : ApiController
    {
        List<User> users = new List<User>();

        public SimpleUserController() { }

        public SimpleUserController(List<User> users)
        {
            this.users = users;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Task.FromResult(GetAllUsers());
        }

        
    }
}
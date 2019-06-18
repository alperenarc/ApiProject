
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnewAPIproject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ApiKey { get; set; }

        public User()
        {
            string GuidKey = Guid.NewGuid().ToString();
            ApiKey = GuidKey;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnewAPIproject.Models
{
    public class UserContext :DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        internal object SingleOrDefault(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}

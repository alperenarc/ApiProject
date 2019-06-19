using AnewAPIproject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnewAPIproject.Helper
{
    public class TokenHelper
    {
        private readonly UserContext _context;

        public TokenHelper(UserContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Key()
        {

            var Keys = await _context.Users.Select(x => x.ApiKey).ToListAsync();

            return Keys;
        }
    }
}

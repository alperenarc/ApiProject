using AnewAPIproject.Helper;
using AnewAPIproject.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AnewAPIproject.Services
{
    public interface IUserService
    {
        JWTUser Authenticate(string username, string password);
        IEnumerable<JWTUser> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        private List<JWTUser> _users = new List<JWTUser>
        {
            new JWTUser { Id = 20, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };
        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public JWTUser Authenticate(string username, string password)
        {
            
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<JWTUser> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }

        JWTUser IUserService.Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using MyWebApp.Data;
using MyWebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWebApp.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _config;

        public AuthenService(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        public User AuthenticateUser(LoginModel loginModel)
        {
            User user = _context.Users.SingleOrDefault(u => (u.Email.
                       Equals(loginModel.Email) &&
                       u.Password.Equals(loginModel.Password))
                       );
            return user;
        }

        public string GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
            var credentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);

            //claim là các thông tin chứa trong token và được mã hóa
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.Fullname),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("UserId", user.UserId.ToString()), // Nếu claim mặc định ko có thì tự định nghĩa

                new Claim("TokenId", Guid.NewGuid().ToString())

                //roles...

            };
            var expiryTime = DateTime.UtcNow.AddMinutes(2); // token tồn tại trong 2p

            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"], 
                claims, expires: expiryTime, signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Services
{
    public interface IAuthenService
    {
       public  User AuthenticateUser(LoginModel loginModel);

        string GenerateToken(User user);
    }
}

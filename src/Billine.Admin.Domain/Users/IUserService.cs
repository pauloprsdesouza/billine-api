using System.Threading.Tasks;

namespace Billine.Admin.Domain.Users
{
    public interface IUserService
    {
        Task<User> Update(User user);
        Task<User> SignIn(User user);
        Task<User> SignUp(User user);
        Task<User> ForgotPassword(User user);
        Task<User> ChangePassword(User user);
        Task<User> GetProfile(string userEmail);
    }
}

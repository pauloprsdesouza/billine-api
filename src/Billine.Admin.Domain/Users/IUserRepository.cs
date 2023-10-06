using System.Threading.Tasks;

namespace Billine.Admin.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> UpdateAsync(User user);
        Task<User> GetProfile(string email);
        Task<User> CreateAsync(User user);
    }
}

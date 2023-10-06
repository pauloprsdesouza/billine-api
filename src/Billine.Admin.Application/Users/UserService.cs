using Billine.Admin.Domain.Notifications;
using Billine.Admin.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billine.Admin.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationContext _notification;

        public UserService(IUserRepository userRepository, INotificationContext notification)
        {
            _userRepository = userRepository;
            _notification = notification;
        }

        public Task<User> ChangePassword(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> ForgotPassword(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetProfile(string userEmail)
        {
            return await _userRepository.GetProfile(userEmail);
        }

        public async Task<User> SignIn(User user)
        {
            User userRegistered = await _userRepository.GetProfile(user.Email);
            if (userRegistered is null)
            {
                return null;
            }

            bool isValidCredentials = BCrypt.Net.BCrypt.Verify(user.Password, userRegistered.Password);
            if (!isValidCredentials)
            {
                _notification.AddForbiddenError("INVALID_CREDENTIALS");
                return null;
            }

            return userRegistered;
        }

        public async Task<User> SignUp(User user)
        {
            User userRegistered = await _userRepository.GetProfile(user.Email);
            if (userRegistered != null)
            {
                _notification.AddValidationError("THIS_USER_ALREADY_EXISTS");
                return null;
            }

            user.CreatedAt = DateTimeOffset.UtcNow;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _ = await _userRepository.CreateAsync(user);

            return user;
        }

        public async Task<User> Update(User user)
        {
            _ = await _userRepository.UpdateAsync(user);

            return user;
        }
    }
}

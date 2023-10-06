using System;

namespace Billine.Admin.Contracts.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

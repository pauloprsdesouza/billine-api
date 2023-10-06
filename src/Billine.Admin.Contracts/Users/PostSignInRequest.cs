using System.ComponentModel.DataAnnotations;

namespace Billine.Admin.Contracts.Users
{
    public class PostSignInRequest
    {
        /// <summary>
        /// The User's Email
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The User's Password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Billine.Admin.Contracts.Users
{
    public class PutUserRequest
    {
        /// <summary>
        /// The User's Email
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }


        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// The User's Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// The User's Confirm Password
        /// </summary>
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace SimpleVotingApplication.Models.Login_Account
{
    public class LoginAccountModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }
    }
}

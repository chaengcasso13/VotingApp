using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace SimpleVotingApplication.Models.Login_Account
{
    [Table("VotersTable")]
    public class RegisterUserModel
    {
        [Key]
        public int VoterID { get; set; }
        [Column("loginID")]
        public string? LoginID { get; set; }
        // Name
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        // About the person
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string CivilStatus { get; set; }
        public string Citizenship { get; set; }

        // Address
        public string Province { get; set; }
        public string City { get; set; }
        public string Barangay { get; set; }
        public string Street { get; set; }

        // Account

        [Required]
        [DataType(DataType.EmailAddress)]
        [Column("username")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password did not match")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        public string Name()
        {
            return FirstName + " " + MiddleName + " " + LastName;
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleVotingApplication.Models.Login_Account
{
    [Table("AdminTable")]
    public class AdminAccountModel
    {
        [Key]
        public int AdminID { get; set; }
        [Column("Id")]
        public string Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("password")]
        public string Password { get; set; }
    }
}

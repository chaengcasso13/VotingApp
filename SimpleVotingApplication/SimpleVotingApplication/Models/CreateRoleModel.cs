using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleVotingApplication.Models
{
    public class CreateRoleModel
    {
        [Key]
        [Column("Id")]
        public string? RoleId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string? RoleName { get; set; }
    }
}

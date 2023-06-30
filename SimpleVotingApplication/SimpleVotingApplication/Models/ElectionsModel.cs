using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleVotingApplication.Models
{
    [Table("ElectionsTable")]
    public class ElectionsModel
    {
        [Key]
        [Column("electionID")]
        public int Id { get; set; }
        [Required]
        [Column("startDate")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayName("End Date")]
        [Column("endDate")]
        public DateTime EndDate { get; set; }
    }
}

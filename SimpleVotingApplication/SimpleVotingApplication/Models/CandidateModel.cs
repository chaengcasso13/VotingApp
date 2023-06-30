using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleVotingApplication.Models
{
    [Table("CandidatesTable")]
    public class CandidateModel
    {
        [Key]
        public int CandidateID { get; set; }
        [DisplayName("First Name")]
        public string? CandidateFname { get; set; }
        [DisplayName("Middle Name")]
        public string? CandidateMname { get; set; }
        [DisplayName("Last Name")]
        public string? CandidateLname { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public string FullName
        {
            get
            {
                return $"{CandidateFname} {CandidateMname} {CandidateLname}";
            }
        }
    }
}

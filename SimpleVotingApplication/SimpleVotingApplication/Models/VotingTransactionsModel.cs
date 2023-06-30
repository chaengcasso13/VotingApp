using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleVotingApplication.Models
{
    [Table("VotingTransactionsTable")]
    public class VotingTransactionsModel
    {
        [Key]
        [Column("transactionID")]
        public int TransactionID { get; set; }
        [Column("userID")]
        public string? VoterID { get; set; }
        [Column("dateOfTransaction")]
        public DateTime DateOfTransaction { get; set; }
        [Column("candidateID")]
        public int CandidateID { get; set; }

        //public List<CandidateModel>? Candidates { get; set; }
    }
}

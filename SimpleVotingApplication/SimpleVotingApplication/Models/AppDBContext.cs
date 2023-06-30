using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleVotingApplication.Models.Login_Account;
using SimpleVotingApplication.Models;

namespace SimpleVotingApplication.Models
{
    public class AppDBContext: IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
            
        }

        public DbSet<RegisterUserModel> VotersTable { get; set; }
        public DbSet<AdminAccountModel> AdminAccounts { get; set; }
        public DbSet<CandidateModel> Candidates { get; set; }
        public DbSet<SimpleVotingApplication.Models.CreateRoleModel> CreateRoleModel { get; set; } = default!;
        public DbSet<ElectionsModel> Elections { get; set; }
        public DbSet<VotingTransactionsModel> Transactions { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleVotingApplication.Models;

namespace SimpleVotingApplication.Controllers
{
    public class VotingTransactionController : Controller
    {
        private readonly AppDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public VotingTransactionController(AppDBContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._dbContext = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        //TODO: Create Navbar for Voters
        public IActionResult Index()
        {
            return View();
        }

        //TODO: Create Voting Form here:
        public IActionResult Vote()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Login");
            }

            ElectionsModel electionModel = _dbContext.Elections.FirstOrDefault();
            var userID = _userManager.GetUserId(User);
            var checkUser = _dbContext.Transactions.FirstOrDefault(user => user.VoterID == userID);

            if (checkUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (electionModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (electionModel.StartDate <= DateTime.Now && electionModel.StartDate >= DateTime.Now)
            {
                return RedirectToAction("Index", "Home");
            }

            var candidates = _dbContext.Candidates.ToList();
            ViewBag.Candidates = candidates;
            return View();
        }
        [HttpPost]
        public IActionResult CastVote(int candidateID)
        {
            ElectionsModel electionModel = _dbContext.Elections.FirstOrDefault();
            if (electionModel.StartDate <= DateTime.Now && electionModel.StartDate >= DateTime.Now)
            {
                return RedirectToAction("Index", "Home");
            }
            // add vote
            var userID = _userManager.GetUserId(User);
            VotingTransactionsModel transactionsModel = new VotingTransactionsModel
            {
                VoterID = userID,
                DateOfTransaction = DateTime.Now,
                CandidateID = candidateID
            };

            _dbContext.Transactions.Add(transactionsModel);
            _dbContext.SaveChanges();


            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult TallyVotes()
        {
            var votesToTally = _dbContext.Transactions.ToList();
            var candidates = _dbContext.Candidates.ToList();
            ViewBag.CandidateVotes = candidates;
            return View(votesToTally);
        }
    }
}

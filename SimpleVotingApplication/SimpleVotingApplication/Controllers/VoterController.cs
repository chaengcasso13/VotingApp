using Microsoft.AspNetCore.Mvc;
using SimpleVotingApplication.Models;
using SimpleVotingApplication.Models.Login_Account;

namespace SimpleVotingApplication.Controllers
{
    public class VoterController : Controller
    {
        private readonly AppDBContext _dbContext;

        public VoterController(AppDBContext context)
        {
            this._dbContext = context;
        }
        [HttpGet]
        public IActionResult ViewVoters()
        {
            var model = _dbContext.VotersTable.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var data = _dbContext.VotersTable.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult DeleteVoters(int id)
        {
            var data = _dbContext.VotersTable.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult DeleteVoters(RegisterUserModel voterModel)
        {
            _dbContext.VotersTable.Remove(voterModel);
            _dbContext.SaveChanges();
            return View("ViewVoters");
        }
    }
}

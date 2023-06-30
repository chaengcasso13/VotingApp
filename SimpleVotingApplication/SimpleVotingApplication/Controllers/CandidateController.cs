using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using SimpleVotingApplication.Models;

namespace SimpleVotingApplication.Controllers
{
    public class CandidateController : Controller
    {
        private readonly AppDBContext? _dbContext;

        public CandidateController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("/Home/Index")]
        public IActionResult Index()
        {
            return View("/Views/RoleBasedPage/Index.cshtml");
        }

        [HttpGet]
        [Route("/Kandidato")]
        public IActionResult Candidates()
        {
            var candidates = _dbContext?.Candidates.ToList();
            return View("/Views/RoleBasedPage/Candidates.cshtml", candidates);
        }

        [HttpGet]
        public IActionResult CandidateForm(CandidateModel candidateModel)
        {
            return View("/Views/RoleBasedPage/CandidateForm.cshtml", candidateModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCandidate(CandidateModel candidateModel)
        {
            if(ModelState.IsValid)
            {
                await _dbContext.Candidates.AddAsync(candidateModel);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Candidates");

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var candidate = await _dbContext.Candidates.FindAsync(id);
            return View(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CandidateModel candidateModel)
        {
            var candidate = _dbContext.Candidates.SingleOrDefault(c => c.CandidateID == id);
            if (candidate == null)
            {
                return NotFound();
            }

            candidate.CandidateFname = candidateModel.CandidateFname;
            candidate.CandidateMname = candidateModel.CandidateMname;
            candidate.CandidateLname = candidateModel.CandidateLname;
            candidate.DateOfBirth = candidateModel.DateOfBirth;
            _dbContext.SaveChanges();

            return RedirectToAction("Candidates");
        }

        // DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = _dbContext.Candidates.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CandidateModel candidateModel)
        {
            _dbContext.Candidates.Remove(candidateModel);
            _dbContext.SaveChanges();

            return RedirectToAction("Candidates");
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = _dbContext.VotersTable.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }
    }
}

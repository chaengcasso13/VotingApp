using Microsoft.AspNetCore.Mvc;
using SimpleVotingApplication.Models;

namespace SimpleVotingApplication.Controllers
{
    public class ElectionController : Controller
    {
        private readonly AppDBContext _dbContext;

        public ElectionController(AppDBContext context) 
        {
            this._dbContext = context;
        }
        [HttpGet]
        public IActionResult AddElection()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddElection(ElectionsModel electionsModel)
        {
            if (ModelState.IsValid)
            {
                await _dbContext.Elections.AddAsync(electionsModel);
                await _dbContext.SaveChangesAsync();

                //TODO: Change event view after add event:
                return RedirectToAction("ViewElection");

            }
            return View();
        }
        [HttpGet]
        public IActionResult ViewElection()
        {
            var model = _dbContext.Elections.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult EditElection(int id)
        {
            var model = _dbContext.Elections.Find(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditElection(int id, ElectionsModel electionModel)
        {
            var electionEvent = _dbContext.Elections.SingleOrDefault(c => c.Id == id);
            if (electionEvent == null)
            {
                return NotFound();
            }

            electionEvent.Id = electionModel.Id;
            electionEvent.StartDate = electionModel.StartDate;
            electionEvent.EndDate = electionModel.EndDate;
            _dbContext.SaveChanges();

            //TODO: Change event view after edit event:
            return RedirectToAction("ViewElection");
        }

        [HttpGet]
        public IActionResult DeleteElection(int id)
        {
            var data = _dbContext.Elections.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteElection(ElectionsModel electionModel)
        {
            _dbContext.Elections.Remove(electionModel);
            _dbContext.SaveChanges();

            return RedirectToAction("ViewElection");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleVotingApplication.Models;
using System.Security.Principal;

namespace SimpleVotingApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpGet]
        [Authorize]
        public IActionResult CreateRole()
        { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleModel createRoleModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRoleModel.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}

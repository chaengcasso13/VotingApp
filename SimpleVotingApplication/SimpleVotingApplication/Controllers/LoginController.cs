using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleVotingApplication.Models;
using SimpleVotingApplication.Models.Login_Account;

namespace SimpleVotingApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDBContext _appDBContext;
        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDBContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._appDBContext = context;
        }
        public AppDBContext dbContext { get; set; }
        //public LoginController(AppDBContext appDBContext)
        //{
        //    dbContext = appDBContext;
        //}
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAdmin()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel registerUser)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { 
                    UserName = registerUser.Email, 
                    Email = registerUser.Email 
                };
                var result = await _userManager.CreateAsync(user, registerUser.Password);

                if (result.Succeeded)
                {
                    var voter = new RegisterUserModel()
                    {
                        LoginID = user.Id,
                        LastName = registerUser.LastName,
                        FirstName = registerUser.FirstName,
                        MiddleName = registerUser.MiddleName,
                        Sex = registerUser.Sex,
                        DateOfBirth = registerUser.DateOfBirth,
                        PlaceOfBirth = registerUser.PlaceOfBirth,
                        CivilStatus = registerUser.CivilStatus,
                        Citizenship = registerUser.Citizenship, 
                        Province = registerUser.Province,
                        City = registerUser.City,
                        Barangay = registerUser.Barangay,
                        Street = registerUser.Street,
                        Email = registerUser.Email,
                        Password = registerUser.Password
                    };

                    await _appDBContext.AddAsync(voter);
                    await _appDBContext.SaveChangesAsync();

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerUser);
        }

        [HttpPost]
        [Route("Login/Login")]
        public async Task<IActionResult> Login(LoginAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var email = await _signInManager.UserManager.FindByEmailAsync(model.Email);
                    var verifyEmail = _appDBContext.AdminAccounts.FirstOrDefault(x => x.Username == model.Email);
                    // check if admin or voter
                    if ( verifyEmail != null) 
                    {
                        // is admin
                        return RedirectToAction("TallyVotes", "VotingTransaction");
                    } else
                        // is voter
                        return RedirectToAction("Index", "Home");
                }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
    }
}

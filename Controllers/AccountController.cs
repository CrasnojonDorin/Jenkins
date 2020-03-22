using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {

        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(StoreContext context, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}



        public IActionResult Register()
        {

            var viewModel = new RegisterViewModel
            {
                Genders = _context.Genders.ToList()
            };

            return View(viewModel);
        }

        public IActionResult Login()
        {

            var viewModel = new LoginViewModel();

            return View(viewModel);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login,
                    model.Password,model.RememberMe, false);

                //czy poprawnie zalogowano
                if (result.Succeeded)
                {
                    //Zabezpieczone przed atakami przez przekieowanie na zewnętrzne strony
                    //przez sprawdzenie czu returnurl jest url lokalnym
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        //Gdy próbowano wejść w miejsce gdzie wymaga to zalogowania,
                        //to po zalogowaniu przekieruje w to miejsce gdzie chcieliśmy wejść
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "");

            return View("Login", model);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
           

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Login,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                //czy użytkownik został utworzony
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                  var userInDb = _context.Users.Single(u => u.UserName == user.UserName);

                   var customer = new Customer
                   {
                       UserId = userInDb.Id,
                       GenderId = model.GenderId
                   };

                   _context.Customers.Add(customer);
                   _context.SaveChanges();

                   return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            model.Genders = _context.Genders.ToList();


            return View("Register", model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();


            return RedirectToAction("Index", "Home");
        }

    }
}
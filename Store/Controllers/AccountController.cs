using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class AccountController : Controller
    {

        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AccountController(StoreContext context, 
            UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {

            var viewModel = new RegisterViewModel
            {
                Genders = _context.Genders.ToList()
            };
           
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {

            var viewModel = new LoginViewModel();

            return View(viewModel);

        }

        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName,
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
                    System.Diagnostics.Debug.WriteLine($"Użytkownik {model.UserName} zalogował się.");
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "");

            return View("Login", model);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                var user = new User();

                _mapper.Map(model, user);

                var result = await _userManager.CreateAsync(user, model.Password);

                //czy użytkownik został utworzony
                if (result.Succeeded)
                {
                    //automatycznie dodaj nowego użytkownika do roli User
                    await _userManager.AddToRoleAsync(user, "User");

                    //zaloguj od razu po rejestracji
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    _context.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Przekierowano do home, po rejestracji");
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    System.Diagnostics.Debug.WriteLine("Error przy rejestracji");
                    ModelState.AddModelError("", error.Description);
                }
            }

            model.Genders = _context.Genders.ToList();

            System.Diagnostics.Debug.WriteLine($"Użytkownik {model.UserName} zarejestrował się.");
            return View("Register", model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            System.Diagnostics.Debug.WriteLine("Poprawne wylogowanie");
            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> Profile()
        {
             _signInManager.IsSignedIn(User);

             var userName = User.Identity.Name;

             var userInDb = await _userManager.FindByNameAsync(userName);

            var user = await _context.Users
                .Include(u => u.Gender)
                .SingleAsync(x => x.Id.Equals(userInDb.Id));


            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var viewModel = _mapper.Map<User, UserFormViewModel>(user);

            viewModel.Genders = _context.Genders.ToList();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserFormViewModel model)
        {
            string uniqueFileName = null;

            if (ModelState.IsValid)
            {
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                     uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    await model.Photo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }


                var user = await _userManager.FindByIdAsync(model.Id.ToString());

                _mapper.Map(model, user);
                if (model.Photo != null)
                {
                    user.PhotoPath = uniqueFileName;
                }

                await _userManager.UpdateAsync(user);

                await _context.SaveChangesAsync();
                System.Diagnostics.Debug.WriteLine($"Użytkownik {user.UserName} edytował profil");
                return Ok();
            }


            var viewModel = _mapper.Map<UserFormViewModel, UserFormViewModel>(model);
            viewModel.Genders = _context.Genders.ToList();
            return View(viewModel);
        }

        public async Task<IActionResult> OrdersHistory()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var orders = _context.Orders.Where(x=>x.UserId == user.Id);

            System.Diagnostics.Debug.WriteLine("Wyswietlenie historii");
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _context.Orders
                .Include(x => x.User)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Id == id);


            return View(order);
        }
    }
}
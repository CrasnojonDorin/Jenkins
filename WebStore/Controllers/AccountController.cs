using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IWebHostEnvironment _hostEnvironment;


        public AccountController(StoreContext context, 
            UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
        }


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
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    GenderId = model.GenderId,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                //czy użytkownik został utworzony
                if (result.Succeeded)
                {
                    //zaloguj od razu po rejestracji
                    await _signInManager.SignInAsync(user, isPersistent: false);

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


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users
                .Include(u => u.Gender)
                .SingleAsync(x => x.Id.Equals(id));


            return View(user);
        }


        /// <summary>
        /// Edycja użytkownika
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> UserForm(int id)
        {
            var user = await _context.Users
                .Include(x=>x.Gender)
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (user == null)
                return NotFound();

            var viewModel = new UserFormViewModel
            {
                Genders = _context.Genders.ToList(),
                GenderId = user.GenderId,
                PhoneNumber = user.PhoneNumber,
                Town = user.Town,
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
            };

            return View("UserForm", viewModel);
        }


        /// <summary>
        /// Aktualizuje informacje o użytkowniku
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserForm(UserFormViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    await model.Photo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }


                var user = _userManager.FindByIdAsync(model.Id.ToString()).Result;

                //za pomocą mappera można skrócić cały kod niżej do jedenj linijki
                _mapper.Map(model, user);


                await _userManager.UpdateAsync(user);

                _context.SaveChanges();

                return RedirectToAction("Index", "Account");
            }

            var viewModel = new UserFormViewModel
            {
                Genders = _context.Genders.ToList(),
                GenderId = model.GenderId,
                PhoneNumber = model.PhoneNumber,
                Town = model.Town,
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName
            };

            return View("UserForm", viewModel);
        }


        
    }
}
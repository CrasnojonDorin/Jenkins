using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CustomerController(StoreContext context, IMapper mapper, UserManager<User> userManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        [HttpGet]
        public IActionResult Index()
        {
            var users =_context.Users
                .Include(u =>u.Customer)
                .ToList();


            return View(users);
        }

        [HttpGet]
        public IActionResult Details(string username)
        {
            var user = _context.Users
                .Include(u=>u.Customer)
                .Include(u=>u.Customer.Gender)
                .Single(u => u.UserName.Equals(username));



            return View(user);

        }

        /// <summary>
        /// Formularz klienta
        /// </summary>
        /// <returns></returns>
        public IActionResult CustomerForm()
        {

            var viewModel = new CustomerFormViewModel
            {
                Genders = _context.Genders
            };

            return View(viewModel);
        }

        /// <summary>
        /// Edycja istniejącego klienta korzystając z wcześniej utworzonego formularza
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = _context.Users
                .Include(u => u.Customer)
                .Include(u => u.Customer.Gender)
                .SingleOrDefault(c => c.Id == id);

            if (user == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Genders = _context.Genders.ToList(),
                GenderId = user.Customer.GenderId,
                PhoneNumber = user.Customer.PhoneNumber,
                Town = user.Customer.Town,
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
            };

            return View("CustomerForm", viewModel);
        }

        /// <summary>
        /// Dodaje klienta do bazy bądź aktualizuje obecnego 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CustomerFormViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Photo !=null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }


                var user = _userManager.FindByIdAsync(model.Id).Result;

                //za pomocą mappera można skrócić cały kod niżej do jedenj linijki
                _mapper.Map(model, user);


                await _userManager.UpdateAsync(user);

                var customer = _context.Customers.Single(c => c.UserId == user.Id);

                customer.GenderId = model.GenderId;
                customer.PhoneNumber = model.PhoneNumber;
                customer.Town = model.Town;
                customer.PhotoPath = uniqueFileName;

                _context.Customers.Update(customer);
                _context.SaveChanges();

                return RedirectToAction("Index", "Customer");
            }

            var viewModel = new CustomerFormViewModel
            {
                Genders = _context.Genders.ToList(),
                GenderId = model.GenderId,
                PhoneNumber = model.PhoneNumber,
                Town = model.Town,
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName
            };

            return View("CustomerForm", viewModel);


        }


    }
}
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AdministrationController : Controller
    {

        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        private readonly StoreContext _context;

        public AdministrationController(RoleManager<Role> roleManager, UserManager<User> userManager, StoreContext context,
            IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            return View(users);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;

            return View(roles);
        }


        [HttpGet]
        public async Task<IActionResult> EditRole(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role==null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return NotFound();
            }

            var model = new EditRoleViewModel
            {
                Id = id,
                RoleName = role.Name,
                Users = new List<User>()
            };

            foreach (var user in await _userManager.GetUsersInRoleAsync(role.Name))
            {
                model.Users.Add(user);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString());

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return NotFound();
            }

            role.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            model.Users = new List<User>();

            foreach (var user in await _userManager.GetUsersInRoleAsync(role.Name))
            {
                model.Users.Add(user);
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(int roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return NotFound();
            }

            var model = new List<UserRoleViewModel>();

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = await IsInRoleAsync(user,role.Name)
                };

                model.Add(userRoleViewModel);
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return NotFound();
            }

            bool ifSucceeded = false;

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId.ToString());

                if (model[i].IsSelected && !(await IsInRoleAsync(user, role.Name)))
                {
                    var result = await _userManager.AddToRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {
                        ifSucceeded = true;
                    }
                }
                else if (!model[i].IsSelected && await IsInRoleAsync(user, role.Name))
                {
                    ifSucceeded = await DeleteRoleAsync(user, role.Id);
                }

                if (ifSucceeded)
                {
                    if (i < model.Count - 1)
                    {
                        continue;
                    }

                    return RedirectToAction("EditRole", new {id = roleId});
                }
            }


            return RedirectToAction("EditRole", new { id = roleId });
        }

        /// <summary>
        /// Własna implementacja sprawdzająca czy użytkownik znajduje się w danej roli
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        private async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

            foreach (var userInRole in usersInRole)
            {
                if (userInRole.Id == user.Id)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Własna implementacja usuwania użytkownika o danej roli
        /// ponieważ removeroleinasync nie działało ://
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private async Task<bool> DeleteRoleAsync(User user, int roleId)
        {

            var userRole = new IdentityUserRole<int>
            {
                UserId = user.Id,
                RoleId = roleId
            };

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();


            return true;
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
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users
                .Include(x => x.Gender)
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

            return View("EditUser", viewModel);
        }


        /// <summary>
        /// Aktualizuje informacje o użytkowniku
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserFormViewModel model)
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

                return RedirectToAction("Index", "Administration");
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

            return View("EditUser", viewModel);
        }


    }
}
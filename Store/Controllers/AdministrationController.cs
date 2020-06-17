using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Authorize(Roles ="Admin")]

    public class AdministrationController : Controller
    {

        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        private readonly StoreContext _context;

        public AdministrationController(RoleManager<Role> roleManager, UserManager<User> userManager, StoreContext context,
            IWebHostEnvironment hostEnvironment, IMapper mapper, SignInManager<User> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();

            if (_signInManager.IsSignedIn(User))
            {
                //zabezpieczenie aby admin nie mógł usunąć i edytować swojego konta będąc zalogowanym
                var userName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                users.Remove(user);
            }

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
                    System.Diagnostics.Debug.WriteLine($"Stworzono role {model.RoleName}");

                    return RedirectToAction("Roles");
                }

                foreach (var error in result.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Nieudana próba stworzenia roli {model.RoleName}");

                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Roles()
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
                System.Diagnostics.Debug.WriteLine($"Nie znaleziono roli o id = {model.Id}");
                return NotFound();
            }

            role.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                System.Diagnostics.Debug.WriteLine($"Edytowano rolę o id = {model.Id}");

                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
                System.Diagnostics.Debug.WriteLine($"Nieudana rdycja roli o id = {model.Id}");

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
                System.Diagnostics.Debug.WriteLine($"Nie znaleziono roli o id = {roleId}");

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
                System.Diagnostics.Debug.WriteLine($"Nie znaleziono roli o id = {roleId}");
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
                    System.Diagnostics.Debug.WriteLine("Poprawne edytowano uzytkowników w roli");

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
        public async Task<IActionResult> UserDetails(int id)
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
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                PhotoPath = user.PhotoPath
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
                user.PhotoPath = uniqueFileName;


                await _userManager.UpdateAsync(user);

                _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine($"Poprawna edycja uzytkownika {user.UserName}");


                return RedirectToAction("Users", "Administration");
            }

            var viewModel = new UserFormViewModel
            {
                Genders = _context.Genders.ToList(),
                GenderId = model.GenderId,
                PhoneNumber = model.PhoneNumber,
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName
            };

            return View("EditUser", viewModel);
        }



        //
        //
        //
        //
        [HttpGet]
        public IActionResult Products()
        {
            var products = _context.Products
               .Include(u => u.Brand)
               .Include(u => u.Color)
               .Include(u => u.Sex)
               .Include(u => u.Category)
               .ToList();

            return View(products);
        }


        [HttpGet]
        public IActionResult AddProduct()
        {
            // todo dodać zapisywanie zdjęć
            var productFormViewModel = new ProductFormViewModel
            {
                Categories = _context.Categories.ToList(),
                Brands = _context.Brands.ToList(),
                Colors = _context.Colors.ToList(),
                Sexes = _context.Sexes.ToList()
            };

            return View("ProductForm", productFormViewModel);
        }

        [HttpGet]
        public IActionResult Stock(int id)
        {
            var stock = _context.Stock.FirstOrDefault(x => x.ProductId == id);

            if (stock == null)
            {
                return View(new StockViewModel { Stock = new List<Stock>(), ProductId = id });
            }

            var viewModel = new StockViewModel
            {
                ProductId = id,
                Name = stock.Name,
                Qty = stock.Qty,
                Stock = _context.Stock.Where(x => x.ProductId == id).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Stock(StockViewModel model)
        {
            if (ModelState.IsValid)
            {
                var stock = new Stock();
                stock.Name = model.Name;
                stock.ProductId = model.ProductId;
                stock.Qty = model.Qty;
                if (model.Qty == 0)
                {
                    stock.IsLastElementOrdered = true;
                }

                if (model.Id == 0)
                {
                    await _context.Stock.AddAsync(stock);
                }
                else
                {
                    stock.Id = model.Id;

                    _context.Stock.Update(stock);
                }
                await _context.SaveChangesAsync();

                System.Diagnostics.Debug.WriteLine($"Stworzenie zapasu {stock.Name} dla produktu o id={stock.ProductId}");

                return Ok();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditStock(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return NotFound();
            }


            return View(stock);
        }

        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            _context.Remove(stock);
            System.Diagnostics.Debug.WriteLine($"Usunięto zapas {stock.Name} dla produktu o id={stock.ProductId}");
            await _context.SaveChangesAsync();


            return RedirectToAction("Stock", new { id = stock.ProductId });
        }

        [HttpGet]
        public IActionResult AddColor()
        {
            return View("ColorForm", new Color());
        }

        [HttpGet]
        public async Task<IActionResult> EditColor(int id)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);

            return View("ColorForm", color);
        }

        public async Task<IActionResult> DeleteColor(int id)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            _context.Remove(color);
            await _context.SaveChangesAsync();
            System.Diagnostics.Debug.WriteLine($"Usunięto kolor {color.Name}");



            return RedirectToAction("Color");
        }


        [HttpGet]
        public IActionResult Color()
        {
            var colors = _context.Colors.ToList();

            return View(colors);
        }

        [HttpGet]
        public IActionResult Category()
        {
            return View(_context.Categories.ToList());
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View("CategoryForm", new Category());
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            return View("CategoryForm", category);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            _context.Remove(category);
            await _context.SaveChangesAsync();
            System.Diagnostics.Debug.WriteLine($"Usunięto kategorię {category.Name}");


            return RedirectToAction("Category");
        }

        [HttpGet]
        public IActionResult AddBrand()
        {
            return View("BrandForm", new Brand());
        }

        [HttpGet]
        public IActionResult Brand()
        {
            return View(_context.Brands.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> EditBrand(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View("BrandForm", brand);
        }

        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);

            if (brand == null)
            {
                return NotFound();
            }

            _context.Remove(brand);
            await _context.SaveChangesAsync();
            System.Diagnostics.Debug.WriteLine($"Usunięto brand {brand.Name}");


            return RedirectToAction("Brand");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();

                // todo zrobić to z użyciem mappera
                product.Id = model.Id;
                product.CategoryId = model.CategoryId;
                product.ColorId = model.ColorId;
                product.BrandId = model.BrandId;
                product.SexId = model.SexId;
                product.Description = model.Description;
                product.Price = decimal.Parse(model.Price);
                product.Name = model.Name;

                string uniqueFileName = null;

                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                product.PhotoPath = uniqueFileName;
                if (product.Id != 0)
                {
                    _context.Products.Update(product);
                    System.Diagnostics.Debug.WriteLine($"Zmodyfikowano produkt {product.Name} o id={product.Id}");
                }
                else
                {
                    await _context.Products.AddAsync(product);
                    System.Diagnostics.Debug.WriteLine($"Dodano produkt {product.Name} o id={product.Id}");
                }

                product.PhotoPath = uniqueFileName;

                await _context.SaveChangesAsync();


                return RedirectToAction("Index");
            }

            var viewModel = new ProductFormViewModel
            {
                Description = model.Description,
                Name = model.Name,
                ColorId = model.ColorId,
                BrandId = model.BrandId,
                Price = model.Price,
                SexId = model.SexId,
                CategoryId = model.CategoryId,
                Categories = _context.Categories.ToList(),
                Colors = _context.Colors.ToList(),
                Brands = _context.Brands.ToList(),
                Sexes = _context.Sexes.ToList()
            };

            return View("AddProduct", viewModel);

        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var productInDb = _context.Products.FirstOrDefault(x => x.Id.Equals(id));

            if (productInDb == null)
            {
                return NotFound();
            }

            var viewModel = new ProductFormViewModel();
            _mapper.Map(productInDb, viewModel);


            viewModel.Categories = _context.Categories.ToList();
            viewModel.Brands = _context.Brands.ToList();
            viewModel.Colors = _context.Colors.ToList();
            viewModel.Sexes = _context.Sexes.ToList();


            return View("ProductForm", viewModel);

        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productInDb = await _context.Products.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (productInDb == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productInDb);
            await _context.SaveChangesAsync();
            System.Diagnostics.Debug.WriteLine($"Usunięto produkt {productInDb.Name} o id={productInDb.Id}");



            return RedirectToAction("Products");

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveBrand(Brand model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                if (model.Id != 0)
                {
                    model.PhotoPath = uniqueFileName;

                    _context.Brands.Update(model);
                    System.Diagnostics.Debug.WriteLine($"Zmodyfikowano markę o id={model.Id}");
                }

                else
                {
                    // todo zrobić to z użyciem mappera
                    var brand = new Brand
                    {
                        Name = model.Name,
                        Description = model.Description,
                        PhotoPath = uniqueFileName
                    };

                    await _context.Brands.AddAsync(brand);
                    System.Diagnostics.Debug.WriteLine($"Poprawne dodanie marki o id={brand.Id}");

                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Brand");
            }

            var viewModel = new Brand
            {
                Name = model.Name,
                Description = model.Description,
            };


            return View("BrandForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    _context.Categories.Update(model);
                    System.Diagnostics.Debug.WriteLine($"Zmodyfikowano kategorię o id={model.Id}");
                }
                else
                {
                    var category = new Category
                    {
                        Name = model.Name
                    };
                    _context.Categories.Add(category);
                    System.Diagnostics.Debug.WriteLine($"Dodano kategorię o id={category.Id}");

                }
                await _context.SaveChangesAsync();

                return RedirectToAction("Category");

            }

            var viewModel = new Category { Name = model.Name };
            return View("AddCategory", viewModel);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveColor(Color model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    _context.Update(model);
                    System.Diagnostics.Debug.WriteLine($"Zmodyfikowano kolor o id={model.Id}");

                }
                else
                {
                    var color = new Color
                    {
                        Name = model.Name
                    };

                    _context.Colors.Add(color);
                    System.Diagnostics.Debug.WriteLine($"Dodano kolor o id={model.Id}");
                }

                await _context.SaveChangesAsync();
                
                return RedirectToAction("Color");
            }


            var viewModel = new Color
            {
                Name = model.Name,
            };
            System.Diagnostics.Debug.WriteLine("Poprawne zapisanie koloru");
            return View("ColorForm", viewModel);
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var orders = _context.Orders
                .Include(x => x.User)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Product)
                .ToList();


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


        /// <summary>
        /// Usuwa zamówienie z bazy i przywraca produkty z zamówienia na magazyn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _context.Orders
                .Include(x => x.OrderProducts)
                .FirstOrDefaultAsync(x => x.Id == id);

            // usuń je w bazie z orderProducts i dodaj je ponownie do sprzedaży

            var orderProducts = order.OrderProducts;
            foreach (var orderProduct in orderProducts)
            {
                _context.Remove(orderProduct);
                var stockInDb = _context.Stock.FirstOrDefault(x => x.Id == orderProduct.StockId);
                if (stockInDb == null)
                {
                    var stock = new Stock
                    {
                        Id = orderProduct.StockId,
                        Name = orderProduct.Stock.Name,
                        ProductId = orderProduct.Stock.ProductId,
                        IsLastElementOrdered = false,
                        Qty = orderProduct.Stock.Qty + 1
                    };
                    await _context.Stock.AddAsync(stock);

                }
                else
                {
                    stockInDb.Qty++;
                    stockInDb.IsLastElementOrdered = false;

                    _context.Stock.Update(stockInDb);
                }
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            System.Diagnostics.Debug.WriteLine($"Anulowano zamówienie o id={order.Id} dla użytkownika {order.User}");



            return RedirectToAction("Orders");
        }


        public async Task<IActionResult> SentOrder(int id)
        {
            var order = await _context.Orders
               .FirstOrDefaultAsync(x => x.Id == id);

            order.IsSend = true;
            order.OrderSendDate = DateTime.Now;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            System.Diagnostics.Debug.WriteLine($"Wysłano zamówienie o id={order.Id} dla użytkownika {order.User}");


            return RedirectToAction("Orders");
        }





    }
}
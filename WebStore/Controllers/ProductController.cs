using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using WebStore.Models;
using WebStore.ViewModels.ProductViewModels;
using Type = WebStore.Models.Type;

namespace WebStore.Controllers
{
    [Authorize(Roles = "User")]
    public class ProductController : Controller
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ProductController(StoreContext storeContext, IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            _context = storeContext;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var products = _context.Products
                .Include(p=>p.Brand)
                .Include(u=>u.Color)
                .Include(u=>u.Sex)
                .Include(u=>u.Size)
                .Include(u=>u.Type)
                .ToList();


            return View(products);
        }

        
        [HttpGet]
        public IActionResult AddProduct()
        {

            var productFormViewModel = new ProductFormViewModel
            {
                Types = _context.Types.ToList(),
                Brands = _context.Brands.ToList(),
                Colors = _context.Colors.ToList(),
                Sexes = _context.Sexes.ToList(),
                ShoeSizes = _context.Sizes.Where(x=>x.TypeId.Equals(1)).ToList(),
                ClothSizes = _context.Sizes.Where(x=>x.TypeId.Equals(2)).ToList()
            };

            return View("Forms/AddProduct",productFormViewModel);
        }
        
        [HttpGet]
        public IActionResult AddColor()
        {
            return View("Forms/AddColor", new ColorFormViewModel{Colors = _context.Colors.ToList()});
        }
       
        [HttpGet]
        public IActionResult AddType()
        {
            return View("Forms/AddType",new Type());
        }
       
        [HttpGet]
        public IActionResult AddSize()
        {
            return View("Forms/AddSize",new SizeFormViewModel {Types = _context.Types.ToList()});
        }
        
        [HttpGet]
        public IActionResult AddBrand()
        {
            return View("Forms/AddBrand", new BrandFormViewModel {Products = _context.Products.ToList()});
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddProduct(ProductFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();

                string uniqueFileName = null;

                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                // todo zrobić to z użyciem mappera

                product.TypeId = model.TypeId;
                product.ColorId = model.ColorId;
                product.BrandId = model.BrandId;
                product.SexId = model.SexId;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Name = model.Name;
                product.SizeId = model.SizeId;
                product.PhotoPath = uniqueFileName;

                _context.Products.Add(product);
                _context.SaveChanges();


                return RedirectToAction("AddProduct");
            }

            var viewModel = new ProductFormViewModel
            {
                Description = model.Description,
                Name = model.Name,
                ColorId = model.ColorId,
                BrandId = model.BrandId,
                Price = model.Price,
                SexId = model.SexId,
                SizeId = model.SizeId,
                TypeId = model.TypeId,
                Types = _context.Types.ToList(),
                Colors = _context.Colors.ToList(),
                Brands = _context.Brands.ToList(),
                Sexes = _context.Sexes.ToList(),
                ShoeSizes = _context.Sizes.Where(x=>x.TypeId.Equals(1)).ToList(),
                ClothSizes = _context.Sizes.Where(x=>x.TypeId.Equals(2)).ToList()
            };

            return View("Forms/AddProduct", viewModel);

        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var productInDb = _context.Products.FirstOrDefault(x=>x.Id.Equals(id));

            if (productInDb == null)
            {
                return NotFound();
            }


            var viewModel = new ProductFormViewModel();
            _mapper.Map(productInDb, viewModel);


            viewModel.Types = _context.Types.ToList();
            viewModel.Brands = _context.Brands.ToList();
            viewModel.Colors = _context.Colors.ToList();
            viewModel.Sexes = _context.Sexes.ToList();
            viewModel.ShoeSizes = _context.Sizes.Where(x => x.TypeId.Equals(1)).ToList();
            viewModel.ClothSizes = _context.Sizes.Where(x => x.TypeId.Equals(2)).ToList();



            return View("Forms/AddProduct", viewModel);

        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddBrand(BrandFormViewModel model)
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

                // todo zrobić to z użyciem mappera
                var brand = new Brand
                {
                    Name = model.Name,
                    Description = model.Description,
                    LogoPath = uniqueFileName
                };

                _context.Brands.Add(brand);
                _context.SaveChanges();

                return RedirectToAction("AddBrand");
            }

            var viewModel = new BrandFormViewModel
            {
                Name = model.Name,
                Description = model.Description,
            };



            return View("Forms/AddBrand", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddType(Type model)
        {
            if (ModelState.IsValid)
            {
                var type = new Type
                {
                    Name = model.Name
                };
                _context.Types.Add(type);
                _context.SaveChanges();

                return RedirectToAction("AddType");

            }

            var viewModel = new Type {Name = model.Name};

            return View("Forms/AddType",viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddSize(SizeFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var size = new Size
                {
                    Name = model.Name,
                    TypeId = model.TypeId
                };

                _context.Sizes.Add(size);
                _context.SaveChanges();

                return RedirectToAction("AddSize");
            }

            var viewModel = new SizeFormViewModel
            {
                Name = model.Name,
                Types = _context.Types.ToList()
            };

            return View("Forms/AddSize", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddColor(ColorFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var color = new Color
                {
                    Name = model.Name
                };

                _context.Colors.Add(color);
                _context.SaveChanges();

                return RedirectToAction("AddColor");
            }
            

            var viewModel = new ColorFormViewModel
            {
                Name = model.Name,
                Colors = _context.Colors.ToList()
            };
            return View("Forms/AddColor", viewModel);
        }

    }
}
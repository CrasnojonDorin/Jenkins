using System;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Models;
using WebStore.ViewModels.ProductViewModels;
using Type = WebStore.Models.Type;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ProductController(StoreContext storeContext, IWebHostEnvironment hostEnvironment)
        {
            _context = storeContext;
            _hostEnvironment = hostEnvironment;
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
        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Brand)
                .Include(u => u.Color)
                .Include(u => u.Sex)
                .Include(u => u.Size)
                .Include(u => u.Type)
                .Single(p => p.Id.Equals(id));


            return View(product);
        }
        
        [HttpGet]
        public IActionResult ProductForm()
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

            return View("Forms/ProductForm",productFormViewModel);
        }
        
        [HttpGet]
        public IActionResult ColorForm()
        {
            return View("Forms/ColorForm", new ColorFormViewModel{Colors = _context.Colors.ToList()});
        }
       
        [HttpGet]
        public IActionResult TypeForm()
        {
            return View("Forms/TypeForm",new Type());
        }
       
        [HttpGet]
        public IActionResult SizeForm()
        {
            return View("Forms/SizeForm",new SizeFormViewModel {Types = _context.Types.ToList()});
        }
        
        [HttpGet]
        public IActionResult BrandForm()
        {
            return View("Forms/BrandForm", new BrandFormViewModel {Products = _context.Products.ToList()});
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ProductForm(ProductFormViewModel model)
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


                return RedirectToAction("ProductForm");
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

            return View("Forms/ProductForm", viewModel);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult BrandForm(BrandFormViewModel model)
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

                return RedirectToAction("BrandForm");
            }

            var viewModel = new BrandFormViewModel
            {
                Name = model.Name,
                Description = model.Description,
            };



            return View("Forms/BrandForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult TypeForm(Type model)
        {
            if (ModelState.IsValid)
            {
                var type = new Type
                {
                    Name = model.Name
                };
                _context.Types.Add(type);
                _context.SaveChanges();

                return RedirectToAction("TypeForm");

            }

            var viewModel = new Type {Name = model.Name};

            return View("Forms/TypeForm",viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult SizeForm(SizeFormViewModel model)
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

                return RedirectToAction("SizeForm");
            }

            var viewModel = new SizeFormViewModel
            {
                Name = model.Name,
                Types = _context.Types.ToList()
            };

            return View("Forms/SizeForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ColorForm(ColorFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var color = new Color
                {
                    Name = model.Name
                };

                _context.Colors.Add(color);
                _context.SaveChanges();

                return RedirectToAction("ColorForm");
            }
            

            var viewModel = new ColorFormViewModel
            {
                Name = model.Name,
                Colors = _context.Colors.ToList()
            };
            return View("Forms/ColorForm", viewModel);
        }

    }
}
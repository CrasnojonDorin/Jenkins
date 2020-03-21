using System;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ProductController(StoreContext storeContext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _context = storeContext;
            _mapper = mapper;
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


        public IActionResult Form()
        {

            var productFormViewModel = new ProductFormViewModel
            {
                Types = _context.Types.ToList(),
                Brands = _context.Brands.ToList(),
                Colors = _context.Colors.ToList(),
                Sexes = _context.Sexes.ToList(),
                Sizes = _context.Sizes.ToList()
                
            };

            return View(productFormViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductFormViewModel model)
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
            

            return RedirectToAction("Form", "Product");
        }



    }
}
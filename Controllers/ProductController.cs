using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public ProductController(StoreContext storeContext, IMapper mapper)
        {
            _context = storeContext;
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
    }
}
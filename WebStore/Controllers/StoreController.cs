using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;

        public StoreController(StoreContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(u => u.Color)
                .Include(u => u.Sex)
                .Include(u => u.Size)
                .Include(u => u.Type)
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
    }
}
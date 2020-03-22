using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Models.DTO;

namespace WebStore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public ProductController(StoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var productsInDb = _context.Products.ToList();

            var products = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(productsInDb);


            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            var productInDb = _context.Products.SingleOrDefault(x=>x.Id.Equals(id));

            var product = _mapper.Map<Product, ProductDTO>(productInDb);

            if (product == null)
            {
                return Conflict("Nie ma takiego produktu");
            }



            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return Conflict("Coś poszło nie tak");
            }

            var productToSave = _mapper.Map<ProductDTO, Product>(product);

            _context.Products.Add(productToSave);
            _context.SaveChanges();

            return Ok();

        }


        [HttpDelete]
        [Route("{id:int}")]

        public IActionResult Delete([FromRoute]int id)
        {
            var productInDb = _context.Products.SingleOrDefault(x => x.Id.Equals(id));

            if (productInDb == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productInDb);
            _context.SaveChanges();

            return Ok();
        }


    }
}
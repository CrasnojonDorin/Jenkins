using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Models.DTO;

namespace WebStore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public CustomerController(StoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        // GET /api/customer
        public IActionResult Get()
        {
            var customersInDb = _context.Customers.ToList();

            var customers = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(customersInDb);

            return Ok(customers);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);



            if (customerInDb == null)
                return NotFound();
            else
            {
                var customer = _mapper.Map<Customer, CustomerDTO>(customerInDb);

                return Ok(customer);
            }
        }

        [HttpPost]
        //POST /api/customer
        public IActionResult New([FromBody] CustomerSaveDTO customerDto)
        {
            if (customerDto == null)
                return Conflict("Nie można dodać klienta");
            else
            {
                var customer = _mapper.Map<CustomerSaveDTO, Customer>(customerDto);
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            
            return NoContent();
        }
    }
}
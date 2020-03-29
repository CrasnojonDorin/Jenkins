using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using WebStore.Models.DTO;

namespace WebStore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly StoreContext _context;
        private readonly IMapper _mapper;


        public AccountController(StoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var usersInDb = _context.Users.ToList();
            var users = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(usersInDb);

            return Ok(users);
        }


        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            var userInDb = _context.Users.SingleOrDefault(c => c.Id.Equals(id));


            if (userInDb == null)
                return NotFound();

            return Ok(userInDb);
        }


        [HttpPost]
        public IActionResult Post([FromBody] UserSaveDTO userCreateDto)
        {
            if (userCreateDto == null)
            {
                return Conflict("Nie można dodać klienta");
            }

            var user = _mapper.Map<UserSaveDTO, User>(userCreateDto);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var userInDb = _context.Users.SingleOrDefault(x => x.Id.Equals(id));

            if (userInDb == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            
            return Ok();
        }




    }
}
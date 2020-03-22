using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            var users = _context.Users.ToList();

            return Ok(users);
        }


        [HttpGet]
        [Route("{username:alpha}")]
        public IActionResult GetUser([FromRoute] string username)
        {
            var userInDb = _context.Users.SingleOrDefault(c => c.UserName.Equals(username));


            if (userInDb == null)
                return NotFound();

            return Ok(userInDb);
        }


        //[HttpPost]
        //public IActionResult Post([FromBody] UserDTO userDTO)
        //{
        //    if (userDTO== null)
        //    {
        //        return Conflict("Nie można dodać klienta");
        //    }

        //    var user = _mapper.Map<UserDTO, User>(userDTO);

        //    _context.Users.Add(user);
        //    _context.SaveChanges();

        //    return Ok();
        //}


        [HttpDelete]
        [Route("{username:alpha}")]
        public IActionResult Delete([FromRoute] string username)
        {
            var userInDb = _context.Users.SingleOrDefault(x => x.UserName.Equals(username));

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
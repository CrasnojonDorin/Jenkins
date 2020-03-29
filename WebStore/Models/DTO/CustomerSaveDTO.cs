using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models.DTO
{
    public class CustomerSaveDTO
    {
        //public int Id { get; set; }

        //public Gender Gender { get; set; }

        public int GenderId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int? PhoneNumber { get; set; }

        public string Town { get; set; }


        //public string UserName { get; set; }

        //public string Password { get; set; }

    }
}

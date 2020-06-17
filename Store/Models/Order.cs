using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ChargeId { get; set; }

        public bool IsSend { get; set; } = false;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        [Required]
        public DateTime? OrderDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime? OrderSendDate { get; set; } = null;

        [Required]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Adres 1")]
        public string Address1 { get; set; }

        [Display(Name = "Adres 2")]
        public string Address2 { get; set; }

        public string Email { get; set; }

        [Required]
        [Display(Name ="Miasto")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Kod pocztowy")]
        public string PostCode { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderProduct> OrderProducts{ get; set; }

    }
}

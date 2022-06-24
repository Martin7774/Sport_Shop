using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Shop.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public decimal price { get; set; }
        [Display(Name = "Opis")]
        public string description { get; set; }
        [Display(Name = "Id image")]
        public string image { get; set; }

        [Display(Name = "Id category")]
        public int categoryId { get; set; }

        public Category Category { get; set; }

    }
}

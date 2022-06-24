using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Shop.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Nazwa Kategori")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public string categoryName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

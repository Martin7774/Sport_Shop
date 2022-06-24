using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Shop.Models
{
    public class SiteUser
    {

        [Display(Name = "Id użytkownika")]
        public int userId { get; set; }
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        [Display(Name = "Nazwa użytkownika")]
        [StringLength(60, MinimumLength = 3)]
        public string userName { get; set; }
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        [Display(Name = "Hasło")]
        [StringLength(60, MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Display(Name = "Id Profesji")]
        public int professionId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz  hasło")]
        [Compare("password", ErrorMessage = "Hasła nie są takie same")]
        public string ConfirmPassword { get; set; }
    }
}

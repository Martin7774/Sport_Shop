using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sport_Shop.Models;

namespace Sport_Shop.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public int ProfessionId { get; set; }
        public UserLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private bool ValidateUser(SiteUser user)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("Sport_ShopContext");
           
            List<string> hashLogins = new List<string>();
            List<string> hashPasswords = new List<string>();
            List<int> listProfessions = new List<int>();

           
            int professionId = 0;    //Id statusu jaki ma u¿ytkownik
            byte[] _salt = new byte[128 / 8];
            int loginAmount = 0;
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Users";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                hashLogins.Add(reader.GetString(1));
                hashPasswords.Add(reader.GetString(2));
                listProfessions.Add(Int32.Parse(reader["professionId"].ToString()));
                _salt = (byte[])reader["Salt"];
            }
            reader.Close();
            con.Close();


            string userName = user.userName;
           
            string userPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.password,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));
            ProfessionId = professionId;
            
           
            foreach (string hashLogin in hashLogins)
            {
                foreach (string hashPassword in hashPasswords)
                {
                    for(int i=0; i <= loginAmount; i++)
                    {
                        ProfessionId = listProfessions.ElementAt(i); 
                    }
                    if ((userName == hashLogin) && (userPassword == hashPassword))
                    {
                        
                        return true;
                    }
                }
                loginAmount++;
            }
            
            return false;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ValidateUser(user))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.userName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");

                CookieOptions cookieOptions = new CookieOptions();
                Response.Cookies.Append("RoleId", ProfessionId.ToString(), cookieOptions);

               

                switch (ProfessionId)
                {
                    case 1:
                    
                        await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/Admin/adminarea");
                    case 2:
                        await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/Products/Index");
                    case 3:
                        await HttpContext.SignInAsync("CookieAuthentication", new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/Products/ClientIndex");
                }

               
            }
            return Page();
        }

    }
}

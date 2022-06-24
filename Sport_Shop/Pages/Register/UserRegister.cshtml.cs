using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sport_Shop.Models;

namespace Sport_Shop.Pages.Register
{
    public class UserRegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private string lblInfoText;

        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public UserRegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                string myCompanyDBcs = _configuration.GetConnectionString("Sport_ShopContext");
                string login = user.userName;
                string password = user.password;

                // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
                byte[] salt = new byte[128 / 8];


                SqlConnection con = new SqlConnection(myCompanyDBcs);
                string sql = "SELECT salt FROM Users";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    salt = (byte[])reader["salt"];
                }

                reader.Close();
                con.Close();

              
                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));



                string myCompanyDB_connection_string = _configuration.GetConnectionString("Sport_ShopContext");
                SqlConnection con2 = new SqlConnection(myCompanyDB_connection_string);
                
                string sql2 = "insert into Users([name], [password],[salt]) values(@Login, @Password, @Salt)";
                SqlCommand cmd2 = new SqlCommand(sql2, con2);

                try
                {
                    con2.Open();
                 


                    cmd2.Parameters.AddWithValue("@Login", login);
                    cmd2.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd2.Parameters.AddWithValue("@Salt", salt);
                    int numAff = cmd2.ExecuteNonQuery();
                   
                }
                catch (SqlException exc)
                {
                    lblInfoText += string.Format("<b>Blad:</b> {0}<br /><br />", exc.Message);
                }
                finally { con2.Close(); }

                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sport_Shop.Models;

namespace Sport_Shop.Pages.Admin
{
    public class editUserModel : PageModel
    {
        [BindProperty]
        public SiteUser editUser { get; set; }
        public IConfiguration _configuration { get; }
        public string lblInfoText;
        public editUserModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            int id_user = Int32.Parse(HttpContext.Request.Query["id"]);
            string myCompanyDB_connection_string =
           _configuration.GetConnectionString("Sport_ShopContext");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);  //Łączenie z bazą i procedurą
            SqlCommand cmd = new SqlCommand("edit_User", con);
            cmd.CommandType = CommandType.StoredProcedure;


            string login = editUser.userName;
            string password = editUser.password;


            //////////////////////////////////////////////////////////////////////////////////////////////////Pozyskiwanie klucza
            string myCompanyDBcs = _configuration.GetConnectionString("Sport_ShopContext");

            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];

            SqlConnection con2 = new SqlConnection(myCompanyDBcs);
            string sql2 = "SELECT salt FROM Users";
            SqlCommand cmd2 = new SqlCommand(sql2, con2);
            con2.Open();
            SqlDataReader reader = cmd2.ExecuteReader();

            while (reader.Read())
            {
                salt = (byte[])reader["salt"];
            }

            reader.Close();
            con2.Close();
       
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8));




            SqlParameter name_SqlParam = new SqlParameter("@Name", SqlDbType.NVarChar,
           50);
            name_SqlParam.Value = login;     ////////////Przesyłanie shaszowanej nazwy użytkownika
            cmd.Parameters.Add(name_SqlParam);
           





            SqlParameter password_SqlParam = new SqlParameter("@Password", SqlDbType.NVarChar, 50);  //Przesyłanie shaszowanego hasła użytkownika
            password_SqlParam.Value = hashedPassword;
            cmd.Parameters.Add(password_SqlParam);



            SqlParameter professionID_SqlParam = new SqlParameter("@ProfessionId", SqlDbType.Int);  //Przesyłanie ID roli
            professionID_SqlParam.Value = editUser.professionId;
            cmd.Parameters.Add(professionID_SqlParam);



            SqlParameter salt_SqlParam = new SqlParameter("@Salt", SqlDbType.VarBinary, 16);  //Przesyłanie klucza
            salt_SqlParam.Value = salt;
            cmd.Parameters.Add(salt_SqlParam);



            

            SqlParameter UserID_SqlParam = new SqlParameter("@ID",
           SqlDbType.Int);
            UserID_SqlParam.Value = id_user;
            
            cmd.Parameters.Add(UserID_SqlParam);


            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("/Admin/adminarea");
        }
    }
}

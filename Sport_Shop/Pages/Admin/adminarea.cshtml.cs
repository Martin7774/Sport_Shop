using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sport_Shop.Models;

namespace Sport_Shop.Pages.Admin
{
    public class adminareaModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;  
        public List<SiteUser> usersList = new List<SiteUser>();
       
        public IConfiguration _configuration { get; }

        public adminareaModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

       

        public string lblInfoText;

        
        public void OnGet()
        {

            string currentUser = HttpContext.Request.Cookies["RoleId"];
            int int_index = Int32.Parse(currentUser);

            if(int_index!=1)
            {
                RedirectToPage("/Login/UserLogin");
            }
            
            string myCompanyDBcs = _configuration.GetConnectionString("Sport_ShopContext");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Users";
            
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                usersList.Add(new SiteUser
                {
                    userId = Int32.Parse(reader["Id"].ToString()),
                    userName = reader.GetString(1), 
                    
                    professionId = Int32.Parse(reader["professionId"].ToString())
            });
            }
            
            reader.Close();
            con.Close();
            

        }
    }
}

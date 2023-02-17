
using BlazorApp20.Data;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace BlazorApp20.Services
{
    public interface IAccountService
    {
        User User { get; }
        Task<bool> Login(Login model);
        Task Logout();

    }
    public class AccountService : IAccountService
    {
        private readonly IJSRuntime _jsRuntime;

        private readonly IConfiguration _configuration;
        private NavigationManager _navigationManager;
        private string _userKey = "user";

        public User User { get; private set; }

        public AccountService(
            NavigationManager navigationManager, IConfiguration Configuration,
            IJSRuntime jsRuntime


        )
        {
            _navigationManager = navigationManager;
            _configuration = Configuration;
            _jsRuntime = jsRuntime;
        }
        public async Task<bool> Login(Login model)
        {
            bool isValid = false;
            User obj = new User();
            string constr = string.Empty;

              constr = _configuration.GetConnectionString("WebConnection");
         //   if (AppDomain.CurrentDomain.FriendlyName == "WpfApp20")
           // {
            //    constr = _configuration.GetConnectionString("WpfConnection");
            //}
           // else
           // {
            //    constr = _configuration.GetConnectionString("WebConnection");
           // }
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT UserName FROM AspNetUsers where username='" + model.Username + "'";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            isValid = true;
                        }
                    }
                    con.Close();
                }
            }
            return isValid;

        }

        public async Task Logout()
        {
            User = null;
            _navigationManager.NavigateTo("account/login");
        }


    }
}
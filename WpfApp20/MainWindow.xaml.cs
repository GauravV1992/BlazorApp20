using BlazorApp20.Data;
using BlazorApp20.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorClassLibrary20;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;
using Microsoft.AspNetCore.Builder;
using System.Net.NetworkInformation;

namespace WpfApp20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppState _appState = new();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(
            //    owner: this,
            //    messageBoxText: $"Current counter value is: {_appState.Counter}",
            //    caption: "Counter");

            Login mainWindow = new Login();
            mainWindow.Show();
            this.Close();
        }
        public IConfiguration _configuration { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            //lblEmail.Content = UserInfo.CustomerEmail;
            //lblName.Content = UserInfo.CustomerName;

            //serviceCollection.AddBlazorWebView();
            serviceCollection.AddWpfBlazorWebView();

            serviceCollection.AddSingleton<WeatherForecastService>();

            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            serviceCollection.AddSingleton<IConfiguration>(_configuration);
            serviceCollection.AddAuthorizationCore();
            serviceCollection.AddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();
            serviceCollection.AddSingleton<AuthenticatedUser>();
            serviceCollection.AddScoped<HttpClient>();
            serviceCollection.AddSingleton<AppState>(_appState);
            serviceCollection.AddScoped<IAccountService, AccountService>();
            //serviceCollection.AddSingleton<AuthenticatedUser>();
            serviceCollection.AddBlazorWebViewDeveloperTools();
            // Add services to the container.
            //serviceCollection.AddControllersWithViews();
            //serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(options =>
            //{
            //    options.Cookie.Name = "myauth";
            //    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
            //});
            var services = serviceCollection.BuildServiceProvider();

            Resources.Add("services", services);
        }

    }

}

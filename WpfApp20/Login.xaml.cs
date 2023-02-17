using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp20
{
    public class UserInfo
    {
        public static String CustomerName = "";
        public static String CustomerEmail = "";
    }
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        string connectionString;
        public IConfiguration _configuration { get; set; }
        public Login()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            connectionString = _configuration.GetConnectionString("WebConnection");
        }
        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            String message = "Invalid Credentials";
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("Select * from AspNetUsers where Email=@CustomerEmail", con);
                cmd.Parameters.AddWithValue("@CustomerEmail", txtUserId.Text.ToString());
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                        message = "1";
                        UserInfo.CustomerEmail = txtUserId.Text.ToString();
                }
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
            }
            if (message == "1")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
                MessageBox.Show(message, "Info");
        }

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

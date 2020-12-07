using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DatabaseConnection;

namespace Store
{
    /// <summary>
    /// Interaction logic for Create_account.xaml
    /// </summary>
    public partial class Create_account : Window
    {
        public Create_account()
        {
            InitializeComponent();
        }

        private void Login_page(object sender, RoutedEventArgs e)
        {
            var next_window = new LoginWindow();
            next_window.Show();
            this.Close();
        }

        private void Create_account_click(object sender, RoutedEventArgs e)
        {
            string full_name = name_field.Text.Trim();
            string email = email_field.Text.Trim();
            string password = password_field.Password;
            string password_check = confirm_password_field.Password;
            if(password != password_check)
            {
                MessageBox.Show("Your passwords doesn't match");
            }
            else
            {
                bool test = API.Create_account(full_name, email, password);
                if (test)
                {
                    MessageBox.Show("Account successfully created!");
                    var next_window = new LoginWindow();
                    next_window.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Email or password, already in use!");
                }
            }
        }
    }
}

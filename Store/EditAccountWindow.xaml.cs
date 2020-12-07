using DatabaseConnection;
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
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Store
{
    /// <summary>
    /// Interaction logic for EditAccountWindow.xaml
    /// </summary>
    public partial class EditAccountWindow : Window
    {
        public EditAccountWindow()
        {
            InitializeComponent();
            Name_field.Text = State.User.Name;
            Email_field.Text = State.User.Email;
        }

        private void Edit_account_Click(object sender, RoutedEventArgs e)
        {
            if (Old_password_field.Password == State.User.PassWord)
            {
                if (New_password_field.Password == Confirm_new_password_field.Password)
                {
                    State.User.PassWord = Confirm_new_password_field.Password;
                    API.ctx.Customers.Update(State.User);
                    API.ctx.SaveChanges();
                }
            }
        }
    }
}

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
            bool changed = false;
            if (Old_password_field.Password == State.User.PassWord)
            {
                if (New_password_field.Password == Confirm_new_password_field.Password)
                {
                    State.User.PassWord = Confirm_new_password_field.Password;
                    API.ctx.SaveChanges();
                    changed = true;
                }
                else
                {
                    MessageBox.Show("Passwords didn't match");
                }
            }

            var name = State.User.Name;
            if (Name_field.Text != name)
            {
                State.User.Name = Name_field.Text;
                API.ctx.SaveChanges();
                changed = true;
            }
            var Email = State.User.Email;
            if (Email_field.Text != name)
            {
                try
                {
                    State.User.Email = Email_field.Text;
                    API.ctx.SaveChanges();
                    changed = true;
                }
                catch
                {
                    MessageBox.Show("Email already in use");
                }

            }

            if (changed)
            {
                MessageBox.Show("Information successfully changed");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserWindow objUserWindow = new UserWindow();
            objUserWindow.Show();
            this.Close();
        }
    }
}

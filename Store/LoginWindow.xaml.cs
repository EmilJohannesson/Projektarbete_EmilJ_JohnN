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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            // Använder min namngedda TextBox objektinstans för att få tag på
            // det användaren skrev in.
            string username_in_text = NameField.Text.Trim();

            string password_in_text = PasswordField.Password;

            // Kallar på min databas api för att få ut ett record som matchar namnet.
            State.User = API.GetCustomerByName(username_in_text);

            State.Password = API.GetPasswordByName(password_in_text);

            // Om ett record hittades...
            if (State.User != null & State.Password != null )
            {
                // Skapa och visa nästa fönster.
                var next_window = new MainWindow();
                next_window.Show();

                // Stäng ner nuvarande login fönster
                this.Close();
            }
            else
            {
                // Annars återställer vi innehållet i TextBox
                NameField.Text = "...";
            }
        }
    }
}

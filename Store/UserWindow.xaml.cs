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
using System.Linq;

namespace Store
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            UserLabel.Content = State.User.Name;

            for (int i = 0; i < State.User.Sales.Count; i++)
            {
                Rental rentals = State.User.Sales[i];
                var label = new Label();
                label.Content = rentals.Movie.Title;
                Grid.SetRow(label, i);
                RentedMoviesGrid.Children.Add(label);
            }
        }
    }
}

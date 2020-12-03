using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatabaseConnection;

namespace Store
{
    /// <summary>
    /// Interaction logic for Sort_function_abc.xaml
    /// </summary>
    public partial class Sort_function_abc : Window
    {
        public Sort_function_abc()
        {
            InitializeComponent();

            int movie_skip_count = 0;
            int movie_take_count = 30;
            State.Movies = API.GetMovieSliceAlpha(movie_skip_count, movie_take_count);

            int column_count = MovieGrid.ColumnDefinitions.Count;

            /*
             * cols = 3, movs = 10
             * 
             * rows = movs/cols = 3.333
             * 
             * vi behöver alltså 4 rader. Vi kan inte bara göra en vanlig heltalsdivision.
             * 
             * rows = ceiling(movs/cols) = 4
             */
            int row_count = (int)Math.Ceiling((double)State.Movies.Count / (double)column_count);

            for (int y = 0; y < (row_count * 2); y++)
            {
                // Skapa en rad-definition för att bestämma hur hög just denna raden är.
                MovieGrid.RowDefinitions.Add(
                    new RowDefinition()
                    {
                        Height = new GridLength(300, GridUnitType.Pixel)
                    });

                // Lägga till en film i varje cell för en rad
                for (int x = 0; x < column_count; x++)
                {
                    // Räkna ut vilken film vi ska ploppa in härnäst utifrån mina x,y koordinater
                    int i = (y / 2) * column_count + x;
                    // Kolla så att vi inte försöker fylla mer Grid celler än vi har filmrecords.
                    if (i < State.Movies.Count)
                    {
                        // Hämta ett film record
                        var movie = State.Movies[i];

                        // Försök att skapa en Image Controller(legobit) och
                        // placera den i rätt Grid cell enl. x,y koordinaterna
                        // Skapa en Image som visar filmomslaget
                        var image = new Image()
                        {
                            Cursor = Cursors.Hand, // Visa en 'click me' hand när man hovrar över bilden
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(4, 4, 4, 4),
                        };
                        image.MouseUp += Image_MouseUp;

                        try
                        {
                            image.Source = new BitmapImage(new Uri(movie.ImageURL)); // Hämta hem bildlänken till RAM
                        }
                        catch (Exception e) when
                            (e is ArgumentNullException ||
                             e is System.IO.FileNotFoundException ||
                             e is UriFormatException)
                        {
                            // Om något gick fel så lägger vi in en placeholder 
                            image.Source = new BitmapImage(new Uri("https://wolper.com.au/wp-content/uploads/2017/10/image-placeholder.jpg"));
                        }

                        // Lägg till Image i Grid
                        MovieGrid.Children.Add(image);

                        // Placera in Image i Grid
                        Grid.SetRow(image, y);
                        Grid.SetColumn(image, x);
                    }
                }
                MovieGrid.RowDefinitions.Add(
                    new RowDefinition()
                    {
                        Height = new GridLength(50, GridUnitType.Pixel)
                    });
                for (int i = 0; i < column_count; i++)
                {
                    int x = (y / 2) * column_count + i;
                    var test = State.Movies[x];
                    var label = new Label();
                    label.Content = test.Title + " " + test.Score;
                    Grid.SetRow(label, y + 1);
                    Grid.SetColumn(label, i);
                    MovieGrid.Children.Add(label);
                }
                y++;
            }
        }

        // Vad som händer när man klickar på en filmikon i appen.
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Ta reda på vilken koordinat den klickade bilden har.
            var x = Grid.GetColumn(sender as UIElement);
            var y = Grid.GetRow(sender as UIElement);

            // Används koordinaten för att ta reda på vilken motsvarande record det rörde sig om.
            int i = y * MovieGrid.ColumnDefinitions.Count + x;
            // Lägg valet på minne.
            State.Pick = State.Movies[i];

            // Försök att registrera en uthyrning.
            if (API.RegisterSale(State.User, State.Pick))
                // MessageBox är små pop-up fönster som är behändiga för att varna användaren om fel etc.
                MessageBox.Show("All is well and you can download your movie now.", "Sale Succeeded!", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("An error happened while buying the movie, please try again at a later time.", "Sale Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void UserWindowButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow objUserWindow = new UserWindow();
            objUserWindow.Show();
        }

        private void Sort_by_name_Click(object sender, RoutedEventArgs e)
        {
            Sort_function_abc sorter = new Sort_function_abc();
            sorter.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var next_window = new MainWindow();
            next_window.Show();
            this.Close();
        }
    }
}


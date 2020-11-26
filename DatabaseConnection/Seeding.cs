using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DatabaseConnection
{
    class Seeding
    {
        static void Main() 
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            using (var ctx = new Context())
            {
                ctx.RemoveRange(ctx.Sales);
                ctx.RemoveRange(ctx.Movies);
                ctx.RemoveRange(ctx.Customers);

                ctx.AddRange(new List<Customer> {
                    new Customer { Name = "Björn", Email = "Björn@mail.com", PassWord = "hunter1" },
                    new Customer { Name = "Robin", Email = "Robin@mail.com", PassWord = "hunter2" },
                    new Customer { Name = "Kalle", Email = "Kalle@mail.com", PassWord = "hunter3" },
                    new Customer { Name = "John",  Email = "John@mail.com",  PassWord = "hunter4" },
                    new Customer { Name = "Emil",  Email = "Emil@mail.com",  PassWord = "hunter5" }

                });

                // Här laddas data in från SeedData foldern för att fylla ut Movies tabellen
                var movies = new List<Movie>();
                var lines = File.ReadAllLines(@"..\..\..\SeedData\MovieGenre.csv");
                for (int i = 1; i < 200; i++)
                {
                    // imdbId,Imdb Link,Title,IMDB Score,Genre,Poster
                    var cells = lines[i].Split(',');

                    var url = cells[5].Trim('"');

                    string scoreString = cells[3];
                    double score = double.Parse(scoreString);

                    // Hoppa över alla icke-fungerande url:er
                    try{ var test = new Uri(url); }
                    catch (Exception) { continue; }

                    movies.Add(new Movie { Title = cells[2], Score = score, ImageURL = url });
                }
                ctx.AddRange(movies);

                ctx.SaveChanges();
            }
        }
    }
}

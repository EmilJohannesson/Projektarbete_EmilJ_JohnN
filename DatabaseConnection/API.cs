﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    public static class API
    {
        // Här har jag ett kontext tillgängligt för alla API metoder.
        public static Context ctx;

        // Statiska konstruktorer kallas på innan den statiska klassen används.
        static API()
        {
            ctx = new Context();
        }
        public static List<Movie> GetMovieSlice(int skip_x, int take_x)
        {
            return ctx.Movies
                .OrderBy(m => m.Title)
                .Skip(skip_x)
                .Take(take_x)
                .ToList();
        }
        public static List<Movie> GetMovieSliceAlpha(int skip_x, int take_x)
        {
            return ctx.Movies
                .OrderByDescending(m => m.Score)
                .Skip(skip_x)
                .Take(take_x)
                .ToList();
        }
        public static Customer GetCustomerByEmail(string name)
        {
            return ctx.Customers
                .FirstOrDefault(c => c.Email.ToLower() == name.ToLower());
        }
        public static bool GetPasswordByEmail(string password, string username) // Metod för att kolla password
        {
            var password_check = "";
            try
            {
                password_check = ctx.Customers.Where(c => c.Email == username).Single().PassWord; // hämta password från Databasen
            }
            catch
            {
                return false;
            }

            if (Convert.ToString(password_check) == password) // Kolla om angivet password stämmer enligt Databasen
            {
                return true;
            }
            else return false;
        }
        public static bool RegisterSale(Customer customer, Movie movie)
        {
            // Försök att lägga till ett nytt sales record
            try
            {
                ctx.Add(new Rental() { Date = DateTime.Now, ReturnDate = DateTime.Now.AddDays(7), Customer = customer, Movie = movie });

                bool one_record_added = ctx.SaveChanges() == 1;
                return one_record_added;
            }
            catch(DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                return false;
            }
        }
        public static bool Create_account(string Name, string email, string password)
        {
            try
            {
                using (var ctx = new Context())
                {
                    ctx.AddRange(new List<Customer> {
                    new Customer { Name = Name, Email = email, PassWord = password }
                    });

                    ctx.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

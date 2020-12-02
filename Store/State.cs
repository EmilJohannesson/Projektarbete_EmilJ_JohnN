using System;
using System.Collections.Generic;
using System.Text;
using DatabaseConnection;
using Microsoft.EntityFrameworkCore;

namespace Store
{
    /* En statisk klass som kan hålla koll på data medans jag växlar programfönster.
     * Eftersom den ligger utanför fönsterklasserna så kan båda komma åt den närsomhelst.
     */
    static class State
    {
        public static Customer User { get; set; } // Användaren som loggat in
        public static Customer Password { get; set; }
        public static List<Movie> Movies { get; set; } // Filmer jag hämtat från databasen
        public static Movie Pick { get; set; } // Film som användaren valt att hyra
        public static Rental Rental { get; set; } // Hyrdatum
    }
}

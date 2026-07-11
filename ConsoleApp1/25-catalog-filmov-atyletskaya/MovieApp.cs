using System;
using System.Collections.Generic;
using System.Text;

namespace _25_catalog_filmov_atyletskaya
{
    internal class MovieApp
    {
        public MovieApp()
        {
            Console.WriteLine("===Movies catalogue===");
            Console.WriteLine("1. Add new movie");
            Console.WriteLine("2. Rate a move");
            Console.WriteLine("3. All movies");
            Console.WriteLine("4. Search by name");
            Console.WriteLine("5. Search by genre");
            Console.WriteLine("6. Top-5");
            Console.WriteLine("7. Delete a movie");
            Console.WriteLine("0. Leave");

            Console.ReadLine();
        }
    }
}

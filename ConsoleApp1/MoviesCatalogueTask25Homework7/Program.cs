using System;
using System.Numerics;

namespace MoviesCatalogueTask25Homework7
{
    internal class Program
    {
        static void Main(string[] args)

        {
            while (true)
            {
                var option = 0;
                bool isOptionCorrect;

                Console.WriteLine("=== Movie Catalog ===");
                Console.WriteLine("Please enter a number to proceed");
                Console.WriteLine("1. Add Movie");
                Console.WriteLine("2. Rate Movie");
                Console.WriteLine("3. Show All Movies");
                Console.WriteLine("4. Search by Title");
                Console.WriteLine("5. Filter by Genre");
                Console.WriteLine("6. Top 5 Movies");
                Console.WriteLine("7. Delete Movie");
                Console.WriteLine("0. Exit");

                var selectedOption = Console.ReadLine();

                isOptionCorrect = int.TryParse(selectedOption, out option);

                switch (option)
                {
                    case 1: Console.WriteLine("Enter data to add a new movie"); break;
                    case 2: Console.WriteLine("Select a movie to rate"); break;
                    case 3: Console.WriteLine("All movies"); break;
                    case 4: Console.WriteLine("Enter a name to search"); break;
                    case 5: Console.WriteLine("Select a genre"); break;
                    case 6: Console.WriteLine("Here's a top-5"); break;
                    case 7: Console.WriteLine("Select a movie you'd like to delete"); break;
                    case 0: return;
                    default: Console.WriteLine("Invalid option. Please try again."); break; // не работает, если ввести букву, но работает, если ввести число, которого нет в списке
                }
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalogueTask25Homework7
{
    public class MovieApp
    {
        private MovieCatalog _catalog = new MovieCatalog();
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("=== Movie Catalog ===");
                Console.WriteLine("1. Add new movie");
                Console.WriteLine("2. Rate movie");
                Console.WriteLine("3. Show all movies");
                Console.WriteLine("4. Search by title");
                Console.WriteLine("5. Filter by genre");
                Console.WriteLine("6. Top 5");
                Console.WriteLine("7. Delete movie");
                Console.WriteLine("0. Exit");

                if (!int.TryParse(Console.ReadLine(), out int option) || option < 0 || option > 7)
                {
                    Console.WriteLine("Invalid option. Try again.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        AddMovie();
                        break;
                    case 0:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Not implemented yet.");
                        break;
                }
            }
        }

        // Вспомогательный метод меню для сбора данных с консоли
        private void AddMovie()
        {
            Console.WriteLine("--- Add a New Movie ---");

            Console.Write("Enter movie title");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) return;

            Console.Write("Enter movie release year");
            if (!int.TryParse(Console.ReadLine(), out int year)) return;

            Console.WriteLine("Select a genre");
            foreach (var genreValue in Enum.GetValues(typeof(MovieGenreList)))
            {
                Console.WriteLine($"{(int)genreValue}. {genreValue}");
            }
            Console.Write("Enter genre number: ");
            if (!int.TryParse(Console.ReadLine(), out int genreNumber) || !Enum.IsDefined(typeof(MovieGenreList), genreNumber)) return;

            MovieGenreList selectedGenre = (MovieGenreList)genreNumber;

            // СОЗДАЕМ ОБЪЕКТ И ОТДАЕМ ЕГО В КАТАЛОГ (как требует ТЗ!)
            Movie newMovie = new Movie(title, year, selectedGenre);
            _catalog.Add(newMovie);

            Console.WriteLine($"\nSuccess: Movie \"{title}\" added to catalog!");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalogueTask25Homework7
{
    public class MovieApp
    {
        private MovieCatalogue _catalog = new MovieCatalogue();
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("=== Movie Catalogue ===");
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
                    case 3:
                        ShowAllMovies();
                        break;
                    case 0:
                        Console.WriteLine("We don't have a db now, so say goodbye to your data");
                        return;
                    default:
                        Console.WriteLine("Not implemented yet.");
                        break;
                }
            }
        }

        private void AddMovie()
        {
            Console.WriteLine("=== Add new movie ===");

            var movieTitle = string.Empty;
            while (string.IsNullOrWhiteSpace(movieTitle))
            {
                Console.Write("Enter movie title:");
                movieTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(movieTitle))
                {
                    Console.WriteLine("Error: Title cannot be empty. Please try again.");
                }
            }

            var movieYear = 0;
            while (true)
            {
                Console.Write("Enter movie release year (e.g. 2010):");
                var yearInput = Console.ReadLine();

                if (int.TryParse(yearInput, out var parsedYear) && parsedYear >= 1900 && parsedYear <= 2050)
                {
                    movieYear = parsedYear;
                    break;
                }
                Console.WriteLine("Error: Invalid input. Please enter a valid four-digit year (1990-2050).");
            }

            Console.WriteLine("Select a genre:");
            foreach (var genreValue in Enum.GetValues(typeof(MovieGenreList)))
            {
                Console.WriteLine($"{(int)genreValue}. {genreValue}");
            }

            var genreInputNumber = 0;
            while (true)
            {
                Console.Write("Enter genre number:");
                var genreInput = Console.ReadLine();

                if (int.TryParse(genreInput, out var parsedGenre) && Enum.IsDefined(typeof(MovieGenreList), parsedGenre))
                {
                    genreInputNumber = parsedGenre;
                    break;
                }
                Console.WriteLine("Error: Invalid genre selection. Please choose a number from the list.");
            }

            var movieGenre = (MovieGenreList)genreInputNumber;

            var newMovie = new Movie(movieTitle, movieYear, movieGenre);
            _catalog.Add(newMovie);

            Console.WriteLine($"Movie \"{movieTitle}\" ({movieYear}) added to catalogue");
        }

        private void ShowAllMovies()
        {
            Console.WriteLine("=== All Movies ===");

            // Запрашиваем список фильмов у каталога
            var moviesList = _catalog.GetAll();

            // Проверяем: если фильмов еще нет, то и выводить нечего
            if (moviesList.Count == 0)
            {
                Console.WriteLine("There's no movies yet");
                return;
            }

            // Бежим циклом по каждому фильму и выводим его свойства на экран
            foreach (var movie in moviesList)
            {
                // Проверяем рейтинг: если он null, пишем "N/A", иначе выводим число
                string ratingDisplay = movie.Rating.HasValue ? $"{movie.Rating.Value}/10" : "N/A";

                Console.WriteLine($"- \"{movie.Title}\" ({movie.Year}) | Genre: {movie.Genre} | Rating: {ratingDisplay}");
            }
        }
    }
}
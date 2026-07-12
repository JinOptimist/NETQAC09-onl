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
                Console.WriteLine("7. Delete a movie");
                Console.WriteLine("0. Exit");

                if (!int.TryParse(Console.ReadLine(), out var option) || option < 0 || option > 7)
                {
                    Console.WriteLine("Invalid option");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        AddMovie();
                        break;
                    case 2:
                        RateMovie();
                        break;
                    case 3:
                        ShowAllMovies();
                        break;
                    case 4:
                        SearchByTitle();
                        break;
                    case 5:
                        FilterByGenre();
                        break;
                    case 7:
                        DeleteMovie(); 
                        break;
                    case 0:
                        Console.WriteLine("We don't have a db now, so say goodbye to your data");
                        return;
                    default:
                        Console.WriteLine("Not implemented yet");
                        break;
                }
            }
        }

        private void AddMovie() //метод добавления фильма
        {
            Console.Clear();
            Console.WriteLine("=== Add new movie ===");

            //ввоод названия фильма с проверкой на пустую строку
            var movieTitle = string.Empty;
            while (string.IsNullOrWhiteSpace(movieTitle))
            {
                Console.Write("Enter movie title:");
                movieTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(movieTitle))
                {
                    Console.WriteLine("Title cannot be empty");
                }
            }

            //ввод года выпуска фильма с проверкой на корректность в заданном интервале
            var movieYear = 0;
            var minYear = 1900;
            var maxYear = 2050;
            while (true)
            {
                Console.Write($"Enter movie release year between {minYear} - {maxYear}:");
                var yearInput = Console.ReadLine();

                if (int.TryParse(yearInput, out var parsedYear) && parsedYear >= minYear && parsedYear <= maxYear)
                {
                    movieYear = parsedYear;
                    break;
                }
                Console.WriteLine($"Please enter a four-digit year ({minYear} - {maxYear})");
            }


            Console.WriteLine("Please select a genre from the list below:");
            foreach (var genreValue in Enum.GetValues(typeof(MovieGenreList)))
            {
                Console.WriteLine($"{(int)genreValue}. {genreValue}");
            }

            var genreInputNumber = 0;
            while (true)
            {
                var genreInput = Console.ReadLine();

                if (int.TryParse(genreInput, out var parsedGenre) && Enum.IsDefined(typeof(MovieGenreList), parsedGenre))
                {
                    genreInputNumber = parsedGenre;
                    break;
                }
                Console.WriteLine("Please choose a number from the list");
            }

            var movieGenre = (MovieGenreList)genreInputNumber;

            var newMovie = new Movie(movieTitle, movieYear, movieGenre);
            _catalog.Add(newMovie);

            Console.WriteLine($"Movie \"{movieTitle}\" ({movieYear}) added to catalogue");
        }

        private void ShowAllMovies()
        {
            Console.Clear();
            Console.WriteLine("=== All Movies ===");

            var moviesList = _catalog.GetAll();

            if (moviesList.Count == 0)
            {
                Console.WriteLine("There's no movies yet");
                return;
            }

            foreach (var movie in moviesList)
            {
                var movieRating = movie.Rating.HasValue ? $"{movie.Rating.Value}/10" : "N/A";

                Console.WriteLine($"- \"{movie.Title}\" ({movie.Year}) | Genre: {movie.Genre} | Rating: {movieRating}");
            }
        }

        private void DeleteMovie()
        {
            Console.Clear();
            Console.WriteLine("=== Delete a movie ===");

            var moviesList = _catalog.GetAll();
            if (moviesList.Count == 0)
            {
                Console.WriteLine("No movies to delete");
                return;
            }

            Console.WriteLine("Select the index of the movie you want to delete:");
            for (var i = 0; i < moviesList.Count; i++)
            {
                Console.WriteLine($"{i}. \"{moviesList[i].Title}\" ({moviesList[i].Year})");
            }

            var selectedMovieNumberToDelete = 0;
            while (true)
            {
                Console.WriteLine("Enter movie index to delete:");
                var indexInput = Console.ReadLine();

                if (int.TryParse(indexInput, out var parsedIndex) && parsedIndex >= 0 && parsedIndex < moviesList.Count)
                {
                    selectedMovieNumberToDelete = parsedIndex;
                    break;
                }
                Console.WriteLine($"Error: Invalid selection. Please enter a number between 0 and {moviesList.Count - 1}");
            }

            var selectedMovieToDelete = moviesList[selectedMovieNumberToDelete].Title;

            var isDeleted = _catalog.Delete(selectedMovieNumberToDelete);

            if (isDeleted)
            {
                Console.WriteLine($"Movie \"{selectedMovieToDelete}\" has been removed");
            }
            else
            {
                Console.WriteLine("Could not delete the movie");
            }
        }
        private void FilterByGenre()
        {
            Console.Clear();
            Console.WriteLine("=== Filter by Genre ===");

            var allMovies = _catalog.GetAll();
            if (allMovies.Count == 0)
            {
                Console.WriteLine("No movies to filter");
                return;
            }

            Console.WriteLine("Select a genre to filter by:");
            foreach (var genreValue in Enum.GetValues(typeof(MovieGenreList)))
            {
                Console.WriteLine($"{(int)genreValue}. {genreValue}");
            }

            var genreInputNumber = 0;
            while (true)
            {
                Console.WriteLine("Enter genre number:");
                var genreInput = Console.ReadLine();

                if (int.TryParse(genreInput, out var parsedGenre) && Enum.IsDefined(typeof(MovieGenreList), parsedGenre))
                {
                    genreInputNumber = parsedGenre;
                    break;
                }
                Console.WriteLine("Please choose a number from the list");
            }

            var selectedGenre = (MovieGenreList)genreInputNumber;

            var filteredMovies = _catalog.GetByGenre(selectedGenre);

            Console.WriteLine($"Movies in genre: {selectedGenre}");
            if (filteredMovies.Count == 0)
            {
                Console.WriteLine($"No movies found in {selectedGenre} genre.");
                return;
            }

            foreach (var movie in filteredMovies)
            {
                var ratingDisplay = movie.Rating.HasValue ? $"{movie.Rating.Value}/10" : "N/A";
                Console.WriteLine($"- \"{movie.Title}\" ({movie.Year}) | Rating: {ratingDisplay}");
            }
        }

        private void RateMovie()
        {
            Console.Clear();
            Console.WriteLine("=== Rate a Movie ===");

            var moviesList = _catalog.GetAll();
            if (moviesList.Count == 0)
            {
                Console.WriteLine("The catalog is empty. Nothing to rate");
                return;
            }

            Console.WriteLine("Select the index of the movie you want to rate:");
            for (var i = 0; i < moviesList.Count; i++)
            {
                var currentRating = moviesList[i].Rating.HasValue ? $"{moviesList[i].Rating.Value}/10" : "No rating yet";
                Console.WriteLine($"{i}. \"{moviesList[i].Title}\" (Current rating: {currentRating})");
            }

            var selectedIndex = 0;
            while (true)
            {
                Console.Write("\nEnter movie index:");
                var indexInput = Console.ReadLine();

                if (int.TryParse(indexInput, out var parsedIndex) && parsedIndex >= 0 && parsedIndex < moviesList.Count)
                {
                    selectedIndex = parsedIndex;
                    break;
                }
                Console.WriteLine($"Please enter a number between 0 and {moviesList.Count - 1}");
            }

            var movieRating = 0;
            while (true)
            {
                Console.Write("Enter your rating (from 1 to 10):");
                var ratingInput = Console.ReadLine();

                if (int.TryParse(ratingInput, out var parsedRating) && parsedRating >= 1.0 && parsedRating <= 10.0)
                {
                    movieRating = parsedRating;
                    break;
                }
                Console.WriteLine("Please enter a number between 1 and 10");
            }

            var targetMovieTitle = moviesList[selectedIndex].Title;

            var isRated = _catalog.RateMovie(selectedIndex, movieRating);

            if (isRated)
            {
                Console.WriteLine($"Movie \"{targetMovieTitle}\" has been rated {movieRating}/10");
            }
            else
            {
                Console.WriteLine("Could not apply rating");
            }
        }

        private void SearchByTitle()
        {
            Console.Clear();
            Console.WriteLine("=== Search by title ===");

            var allMovies = _catalog.GetAll();
            if (allMovies.Count == 0)
            {
                Console.WriteLine("The catalog is empty. Nothing to search");
                return;
            }

            var searchKeyword = string.Empty;
            while (string.IsNullOrWhiteSpace(searchKeyword))
            {
                Console.Write("Enter movie title or part of it:");
                searchKeyword = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(searchKeyword))
                {
                    Console.WriteLine("Search cannot be empty");
                }
            }

            var foundMovies = _catalog.SearchByTitle(searchKeyword);

            Console.WriteLine($"Search results for \"{searchKeyword}\"");
            if (foundMovies.Count == 0)
            {
                Console.WriteLine("No movies found matching your search");
                return;
            }

            foreach (var movie in foundMovies)
            {
                var ratingDisplay = movie.Rating.HasValue ? $"{movie.Rating.Value}/10" : "N/A";
                Console.WriteLine($"- \"{movie.Title}\" ({movie.Year}) | Genre: {movie.Genre} | Rating: {ratingDisplay}");
            }
        }
    }
}
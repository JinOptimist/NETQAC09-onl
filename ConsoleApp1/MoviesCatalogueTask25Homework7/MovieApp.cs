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
                Console.WriteLine("6. Top movies");
                Console.WriteLine("7. Delete a movie");
                Console.WriteLine("0. Exit");

                if (!int.TryParse(Console.ReadLine(), out var userMenuInput) || !Enum.IsDefined(typeof(MenuOption), userMenuInput))
                {
                    Console.WriteLine("Invalid option");
                    continue;
                }

                var option = (MenuOption)userMenuInput;

                switch (option)
                {
                    case MenuOption.AddMovie:
                        AddMovie();
                        break;
                    case MenuOption.RateMovie:
                        RateMovie();
                        break;
                    case MenuOption.ShowAllMovies:
                        ShowAllMovies();
                        break;
                    case MenuOption.SearchByTitle:
                        SearchByTitle();
                        break;
                    case MenuOption.FilterByGenre:
                        FilterByGenre();
                        break;
                    case MenuOption.ShowTopMovies:
                        ShowTopMovies();
                        break;
                    case MenuOption.DeleteMovie:
                        DeleteMovie();
                        break;
                    case MenuOption.Exit:
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

            //вывод списка жанров и выбор жанра с проверкой на корректность ввода
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

        private void ShowAllMovies() //метод отображения всех фильмов
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
                var ratingDisplay = movie.Rating.HasValue ? $"{movie.Rating.Value}/10" : "N/A";
                Console.WriteLine($"- \"{movie.Title}\" ({movie.Year}) | Genre: {movie.Genre} | Rating: {ratingDisplay}");
            }
        }

        private void DeleteMovie() //метод удаления фильма
        {
            Console.Clear();
            Console.WriteLine("=== Delete a movie ===");

            var moviesList = _catalog.GetAll();
            if (moviesList.Count == 0)
            {
                Console.WriteLine("No movies to delete");
                return;
            }

            //удаление фильма по индексу с проверкой на корректность ввода
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
                Console.WriteLine($"Please enter a number between 0 and {moviesList.Count - 1}");
            }

            var selectedMovieToDelete = moviesList[selectedMovieNumberToDelete].Title;

            var isDeleted = _catalog.Delete(selectedMovieNumberToDelete);

            //вывод сообщения об успешном удалении или ошибке
            if (isDeleted)
            {
                Console.WriteLine($"Movie \"{selectedMovieToDelete}\" has been removed");
            }
            else
            {
                Console.WriteLine("Could not delete the movie");
            }
        }

        private void FilterByGenre() //метод фильтрации фильмов по жанру
        {
            Console.Clear();
            Console.WriteLine("=== Filter by genre ===");

            var allMovies = _catalog.GetAll();
            if (allMovies.Count == 0)
            {
                Console.WriteLine("No movies to filter");
                return;
            }

            //вывод списка жанров и выбор жанра с проверкой на корректность ввода
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

            //фильтрация фильмов по выбранному жанру и вывод результатов
            var selectedGenre = (MovieGenreList)genreInputNumber;

            var filteredMovies = _catalog.GetByGenre(selectedGenre);

            Console.WriteLine($"Movies in genre: {selectedGenre}");
            if (filteredMovies.Count == 0)
            {
                Console.WriteLine($"No movies found in {selectedGenre} genre");
                return;
            }

            foreach (var movie in filteredMovies)
            {
                var ratingDisplay = movie.Rating.HasValue ? $"{movie.Rating.Value}/10" : "N/A";
                Console.WriteLine($"- \"{movie.Title}\" ({movie.Year}) | Rating: {ratingDisplay}");
            }
        }

        private void RateMovie() //метод оценки фильма
        {
            Console.Clear();
            Console.WriteLine("=== Rate a Movie ===");

            var moviesList = _catalog.GetAll();
            if (moviesList.Count == 0)
            {
                Console.WriteLine("The catalog is empty. Nothing to rate");
                return;
            }

            //вывод списка фильмов с текущим рейтингом и выбор фильма по индексу с проверкой на корректность ввода
            Console.WriteLine("Select the index of the movie you want to rate:");
            for (var i = 0; i < moviesList.Count; i++)
            {
                var ratingDisplay = moviesList[i].Rating.HasValue ? $"{moviesList[i].Rating.Value}/10" : "N/A";
                Console.WriteLine($"{i}. \"{moviesList[i].Title}\" (Current rating: {ratingDisplay})");
            }

            var selectedIndex = 0;
            while (true)
            {
                Console.Write("Enter movie index:");
                var indexInput = Console.ReadLine();

                if (int.TryParse(indexInput, out var parsedIndex) && parsedIndex >= 0 && parsedIndex < moviesList.Count)
                {
                    selectedIndex = parsedIndex;
                    break;
                }
                Console.WriteLine($"Please enter a number between 0 and {moviesList.Count - 1}");
            }

            //ввод рейтинга фильма с проверкой на корректность ввода
            var movieRating = 0;
            while (true)
            {
                Console.Write("Enter your rating (from 1 to 10):");
                var ratingInput = Console.ReadLine();

                if (int.TryParse(ratingInput, out var parsedRating) && parsedRating >= 1 && parsedRating <= 10)
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

        private void SearchByTitle() //метод поиска фильма по названию
        {
            Console.Clear();
            Console.WriteLine("=== Search by title ===");

            var allMovies = _catalog.GetAll();
            if (allMovies.Count == 0)
            {
                Console.WriteLine("The catalog is empty. Nothing to search");
                return;
            }

            //ввод ключевого слова для поиска с проверкой на пустую строку
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

            //поиск фильмов по ключевому слову и вывод результатов
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

        private void ShowTopMovies() //метод отображения топ 5 фильмов по рейтингу
        {
            var topCount = 5;
            Console.Clear();
            Console.WriteLine($"=== Top {topCount} ===");

            var topMovies = _catalog.GetTopMovies(topCount);

            if (topMovies.Count == 0)
            {
                Console.WriteLine("No rated movies found");
                return;
            }

            Console.WriteLine($"Here are the highest rated movies (if there're more than 5 highest rated, the first rated will be shown):");
            var position = 1;
            foreach (var movie in topMovies)
            {
                var ratingDisplay = movie.Rating.HasValue ? $"{movie.Rating.Value}/10" : "N/A";
                Console.WriteLine($"{position}. \"{movie.Title}\" ({movie.Year}) | Rating: {ratingDisplay}");
                position++;
            }
        }
    }
}
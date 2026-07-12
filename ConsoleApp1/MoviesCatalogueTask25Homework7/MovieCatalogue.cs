using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalogueTask25Homework7
{
    public class MovieCatalogue
    {
        private List<Movie> _movies = new List<Movie>();

        public void Add(Movie movie)
        {
            _movies.Add(movie);
        }

        public List<Movie> GetAll()
        {
            return _movies;
        }

        public bool Delete(int index)
        {
            if (index < 0 || index >= _movies.Count)
            {
                return false; 
            }

            _movies.RemoveAt(index);
            return true;
        }

        public List<Movie> GetByGenre(MovieGenreList genre)
        {
            return _movies.Where(m => m.Genre == genre).ToList();
        }

        public bool RateMovie(int index, int rating)
        {
            if (index < 0 || index >= _movies.Count)
            {
                return false;
            }

            _movies[index].Rating = rating;
            return true;
        }

        public List<Movie> SearchByTitle(string keyword)
        {
            return _movies
                .Where(m => m.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
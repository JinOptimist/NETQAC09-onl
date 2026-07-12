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
        // Rate, Search, GetTop5 и т.д.
    }
}
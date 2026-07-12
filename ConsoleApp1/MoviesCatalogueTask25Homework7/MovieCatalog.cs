using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalogueTask25Homework7
{
    public class MovieCatalog
    {
        private List<Movie> _movies = new List<Movie>();

        public void Add(Movie movie)
        {
            _movies.Add(movie);
        }

        // Rate, Search, GetTop5 и т.д.
    }
}
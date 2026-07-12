using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCatalogueTask25Homework7
{
    public class Movie
    {
        public int Id { get; set; } //если все-таки будут силы сделать сохранение в базу данных, то Id будет нужен для идентификации фильма в базе
        public string Title { get; set; }
        public int Year { get; set; }
        public MovieGenreList Genre { get; set; }
        public int? Rating { get; set; }


        public Movie(string title, int year, MovieGenreList genre)
        {
            Title = title;
            Year = year;
            Genre = genre;
            Rating = null;
        }
    }
}

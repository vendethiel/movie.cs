using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDotNet.Data
{
    internal class MovieDAO
    {
        public DataModelContainer _ctx;

        public MovieDAO(DataModelContainer ctx)
        {
            _ctx = ctx;
        }

        public Movie Find(int id)
        {
            return _ctx.MovieSet.Find(id);
        }

        public List<Movie> FindAll()
        {
            return _ctx.MovieSet.ToList();
        }

        public void Create(Movie movie)
        {
            _ctx.MovieSet.Add(movie);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Removes a movie
        /// 
        /// As entity doesn't have a way to do this by id (except for a manual where),
        /// we use a small trick.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var movie = _ctx.MovieSet.Find(id);
            _ctx.MovieSet.Remove(movie);
            _ctx.SaveChanges();
        }

    }
}

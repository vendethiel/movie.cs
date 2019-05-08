using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDotNet.Data
{
    class RatingDAO
    {
        public DataModelContainer _ctx;

        public RatingDAO(DataModelContainer ctx)
        {
            _ctx = ctx;
        }

        public void Create(Rating rating)
        {
            _ctx.RatingSet.Add(rating);
            _ctx.SaveChanges();
        }

        public Rating FindByUserAndMovie(int userId, int movieId)
        {
            return _ctx.RatingSet.FirstOrDefault(rating => rating.User.Id == userId && rating.Movie.Id == movieId);
        }
    }
}

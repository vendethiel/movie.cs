using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDotNet.Data
{
    class CommentDAO
    {
        public DataModelContainer _ctx;

        public CommentDAO(DataModelContainer ctx)
        {
            _ctx = ctx;
        }

        public Comment FindByUserAndMovie(int userId, int movieId)
        {
            return _ctx.CommentSet.FirstOrDefault(comment => comment.User.Id == userId && comment.Movie.Id == movieId);
        }

        public void Create(Comment comment)
        {
            _ctx.CommentSet.Add(comment);
            _ctx.SaveChanges();
        }
    }
}

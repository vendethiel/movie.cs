using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDotNet.Data
{
    /// <summary>
    /// Facade for the DAOs
    /// 
    /// Implements Singleton
    /// Implements Facade
    /// </summary>
    public class DataManager
    {
        private static Lazy<DataManager> _instance = new Lazy<DataManager>(() => new DataManager());

        /// <summary>
        /// Singleton for the Facade.
        /// </summary>
        public static DataManager Instance => _instance.Value;

        private UserDAO _userDAO;
        private MovieDAO _movieDAO;
        private RatingDAO _ratingDAO;
        private CommentDAO _commentDAO;

        public DataManager()
        {
            var context = new DataModelContainer();
            _userDAO = new UserDAO(context);
            _movieDAO = new MovieDAO(context);
            _ratingDAO = new RatingDAO(context);
            _commentDAO = new CommentDAO(context);

            var includeThisDllPleaseDotNet = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public int AuthenticateUser(string username, string password)
        {
            var user = _userDAO.FindByUsernameAndPassword(username, password);
            return user != null ? user.Id : 0;
        }

        public int RegisterUser(string username, string password)
        {
            var tryFindDupe = _userDAO.FindByUsername(username);
            if (tryFindDupe != null)
                return 0;

            var user = new User();
            user.Username = username;
            user.Password = password;
            _userDAO.Create(user);
            return user.Id;
        }

        public List<Movie> GetMovies()
        {
            return _movieDAO.FindAll();
        }

        public int AddMovie(string name)
        {
            var movie = new Movie();
            movie.Name = name;
            _movieDAO.Create(movie);
            return movie.Id;
        }

        public void RemoveMovie(int id)
        {
            _movieDAO.Delete(id);
        }

        public bool DidUserRateMovie(int userId, int movieId)
        {
            return _ratingDAO.FindByUserAndMovie(userId, movieId) != null;
        }
        public bool DidUserCommentMovie(int userId, int movieId)
        {
            return _commentDAO.FindByUserAndMovie(userId, movieId) != null;
        }

        public void RateMovie(int userId, int movieId, int grade)
        {
            if (DidUserRateMovie(userId, movieId))
                return;
            var rating = new Rating();
            rating.Grade = grade;
            rating.User = _userDAO.Find(userId);
            rating.Movie = _movieDAO.Find(movieId);
            _ratingDAO.Create(rating);
        }

        public void CommentMovie(int userId, int movieId, string body)
        {
            if (DidUserCommentMovie(userId, movieId))
                return;
            var comment = new Comment();
            comment.Body = body;
            comment.User = _userDAO.Find(userId);
            comment.Movie = _movieDAO.Find(movieId);
            _commentDAO.Create(comment);
        }
    }
}

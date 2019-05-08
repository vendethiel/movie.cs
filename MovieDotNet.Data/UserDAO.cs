using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDotNet.Data
{
    /// DAO for User
    internal class UserDAO
    {
        public DataModelContainer _ctx;

        public UserDAO(DataModelContainer ctx)
        {
            _ctx = ctx;
        }

        public User Find(int userId)
        {
            return _ctx.UserSet.Find(userId);
        }

        public User FindByUsername(string username)
        {
            return _ctx.UserSet.FirstOrDefault(user => user.Username == username);
        }

        public User FindByUsernameAndPassword(string username, string password)
        {
            return _ctx.UserSet
                .FirstOrDefault(user => user.Username == username
                                     && user.Password == password);
        }

        public void Create(User user)
        {
            _ctx.UserSet.Add(user);
            _ctx.SaveChanges();
        }
    }
}

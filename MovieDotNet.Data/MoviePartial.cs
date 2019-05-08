using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDotNet.Data
{
    public partial class Movie
    {
        public int AverageRating => Rating.Count > 0 ? Rating.Sum(r => r.Grade) / Rating.Count : 0;

        public string Summary => Name +
            " (" + (Rating.Count > 0 ? AverageRating + "/5" : "no rating!") + ")" +
            (Comment.Count > 0 ? " (" + Comment.Count + " comment(s))" : "");
    }
}

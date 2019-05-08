using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDotNet.Data
{
    partial class Comment
    {
        public string AuthorName => "Written by: " + User.Username;
    }
}

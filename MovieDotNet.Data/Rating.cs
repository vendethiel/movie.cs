//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieDotNet.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rating
    {
        public int Id { get; set; }
        public int Grade { get; set; }
    
        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

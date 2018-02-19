using System;
using System.Collections.Generic;

namespace Movie_Site.Models
{
    public partial class Movie
    {
        public Movie()
        {
            SessionTimes = new HashSet<SessionTimes>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Mdescription { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<SessionTimes> SessionTimes { get; set; }
    }
}

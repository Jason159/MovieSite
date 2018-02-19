using System;
using System.Collections.Generic;

namespace Movie_Site.Models
{
    public partial class Cineplex
    {
        public Cineplex()
        {
            SessionTimes = new HashSet<SessionTimes>();
        }

        public int CineplexId { get; set; }
        public string Location { get; set; }
        public string Cdescription { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<SessionTimes> SessionTimes { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Movie_Site.Models
{
    public partial class SessionTimes
    {
        public int SessionId { get; set; }
        public int CineplexId { get; set; }
        public int MovieId { get; set; }
        public DateTime MovieTime { get; set; }
        public string BookedSeats { get; set; }

        public virtual Cineplex Cineplex { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

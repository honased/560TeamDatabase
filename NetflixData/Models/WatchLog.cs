using System;
using System.Collections.Generic;
using System.Text;

namespace NetflixData.Models
{
    public class WatchLog
    {
        public int WatchLogID { get; set; }
        public int ShowID { get; set; }
        public DateTimeOffset WatchedOn { get; set; }

        public string Log => WatchedOn.ToString("MMMM dd, yyyy hh:mm tt");

        public WatchLog(int watchLogID)
        {
            WatchLogID = watchLogID;
            WatchedOn = DateTimeOffset.MinValue;
            ShowID = 0;
        }
    }
}

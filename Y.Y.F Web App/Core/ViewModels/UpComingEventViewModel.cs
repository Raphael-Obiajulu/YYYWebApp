using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class UpComingEventViewModel
    {
        public int Id {  get; set; }
        public string? EventTitle { get; set; }
        public string EventDate { get; set; }
        public string? EventTime { get; set; }
        public string? EventDetails { get; set; }

        public string? EventImage { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public object PrayerRequests { get; set; }
        public string PrayerRequestTitle { get; set; }
        public string PrayerRequestDetails { get; set; }
    }
}

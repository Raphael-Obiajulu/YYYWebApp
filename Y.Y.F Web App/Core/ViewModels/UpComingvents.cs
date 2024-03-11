using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class UpComingvents
    {
        public int Id {  get; set; }
        public string? EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public string? EventTime { get; set; }
        public string? EventDetails { get; set; }

        public string? EventImage { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}

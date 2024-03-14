using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UpComingEvents : BaseModel
    {
        public string EventTitle { get; set; }
        public string EventDate { get; set; }
        public string? EventTime { get; set; }
        public string EventDetails {  get; set; }

        public string? EventImage { get; set; }

    }
}

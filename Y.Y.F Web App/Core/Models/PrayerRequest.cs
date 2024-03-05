using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DB.YYFEnums;

namespace Core.Models
{
    public class PrayerRequest : BaseModel
    {
        public string PrayerRequestTitle { get; set; }
        public string PrayerRequestDetails { get; set; }
        public string UserId { get; set; }
        [Display(Name = "User")]
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        public StatusEnum PrayerRequestStatus  { get; set; }
    }
}

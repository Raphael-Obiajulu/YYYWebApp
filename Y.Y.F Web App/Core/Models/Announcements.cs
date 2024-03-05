using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Announcements : BaseModel
    {
        public string AnnouncementTitle { get; set; }
        public DateTime DurationFrom { get; set; }
        public DateTime DurationTill { get; set; }
        public string AnnouncementDetails { get; set; }
        public string? UserId { get; set; }
        [Display(Name = "User")]
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}

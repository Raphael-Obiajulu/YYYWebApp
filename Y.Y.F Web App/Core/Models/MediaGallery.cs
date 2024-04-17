using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MediaGallery : BaseModel
    {
        public string MediaTitle { get; set; }
        public string? Sermons { get; set; }

        public string? MediaImage { get; set; }
        public string? WorshipMusic { get; set; }
        public string? Video { get; set; }
        public string? UserId { get; set; }
        [Display(Name = "User")]
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "User")]
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public int DiscussionForumId { get; set; }
        [Display(Name = "Discussion")]
        [ForeignKey("DiscussionForumId")]
        public virtual DiscussionForum Discussion { get; set; }

    }
}

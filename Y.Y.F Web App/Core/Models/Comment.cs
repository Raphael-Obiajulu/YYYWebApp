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
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        [Display(Name = "User")]
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public int DiscussionForumId { get; set; }
        [Display(Name = "Discussion")]
        [ForeignKey("DiscussionForumId")]
        public virtual DiscussionForum Discussion { get; set; }
        public StatusEnum CommentStatus { get; set; }
    }
}

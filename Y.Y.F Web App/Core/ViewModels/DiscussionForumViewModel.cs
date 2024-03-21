using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class DiscussionForumViewModel
    {
        public int Id { get; set; }

        public string? DiscussionTitle { get; set; }

        public string? DiscussionDetails { get; set;}

        public string? UserId { get; set; }
        [Display(Name = "User")]
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
        public DiscussionForumViewModel? SingleDiscussion { get; set; }

        public string? Name { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public List<CommentsViewModel>? Comments { get; set; }
        public int NoOfComments { get; set; }
        public int NoOfLikes { get; set; }
    }
}

using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DB.YYFEnums;

namespace Core.ViewModels
{
    public class CommentsViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DiscussionTitle { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int DiscussionForumId { get; set; }
        public virtual DiscussionForum Discussion { get; set; }
        public StatusEnum CommentStatus { get; set; }
    }
}

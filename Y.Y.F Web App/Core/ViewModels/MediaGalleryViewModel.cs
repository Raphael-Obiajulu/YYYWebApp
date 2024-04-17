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
    public class MediaGalleryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string MediaTitle { get; set; }
        public string? Sermons { get; set; }
        public string? MediaImage { get; set; }
        public string? WorshipMusic { get; set; }
        public string? Video { get; set; }
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public List<Video>? MediaVideos { get; set; }
        public List<MediaGalleryViewModel>? AllMedia { get; set; }
    }
}

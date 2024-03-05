using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DB
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Announcements> Announcements { get; set; }
        public DbSet<BibleStudy> BibleStudies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommonDropdown> CommonDropdowns { get; set; }
        public DbSet<DiscussionForum> Discussions { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<PrayerRequest> PrayerRequests { get; set; }
        public DbSet<UpComingEvents> UpComingEvents { get; set; }
    }
}

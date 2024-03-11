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
    public class PrayerRequestViewModel
    {
        public int Id { get; set; }
        public string PrayerRequestTitle { get; set; }
        public string PrayerRequestDetails { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public StatusEnum PrayerRequestStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; } = false;
    }
}

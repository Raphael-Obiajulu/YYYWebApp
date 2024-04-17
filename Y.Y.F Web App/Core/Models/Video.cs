using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Video : BaseModel
    {
        public string VideoTitle { get; set; }
        public string MediaVideo { get; set; }

    }
}

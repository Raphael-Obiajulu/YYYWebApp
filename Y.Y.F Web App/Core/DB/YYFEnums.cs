using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DB
{
    public class YYFEnums
    {
        public enum StatusEnum
        {
            [Description("Pending")]
            Pending = 1,
            [Description("Approved")]
            Approved = 2,
            [Description("Rejected")]
            Rejected = 3,
        }
    }
}

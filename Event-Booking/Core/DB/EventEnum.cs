using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DB
{
    public class EventEnum
    {
        public enum BookingStatus
        {
            [Description("For Pending")]
            Pending = 1,
            [Description("For Approved")]
            Approved,
            [Description("For Declined")]
            Declined,
            [Description("For Cancelled")]
            Cancelled,
          
        }
    }
}

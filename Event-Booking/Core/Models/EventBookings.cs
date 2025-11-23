using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DB.EventEnum;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Models
{
    public class EventBookings : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Note { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public virtual Events Event { get; set; }
        public string BookerId { get; set; }
        [ForeignKey("BookerId")]
        public virtual ApplicationUser Booker { get; set; }
        public BookingStatus BookingStatus { get; set; }

    }

}

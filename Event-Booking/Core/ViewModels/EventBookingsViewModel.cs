using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DB.EventEnum;

namespace Core.ViewModels
{
    public class EventBookingsViewModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public virtual Events Event { get; set; }
        public string BookerId { get; set; }
        [ForeignKey("BookerId")]
        public virtual ApplicationUser Booker { get; set; }
        public BookingStatus BookingStatus { get; set; }
    }
}

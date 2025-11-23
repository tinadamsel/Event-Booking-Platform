using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class EventsViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public string Summary { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public int EventCapacity { get; set; }
    }
}

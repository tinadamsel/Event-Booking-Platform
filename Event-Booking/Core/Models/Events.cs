using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Models
{
    public class Events : BaseModel
    {
        public string? Title { get; set; }
        public string Summary { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public int EventCapacity { get; set; }

    }
}

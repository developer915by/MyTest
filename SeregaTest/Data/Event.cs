using SeregaTest.Models;
using System;
using System.Collections.Generic;

namespace SeregaTest.Data
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<CustomEventData> Properties { get; set; }
        public virtual List<ApplicationUser> Users { get; set; }
    }
}
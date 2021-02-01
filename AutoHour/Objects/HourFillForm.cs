using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHour.Objects
{
    public class HourFillForm
    {
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

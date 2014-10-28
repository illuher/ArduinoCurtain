using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurtainDriver
{
    public class MovementRequest
    {
        public TimeSpan Time { get; set; }
        public int Position { get; set; }
        public DayOfWeek Day { get; set; }
    }
}

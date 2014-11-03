using System;

namespace CurtainDriver
{
    public class MovementRequest
    {
        public TimeSpan Time { get; set; }
        public int Position { get; set; }
        public DayOfWeek Day { get; set; }
    }
}
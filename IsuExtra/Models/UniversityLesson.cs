using System;

#pragma warning disable SA1201

namespace IsuExtra.Models
{
    public class UniversityLesson
    {
        public string Name { get; }
        public TimePeriod Time { get; }

        public UniversityLesson(TimePeriod startTime, string name)
        {
            Time = startTime;
            Name = name;
        }
    }
}
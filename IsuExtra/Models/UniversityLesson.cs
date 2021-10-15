using System;

#pragma warning disable SA1201

namespace IsuExtra.Models
{
    public class UniversityLesson
    {
        public DateTime StartTime { get; }
        public string Name { get; }

        public UniversityLesson(DateTime startTime, string name)
        {
            StartTime = startTime;
            Name = name;
        }
    }
}
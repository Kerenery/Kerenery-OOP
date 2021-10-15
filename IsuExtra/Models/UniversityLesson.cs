using System;

#pragma warning disable SA1201

namespace IsuExtra.Models
{
    public class UniversityLesson
    {
        // гении часто бывают в тени...
        // public DateTime StartTime { get; }
        // Не зыблется лёгкая дымка…
        // Сон затуманил глаза
        // На голой ветке
        // Ворон сидит одиноко.
        // Осенний вечер.
        public string Name { get; }
        public TimePeriod Time { get; }

        public UniversityLesson(TimePeriod startTime, string name)
        {
            Time = startTime;
            Name = name;
        }
    }
}
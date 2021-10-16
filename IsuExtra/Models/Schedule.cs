using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Schedule
    {
        private Dictionary<Week, List<UniversityLesson>> _schedule = new Dictionary<Week, List<UniversityLesson>>()
        {
            { Week.Monday, new List<UniversityLesson>() },
            { Week.Tuesday, new List<UniversityLesson>() },
            { Week.Wednesday, new List<UniversityLesson>() },
            { Week.Thursday, new List<UniversityLesson>() },
            { Week.Friday, new List<UniversityLesson>() },
            { Week.Saturday, new List<UniversityLesson>() },
            { Week.Sunday, new List<UniversityLesson>() },
        };

        public void AddLesson(Week day, string lessonName, TimePeriod time)
        {
            if (!Enum.IsDefined(typeof(Week), day))
                throw new IsuExtraException("day is not defined");

            _schedule[day].Add(new UniversityLesson(time, lessonName));
        }

        public void RemoveLesson(Week day, string lessonName) =>
            _schedule[day].Remove(_schedule[day].First(l => l.Name == lessonName));

        public List<UniversityLesson> GetLessonsByDay(Week day) => _schedule[day];

        public bool HasLessons() => _schedule.Any();
    }
}
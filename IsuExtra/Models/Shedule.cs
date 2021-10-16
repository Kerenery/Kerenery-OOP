using System;
using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Models
{
    public class Shedule
    {
        private Dictionary<Week, List<UniversityLesson>> _shedule = new Dictionary<Week, List<UniversityLesson>>()
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
            =>
                _shedule[day].Add(new UniversityLesson(time, lessonName));

        public void RemoveLesson(Week day, string lessonName) =>
            _shedule[day].Remove(_shedule[day].First(l => l.Name == lessonName));

        public List<UniversityLesson> GetLessonsByDay(Week day) => _shedule[day];

        public bool HasLessons() => _shedule.Any();
    }
}
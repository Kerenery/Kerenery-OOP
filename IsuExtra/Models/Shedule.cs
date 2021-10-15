using System;
using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Models
{
    public class Shedule
    {
        private Dictionary<Week, List<UniversityLesson>> _shedule = new Dictionary<Week, List<UniversityLesson>>();

        public void AddLesson(Week day, string lessonName, TimePeriod time)
        {
            // if (_shedule.Keys.Any(d => d != day))
            _shedule.Add(day, new List<UniversityLesson>());

            _shedule[day].Add(new UniversityLesson(time, lessonName));
        }

        public void RemoveLesson(Week day, string lessonName) =>
            _shedule[day].Remove(_shedule[day].First(l => l.Name == lessonName));

        public List<UniversityLesson> GetLessonsByDay(Week day) => _shedule[day];
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;
using Group = IsuExtra.Models.Group;

namespace Isu.Tests
{
    public class Tests
    {
        private IsuExtraService<Faculty<Group>> _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuExtraService<Faculty<Group>>();
        }

        [Test]
        public void GetGroupCourse()
        {
            // _isuService.AddFaculty("CAT");
            // _isuService.AddFaculty("ITIP");
            Shedule shedule = new Shedule();
            Shedule newShedule = new Shedule();
            shedule.AddLesson(Week.Monday, "KALIK", TimePeriod.First);
            newShedule.AddLesson(Week.Monday, "KALIK", TimePeriod.Second);
            // IEnumerable<TimePeriod> result = shedule.GetLessonsByDay(Week.Sunday)
            //     .Select(l => l.Time)
            //     .Intersect(newShedule.GetLessonsByDay(Week.Sunday).Select(l => l.Time));
            // foreach (var aboba in result)
            // {
            //     Console.WriteLine(aboba);
            // }

            _isuService.AddFaculty("ITIP");
            _isuService.AddFaculty("CAT");
            _isuService.AddGroup("M3206", "ITIP");
            _isuService.AddGroup("I3205", "CAT");
            //Console.WriteLine(_isuService.GetGroups("ITIP", "M3206").Name);
            _isuService.AddStudent("Nick", "M3206", "ITIP");
            _isuService.AddSheduleToGroup("ITIP", "M3206", shedule);
            _isuService.AddSheduleToGroup("CAT", "I3205", newShedule);
            _isuService.AddStudentToMobileCourse("Nick", "M3206", "I3205", "ITIP", "CAT");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;
using Group = IsuExtra.Models.Group;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IsuExtraService<Faculty<Group>> _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuExtraService<Faculty<Group>>();
        }

        [Test]
        public void IntersectionInShedule_ThrowException()
        {
            var shedule = new Shedule();
            var newShedule = new Shedule();
            shedule.AddLesson(Week.Monday, "KALIK", TimePeriod.First);
            newShedule.AddLesson(Week.Monday, "KALIK+KUMARIK", TimePeriod.First);
            _isuService.AddFaculty("ITIP");
            _isuService.AddFaculty("CAT");
            _isuService.AddGroup("M3206");
            _isuService.AddGroup("I3205");
            _isuService.AddStudent("Nick", "M3206");
            _isuService.AddSheduleToGroup("ITIP", "M3206", shedule);
            _isuService.AddSheduleToGroup("CAT", "I3205", newShedule);
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuService.AddStudentToMobileCourse("Nick", "M3206", "I3205");
            });
        }

        [Test]
        public void GetUnsubscribeStudents()
        {
            _isuService.AddFaculty("ITIP");
            _isuService.AddFaculty("CAT");
            _isuService.AddGroup("M3206");
            _isuService.AddGroup("I3205");
            _isuService.AddStudent("Nick", "M3206");
            _isuService.AddStudent("Pick", "M3206");
            var shedule = new Shedule();
            var newShedule = new Shedule();
            _isuService.AddSheduleToGroup("ITIP", "M3206", shedule);
            _isuService.AddSheduleToGroup("CAT", "I3205", newShedule);
            Console.WriteLine(_isuService.GetUnsubStudent("M3206").First().Name);
        }

        [Test]
        public void GetStudentsFromMobileGroup()
        {
            _isuService.AddFaculty("ITIP");
            _isuService.AddFaculty("CAT");
            _isuService.AddGroup("M3206");
            _isuService.AddGroup("I3205");
            _isuService.AddStudent("Pick", "M3206");
            Console.WriteLine(_isuService.GetStudentsMobileGroup("M3206").First());
        }

        [TestCase("Nick")]
        public void AddStudentTwice_ThrowException(string name)
        {
            _isuService.AddFaculty("ITIP");
            _isuService.AddGroup("M3206");
            _isuService.AddStudent(name, "M3206");
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuService.AddStudent(name, "M3206");
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        private IsuExtraService<Faculty<Group>> _isuService;
        private Schedule _schedule;
        private Schedule _newSchedule;
        private Schedule _superSchedule;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuExtraService<Faculty<Group>>();
            _schedule = new Schedule();
            _newSchedule = new Schedule();
            _superSchedule = new Schedule();
            _schedule.AddLesson(Week.Monday, "KALIK", TimePeriod.First);
            _newSchedule.AddLesson(Week.Monday, "KALIK+KUMARIK", TimePeriod.First);
            _superSchedule.AddLesson(Week.Friday, "RABOTAY", TimePeriod.Fifth);
            _isuService.AddFaculty("ITIP");
            _isuService.AddFaculty("CAT");
            _isuService.AddGroup("M3206");
            _isuService.AddGroup("I3205");
            _isuService.AddGroup("I3206");
        }

        [Test]
        public void IntersectionInSchedule_ThrowException()
        {
            _isuService.AddStudent("Nick", "M3206");
            _isuService.AddSheduleToGroup("ITIP", "M3206", _schedule);
            _isuService.AddSheduleToGroup("CAT", "I3205", _newSchedule);
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuService.AddStudentToMobileCourse("Nick", "M3206", "I3205");
            });
        }

        [Test]
        public void GetUnsubscribeStudents()
        {
            _isuService.AddStudent("Nick", "M3206");
            _isuService.AddStudent("Pick", "M3206");
            var shedule = new Schedule();
            var newShedule = new Schedule();
            _isuService.AddSheduleToGroup("ITIP", "M3206", shedule);
            _isuService.AddSheduleToGroup("CAT", "I3205", newShedule);
            Assert.AreEqual("Nick", _isuService.GetUnsubStudent("M3206").First().Name);
        }

        [Test]
        public void GetStudentsFromMobileGroup()
        {
            _isuService.AddStudent("Pick", "M3206");
            Assert.AreEqual("Pick", _isuService.GetStudentsMobileGroup("M3206").First().Name);
        }

        [TestCase("Nick")]
        public void AddStudentTwice_ThrowException(string name)
        {
            _isuService.AddStudent(name, "M3206");
            Assert.Catch<IsuExtraException>(() =>
            {
                _isuService.AddStudent(name, "M3206");
            });
        }

        [Test]
        public void AddStudentToMobileCourse()
        {
            _isuService.AddSheduleToGroup("ITIP", "M3206", _schedule);
            _isuService.AddSheduleToGroup("CAT", "I3206", _superSchedule);
            _isuService.AddStudent("Pick", "M3206");
            _isuService.AddStudentToMobileCourse("Pick", "M3206", "I3206");
            CollectionAssert.AreEqual(_isuService.GetMobileGroups("CAT").Last().GetStudents(),
                _isuService.GetStudentsMobileGroup("I3206"));
        }

        [Test]
        public void UnsubTinyStudent()
        {
            _isuService.AddSheduleToGroup("ITIP", "M3206", _schedule);
            _isuService.AddSheduleToGroup("CAT", "I3206", _superSchedule);
            _isuService.AddStudent("Pick", "M3206");
            _isuService.AddStudentToMobileCourse("Pick", "M3206", "I3206");
            _isuService.UnsubscribeStudent("Pick", "I3206");
            CollectionAssert.IsEmpty(_isuService.GetStudentsMobileGroup("I3206"));
        }
    }
}
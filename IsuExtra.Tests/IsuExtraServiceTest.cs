using System;
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
        private Schedule _shedule;
        private Schedule _newShedule;
        private Schedule _superShedule;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuExtraService<Faculty<Group>>();
            _shedule = new Schedule();
            _newShedule = new Schedule();
            _superShedule = new Schedule();
            _shedule.AddLesson(Week.Monday, "KALIK", TimePeriod.First);
            _newShedule.AddLesson(Week.Monday, "KALIK+KUMARIK", TimePeriod.First);
            _superShedule.AddLesson(Week.Friday, "RABOTAY", TimePeriod.Fifth);
            _isuService.AddFaculty("ITIP");
            _isuService.AddFaculty("CAT");
            _isuService.AddGroup("M3206");
            _isuService.AddGroup("I3205");
            _isuService.AddGroup("I3206");
        }

        [Test]
        public void IntersectionInShedule_ThrowException()
        {
            _isuService.AddStudent("Nick", "M3206");
            _isuService.AddSheduleToGroup("ITIP", "M3206", _shedule);
            _isuService.AddSheduleToGroup("CAT", "I3205", _newShedule);
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
            _isuService.AddSheduleToGroup("ITIP", "M3206", _shedule);
            _isuService.AddSheduleToGroup("CAT", "I3206", _superShedule);
            _isuService.AddStudent("Pick", "M3206");
            _isuService.AddStudentToMobileCourse("Pick", "M3206", "I3206");
        }

        [Test]
        public void UnsubTinyStudent()
        {
            _isuService.AddSheduleToGroup("ITIP", "M3206", _shedule);
            _isuService.AddSheduleToGroup("CAT", "I3206", _superShedule);
            _isuService.AddStudent("Pick", "M3206");
            _isuService.AddStudentToMobileCourse("Pick", "M3206", "I3206");
            _isuService.UnsubscribeStudent("Pick", "I3206");
        }
    }
}
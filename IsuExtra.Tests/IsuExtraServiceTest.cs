using System;
using System.Collections.Generic;
using IsuExtra.Models;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

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
            Console.WriteLine(
                _isuService.AddFaculty("CAT"));
            Console.WriteLine(
                _isuService.AddFaculty("ITIP"));
            _isuService.AddGroup("M3201", "ITIP");
            UniversityLesson lesson = new UniversityLesson(DateTime.Today, "ABOOP");
            _isuService.
        }
    }
}
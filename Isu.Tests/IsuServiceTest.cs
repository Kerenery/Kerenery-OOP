using System;
using Isu.Models;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M3206");
                _isuService.AddStudent(_isuService.FindGroup("M3206"), "Nick");
                _isuService.AddStudent(_isuService.FindGroup("M3206"), "Nick");
            });
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M3206");
                int count = default;
                
                for (int i = 0; i < 30; ++i)
                {
                    var student = _isuService.AddStudent(_isuService.FindGroup("M3206"), $"NICK POOPICH{++count}");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            { 
                _isuService.AddGroup("M1488");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_InvalidGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M3211");
                _isuService.AddGroup("M3206");
                var superGroup = new Group("M3230");
                _isuService.AddStudent(_isuService.FindGroup("M3211"), "Nickolasha");
                _isuService.ChangeStudentGroup(_isuService.FindStudent("Nickolasha"), superGroup);
            });
        }
    }
}
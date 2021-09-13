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
            _isuService = null;
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuService();
                isu.AddGroup("M3206");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick");
            });
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuService();
                isu.AddGroup("M3206");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick2");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick3");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick4");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick5");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick6");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick7");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick8");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick9");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick10");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick11");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick12");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick13");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick14");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick15");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick16");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick17");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick18");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick19");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick20");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick21");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick22");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick23");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick24");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick25");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick26");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick27");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick28");
                isu.AddStudent(isu.FindGroup("M3206"), "Nick29");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuService();
                isu.AddGroup("Invalid format");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuService();
                isu.AddGroup("M3211");
                isu.AddGroup("M3206");
                Group superGroup = new Group("M3230");
                isu.AddStudent(isu.FindGroup("M3211"), "Nickolasha");
                isu.ChangeStudentGroup(isu.FindStudent("Nickolasha"),superGroup);
            });
        }
    }
}
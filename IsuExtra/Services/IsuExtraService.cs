using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using IsuExtra.Helpers;
using IsuExtra.Models;
using IsuExtra.Tools;
using Group = IsuExtra.Models.Group;

namespace IsuExtra.Services
{
    public class IsuExtraService<T> : IIsuExtraService
        where T : Faculty<Group>
    {
        private readonly List<T> _faculties = new List<T>();

        public Component AddFaculty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IsuExtraException("name cant be null or empty");

            if (_faculties.Any(f => f.Name == name))
                throw new IsuExtraException("such faculty is already registered");

            var faculty = new Faculty<Group>(name);

            _faculties.Add(faculty as T);
            return faculty;
        }

        public Component AddGroup(string name, string facultyName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IsuExtraException("name cant be null");

            Component group = new Group(name);
            _faculties.First(f => f.Name == facultyName).Add(group);
            return group;
        }

        public Component AddStudent(string name, string groupName, string facultyName)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(groupName))
                throw new IsuExtraException("name cant be null");

            Component student = new Student(name);

            // FacultyAttachment faculty = GroupNameHandler.ExtractFaculty(groupName);
            // string ownFaculty = Enum.GetName(typeof(FacultyAttachment), faculty);
            _faculties.First(f => f.Name == facultyName).GetGroup(groupName).Add(student);
            return student;
        }

        public void AddSheduleToGroup(string facultyName, string groupName, Shedule shedule)
        {
            if (string.IsNullOrWhiteSpace(facultyName) || string.IsNullOrWhiteSpace(groupName))
                throw new IsuExtraException("string is null");

            if (_faculties.All(f => f.Name != facultyName))
                throw new IsuExtraException("there is no such faculty");

            _faculties.First(f => f.Name == facultyName).GetGroup(groupName).GroupShedule = shedule;
        }

        public Component AddStudentToMobileCourse(string name, string groupName, string mobileGroupName, string fname, string mname)
        {
            if (string.IsNullOrWhiteSpace(mobileGroupName) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(groupName))
                throw new IsuExtraException("string is null");

            FacultyAttachment ownFaculty = GroupNameHandler.ExtractFaculty(groupName);
            FacultyAttachment mobileFaculty = GroupNameHandler.ExtractFaculty(mobileGroupName);

            if (ownFaculty == mobileFaculty)
                throw new IsuExtraException("same course boy");

            Component student =
                _faculties.First(f => f.Name == fname).GetGroup(groupName).FindStudent(name) ??
                throw new IsuExtraException("There is no such clever student");

            Group ownGroup = _faculties.Find(f => f.Name == fname).GetGroup(groupName);
            Group mobileGroup = _faculties.Find(f => f.Name == mname).GetGroup(mobileGroupName);

            IEnumerable<TimePeriod> result = ownGroup.GroupShedule.GetLessonsByDay(Week.Monday)
                .Select(l => l.Time)
                .Intersect(mobileGroup.GroupShedule.GetLessonsByDay(Week.Monday).Select(l => l.Time));

            if (result is null)
                throw new IsuExtraException("Intersection!");

            _faculties.First(f => f.Name == mname).GetGroup(mobileGroupName).Add(student);

            return student;
        }

        public void UnsubscribeStudent(string name, string faculty)
        {
            throw new System.NotImplementedException();
        }

        public List<Component> GetMobileGroups(string facultyName)
        {
            throw new System.NotImplementedException();
        }

        public List<Component> GetStudentMobileGroup(string groupName)
        {
            throw new System.NotImplementedException();
        }

        public List<Component> GetUnsubStudent(string commonGroupName)
        {
            throw new System.NotImplementedException();
        }

        public Group GetGroups(string faculty, string groupName) => _faculties.First(f => f.Name == faculty).GetGroup(groupName);
    }
}
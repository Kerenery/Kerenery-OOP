using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            // ???
            _faculties.Add(faculty as T);
            return faculty;
        }

        public Component AddGroup(string name, string facultyName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IsuExtraException("name cant be null");

            Component group = new Models.Group(name);

            _faculties.First(f => f.Name == facultyName).Add(group);

            return group;
        }

        public void AddSheduleToGroup(string facultyName, string groupName, List<DayShedule> lessons)
        {
            if (string.IsNullOrWhiteSpace(facultyName) || string.IsNullOrWhiteSpace(groupName))
                throw new IsuExtraException("string is null");

            if (_faculties.All(f => f.Name != facultyName))
                throw new IsuExtraException("there is no such faculty");

            _faculties.First(f => f.Name == facultyName).GetGroup(groupName).SetShedule(lessons);
        }

        public Component AddStudentToMobileCourse(string name, string facultyName)
        {
            throw new System.NotImplementedException();
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
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IsuExtra.Models;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<Faculty> _faculties = new List<Faculty>();

        public Component AddFaculty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IsuExtraException("name cant be null or empty");

            if (_faculties.Any(f => f.Name == name))
                throw new IsuExtraException("such faculty is already registerd");

            var faculty = new Faculty(name);
            _faculties.Add(faculty);
            return faculty;
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
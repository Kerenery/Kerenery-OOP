using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Models;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private static int _studentsCount = default;

        private readonly Dictionary<Group, List<Student>> _repository = new Dictionary<Group, List<Student>>();

        public Group AddGroup(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IsuException("Wrong input");
            }

            if (_repository.Keys.Any(x => x.GroupName == name))
            {
                throw new IsuException("Group already exists");
            }

            var group = new Group(name);
            _repository.Add(group, new List<Student>());
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new IsuException("Wrong input");
            }

            if (_repository.Keys.All(g => g.GroupName != group.GroupName))
            {
                throw new IsuException("Such group doesn't really exist");
            }

            if (_repository.Values.Any(x => x.Any(s => s.StudentName == name)))
            {
                throw new IsuException("Student already exists");
            }

            if (_repository[group].Capacity >= group.MaxStudentCount)
            {
                throw new IsuException("The group reached max students count");
            }

            var student = new Student(name, ++_studentsCount);

            _repository[group].Add(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            if (!_repository.Values.Any(s => s.Any(st => st.StudentId == id)))
            {
                throw new IsuException("Such student doesn't really exists");
            }

            return _repository.Values.SelectMany(x => x).FirstOrDefault(s => s.StudentId == id);
        }

        public Student FindStudent(string name)
        {
            if (!_repository.Values.Any(s => s.Any(st => st.StudentName == name)))
            {
                return null;
            }

            return _repository.Values.First(ls => ls.Any(s => s.StudentName == name)).First(s => s.StudentName == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            if (_repository.Keys.Any(g => g.GroupName != groupName))
            {
                throw new IsuException("Such group doesn't really exist");
            }

            var group = _repository.Keys.First(g => g.GroupName == groupName);
            return _repository[group];
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> oneCourseStudents = new List<Student>();

            foreach (var kvp in _repository.Where(kvp => kvp.Key.CourseNumber == courseNumber))
            {
                oneCourseStudents.AddRange(_repository[kvp.Key]);
            }

            return oneCourseStudents;
        }

        public Group FindGroup(string groupName)
        {
            // returns null as default value
            return _repository.Keys.FirstOrDefault(g => g.GroupName == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _repository.Keys.ToList().FindAll(g => g.CourseNumber == courseNumber);
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (FindStudent(student.StudentName) == null)
            {
                throw new IsuException("There is no such student");
            }

            if (!_repository.Keys.Any(g => g.GroupName == newGroup.GroupName))
            {
                throw new IsuException("There is no such group");
            }

            _repository.Values.First(ls => ls.Any(s => s.Equals(student))).Remove(student);
            _repository[newGroup].Add(student);
        }
    }
}
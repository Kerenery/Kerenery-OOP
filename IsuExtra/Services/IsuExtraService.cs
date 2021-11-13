using System;
using System.Collections.Generic;
using System.Linq;
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

        public Component AddGroup(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IsuExtraException("name cant be null");

            Component group = new Group(name);

            FacultyAttachment faculty = GroupNameHandler.ExtractFaculty(name);
            string facultyName = Enum.GetName(typeof(FacultyAttachment), faculty);
            _faculties.First(f => f.Name == facultyName).Add(group);
            return group;
        }

        public Component AddStudent(string name, string groupName)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(groupName))
                throw new IsuExtraException("name cant be null");

            Component student = new Student(name);

            FacultyAttachment faculty = GroupNameHandler.ExtractFaculty(groupName);
            string ownFaculty = Enum.GetName(typeof(FacultyAttachment), faculty);

            _faculties.First(f => f.Name == ownFaculty).GetGroup(groupName).Add(student);
            return student;
        }

        public void AddSheduleToGroup(string facultyName, string groupName, Schedule schedule)
        {
            if (string.IsNullOrWhiteSpace(facultyName) || string.IsNullOrWhiteSpace(groupName))
                throw new IsuExtraException("string is null");

            if (_faculties.All(f => f.Name != facultyName))
                throw new IsuExtraException("there is no such faculty");

            if (!schedule.HasLessons())
                throw new IsuExtraException("Schedule.cs is empty");

            _faculties.First(f => f.Name == facultyName).GetGroup(groupName).GroupShedule = schedule;
        }

        public Component AddStudentToMobileCourse(string name, string groupName, string newGroupName)
        {
            if (string.IsNullOrWhiteSpace(newGroupName) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(groupName))
                throw new IsuExtraException("string is null");

            FacultyAttachment faculty = GroupNameHandler.ExtractFaculty(groupName);
            string ownFacultyName = Enum.GetName(typeof(FacultyAttachment), faculty);
            FacultyAttachment mobileFaculty = GroupNameHandler.ExtractFaculty(newGroupName);
            string mobileFacultyName = Enum.GetName(typeof(FacultyAttachment), mobileFaculty);

            if (string.IsNullOrWhiteSpace(ownFacultyName) || string.IsNullOrWhiteSpace(mobileFacultyName))
                throw new IsuExtraException("there is no such faculty");

            if (ownFacultyName == mobileFacultyName)
                throw new IsuExtraException("same course boy");

            Student student =
                _faculties.First(f => f.Name == ownFacultyName).GetGroup(groupName).FindStudent(name) ??
                throw new IsuExtraException("There is no such clever student");

            Group ownGroup = _faculties.Find(f => f.Name == ownFacultyName).GetGroup(groupName);
            Group mobileGroup = _faculties.Find(f => f.Name == mobileFacultyName).GetGroup(newGroupName);

            foreach (Week day in (Week[])Enum.GetValues(typeof(Week)))
            {
                var groupSchedule = ownGroup.GroupShedule.GetLessonsByDay(day);
                var newGroupSchedule = mobileGroup.GroupShedule.GetLessonsByDay(day);
                bool isIntersects = groupSchedule.Select(l => l.Time).Intersect(newGroupSchedule.Select(l => l.Time)).Any();

                if (isIntersects)
                    throw new IsuExtraException("Intersection");
            }

            _faculties.First(f => f.Name == mobileFacultyName).GetGroup(newGroupName).Add(student);
            student.SubscribeStudent();
            return student;
        }

        public void UnsubscribeStudent(string name, string groupName)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(groupName))
                throw new IsuExtraException("name is null");

            FacultyAttachment mobileFaculty = GroupNameHandler.ExtractFaculty(groupName);
            string mobileFacultyName = Enum.GetName(typeof(FacultyAttachment), mobileFaculty);

            Group group = _faculties.FirstOrDefault(f => f.Name == mobileFacultyName)?.GetGroup(groupName)
                          ?? throw new IsuExtraException("group doesnt exist");
            Student student = _faculties.FirstOrDefault(f => f.Name == mobileFacultyName)?.GetGroup(groupName)
                .FindStudent(name) ?? throw new IsuExtraException("there is no such student");

            _faculties.FirstOrDefault(f => f.Name == mobileFacultyName)?.GetGroup(groupName)
                .FindStudent(name).SubscribeStudent();
            group.Remove(student);
        }

        public List<Group> GetMobileGroups(string facultyName)
        {
            if (string.IsNullOrWhiteSpace(facultyName))
                throw new IsuExtraException("there is no such faculty");

            return _faculties.FirstOrDefault(f => f.Name == facultyName)?.GetGroups() ??
                   throw new IsuExtraException("there is no such fac");
        }

        public List<Component> GetStudentsMobileGroup(string groupName)
        {
            FacultyAttachment faculty = GroupNameHandler.ExtractFaculty(groupName);
            string ownFacultyName = Enum.GetName(typeof(FacultyAttachment), faculty);

            return GetMobileGroups(ownFacultyName).FirstOrDefault(g => g.Name == groupName)?.GetStudents() ??
                   throw new IsuExtraException("There is no such group");
        }

        public List<Component> GetUnsubStudent(string commonGroupName)
        {
            FacultyAttachment faculty = GroupNameHandler.ExtractFaculty(commonGroupName);
            string ownFacultyName = Enum.GetName(typeof(FacultyAttachment), faculty);

            return GetMobileGroups(ownFacultyName).FirstOrDefault(g => g.Name == commonGroupName)?.GetStudents()
                       .FindAll(s => ((Student)s).IsSubscribed == false) ??
                   throw new IsuExtraException("There is mo such group");
        }
    }
}
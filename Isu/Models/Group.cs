using System.Text.RegularExpressions;
using Isu.Helpers;
using Isu.Tools;

namespace Isu.Models
{
    public class Group
    {
        private string _groupName;
        private CourseNumber _courseNumber;

        public Group(string groupName, int studentsCount = 25)
        {
            GroupName = groupName;
            MaxStudentCount = studentsCount;
            CourseNumber = GroupNameHandler.ExtractCourse(groupName);
        }

        public string GroupName
        {
            get => _groupName;

            private set
            {
                if (!Regex.IsMatch(value, @"M{1}3{1}[1-4]{1}\w{2}", RegexOptions.IgnoreCase))
                {
                     throw new IsuException("Incorrect format");
                }

                _groupName = value;
            }
        }

        public CourseNumber CourseNumber
        {
            get => _courseNumber;
            private set => _courseNumber = value;
        }

        public int MaxStudentCount { get; }
    }
}
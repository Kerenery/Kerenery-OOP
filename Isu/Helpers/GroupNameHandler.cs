using System.Collections.Generic;
using Isu.Models;

namespace Isu.Helpers
{
    public static class GroupNameHandler
    {
        private static readonly Dictionary<char, CourseNumber> _сourseNumbersMapping = new Dictionary<char, CourseNumber>()
        {
            { '1', CourseNumber.First },
            { '2', CourseNumber.Second },
            { '3', CourseNumber.Third },
            { '4', CourseNumber.Fourth },
        };

        public static CourseNumber ExtractCourse(string groupName) => _сourseNumbersMapping[groupName[2]];
    }
}
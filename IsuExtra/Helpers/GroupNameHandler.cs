using System.Collections.Generic;
using System.Linq;
using IsuExtra.Models;

namespace IsuExtra.Helpers
{
    public static class GroupNameHandler
    {
        private static Dictionary<FacultyAttachment, List<string>> _facultiesGroups = new Dictionary<FacultyAttachment, List<string>>()
        {
            { FacultyAttachment.CAT, new List<string> { "M", "K" } },
            { FacultyAttachment.ITIP, new List<string> { "B", "I" } },
        };

        public static FacultyAttachment ExtractFaculty(string groupName)
            => _facultiesGroups.FirstOrDefault(x => x.Value.Any(g => g == groupName[0].ToString())).Key;
    }
}
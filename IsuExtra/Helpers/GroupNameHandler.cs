using System.Collections.Generic;
using System.Linq;
using IsuExtra.Models;
using IsuExtra.Tools;

namespace IsuExtra.Helpers
{
    public static class GroupNameHandler
    {
        private static Dictionary<FacultyAttachment, List<string>> _facultiesGroups = new Dictionary<FacultyAttachment, List<string>>()
        {
            { FacultyAttachment.ITIP, new List<string> { "M", "K" } },
            { FacultyAttachment.CAT, new List<string> { "B", "I" } },
        };

        public static FacultyAttachment ExtractFaculty(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                throw new IsuExtraException("name is null");

            return _facultiesGroups.FirstOrDefault(x => x.Value.Any(g => g == groupName[0].ToString())).Key;
        }
    }
}
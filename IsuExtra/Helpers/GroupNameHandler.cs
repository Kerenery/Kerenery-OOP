using System.Collections.Generic;
using System.Linq;

namespace IsuExtra.Helpers
{
    public static class GroupNameHandler
    {
        private static Dictionary<string, List<string>> _facultiesGroups = new Dictionary<string, List<string>>()
        {
            { "FITIP", new List<string> { "M", "K" } },
            { "BIBIP", new List<string> { "B", "I" } },
        };

        public static string ExtractFaculty(string groupName)
            => _facultiesGroups.FirstOrDefault(x => x.Value.Any(g => g == groupName)).Key;
    }
}
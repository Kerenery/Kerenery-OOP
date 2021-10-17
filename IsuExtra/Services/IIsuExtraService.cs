using System.Collections.Generic;
using IsuExtra.Models;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Component AddFaculty(string name);
        Component AddStudentToMobileCourse(string name, string groupName, string newGroupName);
        void UnsubscribeStudent(string name, string groupName);
        List<Group> GetMobileGroups(string facultyName);
        List<Component> GetStudentsMobileGroup(string groupName);
        List<Component> GetUnsubStudent(string commonGroupName);
    }
}
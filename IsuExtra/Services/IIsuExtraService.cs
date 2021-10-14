using System.Collections.Generic;
using IsuExtra.Models;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Component AddFaculty(string name);
        Component AddStudentToMobileCourse(string name, string facultyName);
        void UnsubscribeStudent(string name, string faculty);
        List<Component> GetMobileGroups(string facultyName);
        List<Component> GetStudentMobileGroup(string groupName);
        List<Component> GetUnsubStudent(string commonGroupName);
    }
}
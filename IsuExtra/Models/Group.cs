using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IsuExtra.Helpers;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Group : Component, IComposite
    {
        private readonly List<Component> _components;

        public Group(string name)
            : base(name)
        {
            if (!Regex.IsMatch(name, @"\w{2}[1-4]\d{2}", RegexOptions.IgnoreCase))
                throw new IsuExtraException("Incorrect format");

            FacultyName = GroupNameHandler.ExtractFaculty(name);
            _components = new List<Component>();
        }

        public FacultyAttachment FacultyName { get; }
        public Schedule GroupShedule { get; set; }

        public Component Add(Component component)
        {
            if (component is not Student)
                throw new IsuExtraException("wrong component boy");

            if (_components.Any(s => ((Student)s).Equals(component)))
                throw new IsuExtraException("such student is already in da group");

            _components.Add(component);
            return component;
        }

        public void Remove(Component component)
        {
            if (component is not Student student)
                throw new IsuExtraException("wrong component boy");

            if (_components.All(s => (s as Student)?.Id != student.Id))
                throw new IsuExtraException("Such student doesn't really exist");

            _components.Remove(student);
        }

        public Student FindStudent(string name) => _components.FirstOrDefault(s => s.Name == name) as Student;
        public List<Component> GetStudents() => _components.ToList();
    }
}
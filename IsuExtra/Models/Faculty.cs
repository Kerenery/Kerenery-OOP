using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Faculty<T> : Component, IComposite
        where T : Component, IComposite
    {
        private readonly List<T> _ownGroups;
        public Faculty(string name)
            : base(name)
        {
            _ownGroups = new List<T>();
        }

        public Component Add(Component component)
        {
            if (component is not T group)
                throw new IsuExtraException("wrong component");

            if (_ownGroups.Any(g => g.Name == component.Name))
                throw new IsuExtraException("already in the list");

            _ownGroups.Add(group);
            return component;
        }

        public void Remove(Component component)
        {
            if (component is not T student)
                throw new IsuExtraException("wrong component boy");

            if (_ownGroups.All(s => s.Id != student.Id))
                throw new IsuExtraException("Such student doesn't really exist");

            _ownGroups.Remove(student);
        }

        public T GetGroup(string name) => _ownGroups.FirstOrDefault(g => g.Name == name) ??
                                          throw new IsuExtraException("there is no such group");
    }
}
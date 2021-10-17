using System;
using IsuExtra.Tools;
#pragma warning disable SA1401

namespace IsuExtra.Models
{
    public abstract class Component
    {
        protected readonly string _name;
        protected string _id;
        public Component(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new IsuExtraException("name cant be null or empty");

            _name = name;
            _id = Guid.NewGuid().ToString();
        }

        public virtual string Id { get => _id; }
        public virtual string Name { get => _name; }
    }
}
﻿using System;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Student : Component
    {
        public Student(string name)
            : base(name)
        {
        }

        public bool IsSubscribed { get; private set; } = false;
        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            var student = (Student)obj;
            return this._name == student._name;
        }

        public void SubscribeStudent() => IsSubscribed = true;
    }
}
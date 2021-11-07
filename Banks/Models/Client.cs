using System;

namespace Banks.Models
{
    public class Client
    {
        public string Name { get; init; }
        public string SecondName { get; init; }
        public string Address { get; init; }
        public string PassportData { get; init; }
        public Guid Id { get; init; }
    }
}
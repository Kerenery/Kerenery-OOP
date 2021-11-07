using System;

namespace Banks.Models
{
    public class Bank
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; init; }

        public byte? Rate { get; init; }

        public decimal? Commission { get; init; }
    }
}
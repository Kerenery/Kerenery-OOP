using System;
using System.ComponentModel.DataAnnotations;
using Banks.Tools;

namespace Banks.Models
{
    public class Bank
    {
        private decimal _profit;
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; init; }
        public decimal Rate { get; init; }

        public decimal CreditLimit { get; init; }

        public decimal Commission { get; init; }

        public decimal Profit
        {
            get => _profit;
            set
            {
                if (value < 0)
                    throw new BanksException("profit can't ve below zero");
                _profit = value;
            }
        }
    }
}
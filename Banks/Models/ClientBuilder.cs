using System;
using System.Text.RegularExpressions;
using Banks.Tools;

namespace Banks.Models
{
    public class ClientBuilder
    {
        private string _name;
        private string _secondName;
        private string _address;
        private string _passportData;
        private Client _client;

        private ClientBuilder() { }

        public static ClientBuilder Init()
            => new ClientBuilder();

        public Client Build()
        {
            _client = new Client()
            {
                Id = Guid.NewGuid(),
                Name = _name, SecondName = _secondName,
                PassportData = _passportData,
                Address = _address,
            };

            return _client;
        }

        public ClientBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BanksException($"{name} is null or whitespace");

            _name = name;
            return this;
        }

        public ClientBuilder SetSecondName(string secondName)
        {
            if (string.IsNullOrWhiteSpace(secondName))
                throw new BanksException($"{secondName} is null or whitespace");

            _secondName = secondName;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new BanksException($"{address} is null or whitespace");

            _address = address;
            return this;
        }

        public ClientBuilder SetPassportData(string passportData)
        {
            if (string.IsNullOrWhiteSpace(passportData))
                throw new BanksException($"{passportData} is null or whitespace");

            _passportData = passportData;
            return this;
        }
    }
}
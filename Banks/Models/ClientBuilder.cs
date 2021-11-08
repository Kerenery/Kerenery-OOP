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
            _name = name;
            return this;
        }

        public ClientBuilder SetSecondName(string secondName)
        {
            _secondName = secondName;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            _address = address;
            return this;
        }

        public ClientBuilder SetPassportData(string passportData)
        {
            _passportData = passportData;
            return this;
        }
    }
}
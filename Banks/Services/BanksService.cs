using System.Collections.Generic;
using System.IO;
using System.Linq;
using Banks.Accounts;
using Banks.Models;
using Banks.Tools;
using Newtonsoft.Json;

namespace Banks.Services
{
    public class BanksService
    {
        private Dictionary<Bank, List<IAccount>> _banks = new Dictionary<Bank, List<IAccount>>();
        private List<Client> _clients = new List<Client>();

        public void SaveState()
        {
            using StreamWriter banksFile
                = File.CreateText(@"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\BanksState.json");
            using StreamWriter clientsFile
                = File.CreateText(@"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\ClientsState.json");
            JsonSerializer banksSerializer = new ();
            banksSerializer.Serialize(banksFile, _banks.ToList());
            JsonSerializer clientsSerializer = new ();
            clientsSerializer.Serialize(clientsFile, _clients);
        }

        public void LoadState()
        {
            using StreamReader banksFReader =
                new (@"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\BanksState.json");
            using StreamReader clientsFReader
                = new (@"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\ClientsState.json");
            string jsonBanks = banksFReader.ReadToEnd();
            string jsonClients = clientsFReader.ReadToEnd();
            _banks = (JsonConvert.DeserializeObject<List<KeyValuePair<Bank, List<IAccount>>>>(jsonBanks)
                      ?? throw new BanksException("deserialization error occured"))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            _clients = JsonConvert.DeserializeObject<List<Client>>(jsonClients);
        }

        public Bank AddBank(string bankName)
        {
            if (string.IsNullOrWhiteSpace(bankName))
                throw new BanksException("name is empty");
            if (_banks.Keys.Any(b => b.Name == bankName))
                throw new BanksException("such bank is already registered");

            var bank = new Bank() { Name = bankName, Commission = 0 };
            _banks.Add(bank, new List<IAccount>());

            return bank;
        }

        public Bank FindBank(string bankName)
            => _banks.Keys.FirstOrDefault(b => b.Name == bankName);

        public Client RegisterClient(Client client)
        {
            if (_clients.Any(c => c.Id == client.Id))
                throw new BanksException("such client is already added");

            _clients.Add(client);
            return client;
        }

       // public void Withdraw(IAccount)
    }
}
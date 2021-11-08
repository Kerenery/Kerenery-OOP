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
        public Dictionary<Bank, List<IAccount>> Banks { get; private set; } = new ();
        public List<Client> Clients { get; private set; } = new ();

        public void SaveState()
        {
            using StreamWriter banksFile
                = File.CreateText(@"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\BanksState.json");
            using StreamWriter clientsFile
                = File.CreateText(@"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\ClientsState.json");
            JsonSerializer banksSerializer = new ();
            banksSerializer.Serialize(banksFile, Banks.ToList());
            JsonSerializer clientsSerializer = new ();
            clientsSerializer.Serialize(clientsFile, Clients);
        }

        public void LoadState()
        {
            using StreamReader banksFReader =
                new (@"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\BanksState.json");
            using StreamReader clientsFReader
                = new (@"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\ClientsState.json");
            string jsonBanks = banksFReader.ReadToEnd();
            string jsonClients = clientsFReader.ReadToEnd();
            Banks = (JsonConvert.DeserializeObject<List<KeyValuePair<Bank, List<IAccount>>>>(jsonBanks)
                      ?? throw new BanksException("deserialization error occured"))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            Clients = JsonConvert.DeserializeObject<List<Client>>(jsonClients);
        }

        public Bank AddBank(string bankName)
        {
            if (string.IsNullOrWhiteSpace(bankName))
                throw new BanksException("name is empty");
            if (Banks.Keys.Any(b => b.Name == bankName))
                throw new BanksException("such bank is already registered");

            var bank = new Bank() { Name = bankName };
            Banks.Add(bank, new List<IAccount>());

            return bank;
        }

        public Bank FindBank(string bankName)
            => Banks.Keys.FirstOrDefault(b => b.Name == bankName);

        public Client RegisterClient(Client client)
        {
            if (Clients.Any(c => c.Id == client.Id))
                throw new BanksException("such client is already added");

            Clients.Add(client);
            return client;
        }
    }
}
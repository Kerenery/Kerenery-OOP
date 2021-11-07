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
        private readonly Dictionary<Bank, List<IAccount>> _banks = new ();
        private readonly List<Client> _clients = new ();

        public void SaveState()
        {
            using StreamWriter banksFile
                = File.CreateText(@"C:\Users\djhit\RiderProjects\Kerenery\Banks\Snapshots\BanksState.json");
            using StreamWriter clientsFile
                = File.CreateText(@"C:\Users\djhit\RiderProjects\Kerenery\Banks\Snapshots\CilentsState.json");
            JsonSerializer banksSerializer = new ();
            banksSerializer.Serialize(banksFile, _banks);
            JsonSerializer clientsSerializer = new ();
            clientsSerializer.Serialize(clientsFile, _clients);
        }

        public void LoadState()
        {
            using StreamReader banksReader =
                new (@"C:\Users\djhit\RiderProjects\Kerenery\Banks\Snapshots\BanksState.json");
        }

        public Bank AddBank(string bankName)
        {
            if (string.IsNullOrWhiteSpace(bankName))
                throw new BanksException("name is empty");
            if (_banks.Keys.Any(b => b.Name == bankName))
                throw new BanksException("such bank is already registered");

            var bank = new Bank() { Name = bankName };
            _banks.Add(bank, new List<IAccount>());

            return bank;
        }

        public Client RegisterClient(Client client)
        {
            if (_clients.Any(c => c.Id == client.Id))
                throw new BanksException("such client is already added");

            _clients.Add(client);
            return client;
        }
    }
}
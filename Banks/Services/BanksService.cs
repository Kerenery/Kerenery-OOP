using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Banks.Accounts;
using Banks.Models;
using Banks.Tools;
using Banks.Transactions;
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

        public IAccount RegisterAccount(Guid holderId, AccountType accountType, Bank bank, Balance balance)
        {
            if (_clients.All(c => c.Id != holderId)) throw new BanksException("there is no such client");

            if (!Enum.IsDefined(typeof(AccountType), accountType)) throw new BanksException("accountType is not defined");

            if (_banks.Keys.All(b => b.Id != bank.Id)) throw new BanksException("there is no such bank");

            IAccount account = accountType switch
            {
                AccountType.Debit => new DebitAccount(balance, holderId),
                AccountType.Deposit => new DepositAccount(balance, holderId, DateTime.Today.AddMonths(1)),
                AccountType.Credit => new CreditAccount(balance, holderId, bank.CreditLimit),
                _ => throw new BanksException("unknown account type")
            };

            _banks[bank].Add(account);

            return account;
        }

        public IAccount GetAccount(Guid holderId, Bank bank)
        {
            if (_banks.Keys.All(b => b.Id != bank.Id))
                throw new BanksException("there is no such bank");

            return _banks[bank].FirstOrDefault(ac => ac.HolderId == holderId)
                   ?? throw new BanksException("there is no such account");
        }

        public decimal Withdraw(IAccount account, decimal money)
        {
            var acc = _banks.Values.First().FirstOrDefault(ac => ac.AccountId == account.AccountId)
                      ?? throw new BanksException("there is no such account");

            var bank = _banks.FirstOrDefault(x => x.Value.Contains(acc)).Key
                       ?? throw new BanksException("such bank does not exist");

            var debitWithdraw = new DebitReplenishHandler(bank);
            var creditWithdraw = new CreditReplenishHandler(bank);
            var depositWithdraw = new DepositReplenishHandler(bank);
            debitWithdraw.SetNext(creditWithdraw).SetNext(depositWithdraw);
            return debitWithdraw.Handle(money, account);
        }

        public decimal Transfer(decimal money, Guid senderId, Guid receiverId)
        {
            var firstAcc = _banks.Values.First().FirstOrDefault(ac => ac.AccountId == senderId)
                      ?? throw new BanksException("there is no such account");
            var secondAcc = _banks.Values.First().FirstOrDefault(ac => ac.AccountId == receiverId)
                           ?? throw new BanksException("there is no such account");
            var senderBank = _banks.FirstOrDefault(x => x.Value.Contains(firstAcc)).Key
                       ?? throw new BanksException("such bank does not exist");
            var receiverBank = _banks.FirstOrDefault(x => x.Value.Contains(secondAcc)).Key
                             ?? throw new BanksException("such bank does not exist");

            var debitTransfer = new WithdrawHandler(senderBank, receiverBank);
            var creditTransfer = new CreditTransactionHandler(senderBank, receiverBank);
            var depositTransfer = new DepositTransactionHandler(senderBank, receiverBank);
            debitTransfer.SetNext(creditTransfer).SetNext(depositTransfer);
            return debitTransfer.Handle(money, firstAcc, secondAcc);
        }
    }
}
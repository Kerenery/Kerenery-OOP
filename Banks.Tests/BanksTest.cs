using System;
using Banks.Accounts;
using Banks.Models;
using Banks.Services;
using Banks.SnapShot;
using Banks.Tools;
using Banks.Transactions;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTest
    {
        [Test]
        public void ResponsibilityCheck()
        {
            var client = ClientBuilder.Init()
                .SetName("Nick")
                .SetSecondName("Kondratev")
                .SetAddress("Pushkin District Underground")
                .SetPassportData("14882281488")
                .Build();
            var client2 = ClientBuilder.Init()
                .SetName("Json")
                .SetSecondName("Stat")
                .SetAddress("Pushkin")
                .SetPassportData("1339213231")
                .Build();
            var bankService = new BanksService();
            bankService.AddBank("PetroMoney10");
            bankService.AddBank("PetroMoney20", commission: Convert.ToDecimal(0.15));
            bankService.RegisterClient(client);
            bankService.RegisterAccount(client.Id, AccountType.Debit, bankService.FindBank("PetroMoney10"), new Balance());
            bankService.RegisterClient(client2);
            bankService.RegisterAccount(client2.Id, AccountType.Debit, bankService.FindBank("PetroMoney20"), new Balance());
            bankService.Withdraw(bankService.GetAccount(client.Id,bankService.FindBank("PetroMoney10")), -1000);
            bankService.Transfer(500, bankService.GetAccount(client.Id, bankService.FindBank("PetroMoney10")).AccountId, 
                bankService.GetAccount(client2.Id, bankService.FindBank("PetroMoney20")).AccountId);
            Console.WriteLine(bankService.GetAccount(client.Id, bankService.FindBank("PetroMoney10")).CurrentBalance.WithdrawBalance);
            Console.WriteLine(bankService.GetAccount(client2.Id, bankService.FindBank("PetroMoney20")).CurrentBalance.WithdrawBalance);
        }

        [Test]
        public void InterestAccrual()
        {
            var client = ClientBuilder.Init()
                .SetName("Nick")
                .SetSecondName("Kondratev")
                .SetAddress("Pushkin District Underground")
                .SetPassportData("14882281488")
                .Build();

            var bankService = new BanksService();
            bankService.AddBank("sberchick", rate: Convert.ToDecimal(0.01));
            bankService.RegisterClient(client);
            bankService.RegisterAccount(client.Id, AccountType.Deposit, bankService.FindBank("sberchick"), new Balance() {FixedBalance = 20});
            bankService.InterestAccrual();
            Console.WriteLine(bankService.GetAccount(client.Id, bankService.FindBank("sberchick")).CurrentBalance.WholeBalance);
        }

        [Test]
        public void AddBankState()
        {
            var bankService = new BanksService();
            bankService.AddBank("JOJOREFERENCE", 0M, 0.4M, 0.3M);
            bankService.SaveState();
        }
    }
}
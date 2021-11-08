using System;
using Banks.Accounts;
using Banks.Models;
using Banks.Services;
using Banks.SnapShot;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTest
    {
        [Test]
        public void BuildClient()
        {
            var client = ClientBuilder.Init()
                .SetName("Nick")
                .SetSecondName("Kondratev")
                .SetAddress("Pushkin District Underground")
                .SetPassportData("14882281488")
                .Build();
            
            Assert.AreEqual("Nick", client.Name);
        }

        [Test]
        public void MementoWorkCheck()
        {
            var account = new DebitAccount(new Balance() { FixedBalance = 15, WithdrawBalance = 30 });
            Caretaker caretaker = new Caretaker() { Account = account };
            caretaker.Backup();
            account.Replenish(500);
            Console.WriteLine(account.CurrentBalance.WholeBalance);
            caretaker.Undo();
            Console.WriteLine(account.CurrentBalance.WholeBalance);
        }

        [Test]
        public void CreditMementoFieldCheck()
        {
            var creditAcc = new CreditAccount(new Balance() {FixedBalance = 12, WithdrawBalance = 50}, 1000);
            Caretaker caretaker = new Caretaker() { Account = creditAcc };
            caretaker.Backup();
            Console.WriteLine(creditAcc.CurrentBalance.WholeBalance);
            creditAcc.Replenish(500);
            Console.WriteLine(creditAcc.CurrentBalance.WholeBalance);
            caretaker.Undo();
            Console.WriteLine(creditAcc.CurrentBalance.WholeBalance);
        }

        [Test]
        public void SerializeUnSerializable()
        {
            var bankService = new BanksService();
            var client = ClientBuilder.Init()
                .SetName("Nick")
                .SetSecondName("Kondratev")
                .SetAddress("Pushkin District Underground")
                .SetPassportData("14882281488")
                .Build();
            bankService.RegisterClient(client);
            bankService.AddBank("PetroMoney1");
            bankService.AddBank("PetroMoney2");
            bankService.AddBank("PetroMoney3");
            bankService.AddBank("PetroMoney4");
            bankService.AddBank("PetroMoney5");
            bankService.SaveState();
            bankService.LoadState();
            Console.WriteLine(bankService.FindBank("PetroMoney5").Id);
        }
    }
}
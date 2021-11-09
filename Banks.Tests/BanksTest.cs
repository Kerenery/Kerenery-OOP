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

        [Test]
        public void ResponsibilityCheck()
        {
            var depAcc = new DepositAccount(new Balance() {FixedBalance = 2000, WithdrawBalance = 3000}, DateTime.Today);
            var credAcc = new CreditAccount(new Balance() {FixedBalance = 2000, WithdrawBalance = 3000}, 5000);
            var bankService = new BanksService();
            bankService.AddBank("PetroMoney10");
            bankService.AddBank("PetroMoney20");
            var fhandler = new WithdrawHandler(bankService.FindBank("PetroMoney10"),
                bankService.FindBank("PetroMoney20"));
            var shandler = new DepositTransactionHandler(bankService.FindBank("PetroMoney10"),
                bankService.FindBank("PetroMoney20"));
            var thandler = new CreditTransactionHandler(bankService.FindBank("PetroMoney10"),
                bankService.FindBank("PetroMoney20"));
            fhandler.SetNext(shandler).SetNext(thandler);
            var result = fhandler.Handle(100, depAcc, credAcc);
            Console.WriteLine(credAcc.CurrentBalance.WholeBalance);
            Console.WriteLine(depAcc.CurrentBalance.WholeBalance);
        }
    }
}
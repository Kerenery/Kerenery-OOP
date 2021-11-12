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
        private BanksService _banksService;
        private Client _firstClient;
        private Client _secondClient;
        private Bank _firstBank;
        private Bank _secondBank;

        [SetUp]
        public void Setup()
        {
            _banksService = new BanksService();

            _firstClient = ClientBuilder.Init()
                .SetName("URARAKA")
                .SetSecondName("MANIKULA")
                .SetAddress("DUCKDAPOLICE")
                .SetPassportData("2281488228")
                .Build();

            _secondClient = ClientBuilder.Init()
                .SetName("BEBRITOSA")
                .SetSecondName("NABALA")
                .SetAddress("DUCKDAPOLICE")
                .SetPassportData("2281488228")
                .Build();

            _firstBank = _banksService.AddBank("PERISCOPE", 0.2m, 3000m, 0.15m);
            _secondBank = _banksService.AddBank("DALASITO", 0.15m, 9000m, 0.135m);

            _banksService.RegisterClient(_firstClient);
            _banksService.RegisterClient(_secondClient);
        }

        [Test]
        public void CreateClient()
        {
            var client = ClientBuilder.Init()
                .SetName("JOJO")
                .SetSecondName("REFERENCE")
                .SetAddress("UNDERGROUND")
                .SetPassportData("14882281488")
                .Build();

            Assert.AreEqual("JOJO", client.Name);
        }

        [Test]
        public void InterestAccrual_SkipMonth()
        {
            var account = _banksService.RegisterAccount(_firstClient.Id, AccountType.Deposit, _firstBank, new Balance()
                { FixedBalance = 200, WithdrawBalance = 5000 });

            var moneyBeforeTimeTravel = account.CurrentBalance.WholeBalance;

            _banksService.SkipMonth();

            var moneyAfterTimeTravel = account.CurrentBalance.WholeBalance;

            Assert.AreNotEqual(moneyBeforeTimeTravel, moneyAfterTimeTravel);

            _banksService.SkipMonth();

            var moneyAfterTwoMonth = account.CurrentBalance.WholeBalance;

            Assert.AreNotEqual(moneyAfterTimeTravel, moneyAfterTwoMonth);
        }

        [Test]
        public void Withdraw_NotEnoughMoney_ThrowException()
        {
            var account = _banksService.RegisterAccount(_firstClient.Id, AccountType.Debit, _firstBank,
                new Balance() { WithdrawBalance = 400 });

            Assert.Catch<BanksException>(() =>
            {
                _banksService.Withdraw(account, 402);
            });
        }

        [Test]
        public void WithdrawOperation_DepositAccountPayDay_ThrowException()
        {
            var firstAccount = _banksService.RegisterAccount(_firstClient.Id, AccountType.Deposit, _firstBank,
                new Balance() { WithdrawBalance = 400 });

            Assert.Catch<BanksException>((() =>
            {
                _banksService.Withdraw(firstAccount, 300);
            }));
        }

        [Test]
        public void TransferOperation_DebitToCredit_NotEnoughMoney_ThrowException()
        {
            var firstAccount = _banksService.RegisterAccount(_firstClient.Id, AccountType.Debit, _firstBank,
                new Balance() { WithdrawBalance = 60123 });

            var secondAccount = _banksService.RegisterAccount(_secondClient.Id, AccountType.Credit, _secondBank,
                new Balance() { WithdrawBalance = 128000 });

            Assert.Catch<BanksException>(() =>
            {
                _banksService.Transfer(900000, _firstClient.Id, _secondClient.Id);
            });
        }

        [Test]
        public void TransferOperation_CreditToCredit_LimitMoment()
        {
            var firstAccount = _banksService.RegisterAccount(_firstClient.Id, AccountType.Credit, _firstBank,
                new Balance() { WithdrawBalance = 200 });

            var secondAccount = _banksService.RegisterAccount(_secondClient.Id, AccountType.Credit, _secondBank,
                new Balance() { WithdrawBalance = 2090 });

            _banksService.Transfer(2000, _firstClient.Id, _secondClient.Id);
        }
    }
}
using System;
using Xunit;
using TransferService;

namespace TransferServiceUnitTests
{
    public class AccountUnitTests
    {
        private const int AccountNumber = 123456789;

        [Fact]
        public void ConstructorSetsInitialAccountBalance()
        {
            const double balance = 53.75;

            var acc = new Account(AccountNumber, balance);

            Assert.Equal(balance, acc.Balance);
        }

        [Fact]
        public void Deposit_SomeAmount_IncreasesBalanceByAmount()
        {
            const double initialBalance = 53.75;
            var acc = new Account(AccountNumber, initialBalance);

            acc.Deposit(24.13);

            Assert.Equal(77.88, acc.Balance);
        }

        [Fact]
        public void Withdraw_AccountHasSufficientBalance_ReducesBalanceByTheAmountOfTheWithdrawal()
        {
            const double initialBalance = 77.88;
            var acc = new Account(AccountNumber, initialBalance);

            acc.Withdraw(24.13);

            Assert.Equal(53.75, acc.Balance);
        }
    }
}

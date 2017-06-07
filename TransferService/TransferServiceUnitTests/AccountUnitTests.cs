using Xunit;
using TransferService;
using System;

namespace TransferServiceUnitTests
{
    public class AccountUnitTests
    {
        private const int AccountNumber = 123456789;

        private static void AssertEqual(double lhs, double rhs)
        {
            Assert.True(Math.Abs(lhs - rhs) < 1e-6);
        }

        [Fact]
        public void ConstructorSetsInitialAccountBalance()
        {
            const double balance = 53.75;

            var acc = new Account(AccountNumber, balance);

            AssertEqual(balance, acc.Balance);
        }

        [Fact]
        public void CreateAccount_ZeroBalance_Allowed()
        {
            var acc = new Account(AccountNumber, 0.0);

            AssertEqual(0.0, acc.Balance);
        }

        [Fact]
        public void CreateAccount_NegativeBalance_Allowed()
        {
            Account acc = new Account(AccountNumber, -0.01);
            AssertEqual(-0.01, acc.Balance);
        }

        [Fact]
        public void Deposit_SomeAmount_IncreasesBalanceByAmount()
        {
            const double initialBalance = 53.75;
            var acc = new Account(AccountNumber, initialBalance);

            acc.Deposit(24.13);

            AssertEqual(77.88, acc.Balance);
        }

        [Fact]
        public void Withdraw_AccountHasSufficientBalance_ReducesBalanceByTheAmountOfTheWithdrawal()
        {
            const double initialBalance = 77.88;
            var acc = new Account(AccountNumber, initialBalance);

            acc.Withdraw(24.13);

            AssertEqual(53.75, acc.Balance);
        }

        [Fact]
        public void Withdraw_WillOverdrawAccount_Allowed()
        {
            const double initialBalance = 77.88;
            var acc = new Account(AccountNumber, initialBalance);

            acc.Withdraw(initialBalance + 0.01);

            AssertEqual(-0.01, acc.Balance);
        }
    }
}

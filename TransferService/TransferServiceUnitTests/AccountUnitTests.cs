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
        public void CreateAccount_ZeroBalance_Allowed()
        {
            var acc = new Account(AccountNumber, 0.0);

            Assert.Equal(0.0, acc.Balance);
        }

        [Fact]
        public void CreateAccount_NegativeBalance_Throws()
        {
            Account acc;
            Assert.Throws<Account.AccountOverdrawnException>(
                    () => acc = new Account(AccountNumber, -0.01)
                );
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

        [Fact]
        public void Withdraw_WillOverdrawAccount_Throws()
        {
            const double initialBalance = 77.88;
            var acc = new Account(AccountNumber, initialBalance);

            Assert.Throws<Account.AccountOverdrawnException>(
                () => acc.Withdraw(initialBalance + 0.01)
            );
        }
    }
}

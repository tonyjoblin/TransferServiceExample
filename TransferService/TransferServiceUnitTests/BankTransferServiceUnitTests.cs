using Xunit;
using TransferService;

namespace TransferServiceUnitTests
{
    public class BankTransferServiceUnitTests
    {
        private const int FromAccountNumber = 12345;
        private const int ToAccountNumber = 6789;
        
        [Fact]
        public void Transfer_NullFromAccount_Throws()
        {
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            Assert.Throws<BankTransferService.TransferException>(() => transferOk = BankTransferService.Transfer(null, toAccount, 24.13));
        }

        [Fact]
        public void Transfer_NullToAccount_Throws()
        {
            var fromAccount = new Account(FromAccountNumber, 53.75);
            bool transferOk = false;

            Assert.Throws<BankTransferService.TransferException>(
                () => transferOk = BankTransferService.Transfer(fromAccount, null, 24.13)
                );
        }

        [Fact]
        public void Transfer_ValidAccountsSufficientBalance_ReturnsTrue()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 24.13);

            Assert.True(transferOk);
        }

        [Fact]
        public void Transfer_ValidAccountsSufficientBalance_FromAccountIsReduced()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 24.13);

            Assert.Equal(53.75, fromAccount.Balance);
        }

        [Fact]
        public void Transfer_ValidAccountsSufficientBalance_ToAccountIsIncreased()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 24.13);

            Assert.Equal(77.88, toAccount.Balance);
        }

        [Fact]
        public void Transfer_ValidAccountsInsufficientBalance_ReturnsFalse()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 77.89);

            Assert.False(transferOk);
        }

        [Fact]
        public void Transfer_ValidAccountsInsufficientBalance_FromAccountUnchanged()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 77.89);

            Assert.Equal(77.88, fromAccount.Balance);
        }

        [Fact]
        public void Transfer_ValidAccountsInsufficientBalance_ToAccountUnchanged()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 77.89);

            Assert.Equal(53.75, toAccount.Balance);
        }

        [Fact]
        public void Transfer_TransferEntireAccountBalance_Allowed()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 77.88);

            Assert.True(transferOk);
            Assert.Equal(0.0, fromAccount.Balance);
        }
    }
}

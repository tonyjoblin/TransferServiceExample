using System;
using Xunit;
using TransferService;

namespace TransferServiceUnitTests
{
    public class BankTransferServiceUnitTests
    {
        private const int FromAccountNumber = 12345;
        private const int ToAccountNumber = 6789;

        private static void AssertEqual(double lhs, double rhs)
        {
            Assert.True(Math.Abs(lhs - rhs) < 1e-6);
        }

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

            AssertEqual(53.75, fromAccount.Balance);
        }

        [Fact]
        public void Transfer_ValidAccountsSufficientBalance_ToAccountIsIncreased()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 24.13);

            AssertEqual(77.88, toAccount.Balance);
        }

        [Fact]
        public void Transfer_OverdrawFromPositiveAccount_AllowedReturnsTrue()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 77.89);

            Assert.True(transferOk);
        }

        [Fact]
        public void Transfer_OverdrawFromPositiveAccount_FromAccountChargedFee()
        {
            var fromAccount = new Account(FromAccountNumber, 100.0);
            var toAccount = new Account(ToAccountNumber, 0.0);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 200.0);

            AssertEqual(-102.0, fromAccount.Balance);
        }

        [Fact]
        public void Transfer_OverdrawFromPositiveAccount_FromAccountChargedFeeRoundsUp()
        {
            var fromAccount = new Account(FromAccountNumber, 0.50);
            var toAccount = new Account(ToAccountNumber, 0.0);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 0.90);

            AssertEqual(-0.41, fromAccount.Balance);
        }

        [Fact]
        public void Transfer_OverdrawFromPositiveAccount_ToAccountCredited()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 77.89);

            AssertEqual(131.64, toAccount.Balance);
        }

        [Fact]
        public void Transfer_OverdrawFromOverdrawnAccount_NotAllowedReturnsFalse()
        {
            var fromAccount = new Account(FromAccountNumber, -0.01);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 77.89);

            Assert.False(transferOk);
        }

        [Fact]
        public void Transfer_OverdrawFromEmptyAccount_Allowed()
        {
            var fromAccount = new Account(FromAccountNumber, 0.0);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 77.89);

            Assert.True(transferOk);
        }

        [Fact]
        public void Transfer_TransferEntireAccountBalance_Allowed()
        {
            var fromAccount = new Account(FromAccountNumber, 77.88);
            var toAccount = new Account(ToAccountNumber, 53.75);
            bool transferOk = false;

            transferOk = BankTransferService.Transfer(fromAccount, toAccount, 77.88);

            Assert.True(transferOk);
            AssertEqual(0.0, fromAccount.Balance);
        }
    }
}

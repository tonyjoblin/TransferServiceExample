
namespace TransferService
{
    public class BankTransferService
    {
        public class TransferException : System.Exception
        { }

        public static bool Transfer(Account from, Account to, double transferAmount)
        {
            if (from == null || to == null)
            {
                throw new TransferException();
            }
            if (from.Balance < transferAmount)
            {
                return false;
            }
            from.Withdraw(transferAmount);
            to.Deposit(transferAmount);
            return true;
        }
    }
}

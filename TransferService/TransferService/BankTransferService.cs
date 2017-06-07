
namespace TransferService
{
    public class BankTransferService
    {
        public class TransferException : System.Exception
        { }

        private static double CalculateOverdrawnAccountFee(Account acc)
        {
            if (acc.Balance < 0.0)
            {
                double fee = System.Math.Abs(acc.Balance) * 0.02;
                fee = System.Math.Round(fee, 2, System.MidpointRounding.AwayFromZero);
                return fee;
            }
            return 0.0;
        }

        /// <summary>
        /// Transfers money from the 'from' account to the 'to' account.
        /// Overdraws are charged a fee.
        /// May throw an exception if the accounts are not valid.
        /// </summary>
        /// <param name="from">Account from where the money is withdrawn</param>
        /// <param name="to">Account where money is deposited</param>
        /// <param name="transferAmount">The amount of money transfered, must be positive.</param>
        /// <returns>False if the transaction is not allowed.</returns>
        public static bool Transfer(Account from, Account to, double transferAmount)
        {
            if (from == null || to == null)
            {
                throw new TransferException();
            }
            if (transferAmount < 0.0)
            {
                return false;
            }
            if (from.Balance < 0.0)
            {
                return false;
            }
            from.Withdraw(transferAmount);
            if (from.Balance < 0.0)
            {
                double fee = CalculateOverdrawnAccountFee(from);
                from.Withdraw(fee);
            }
            to.Deposit(transferAmount);
            return true;
        }
    }
}

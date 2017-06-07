
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

        public static bool Transfer(Account from, Account to, double transferAmount)
        {
            if (from == null || to == null)
            {
                throw new TransferException();
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

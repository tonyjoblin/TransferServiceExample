using System;
using System.Collections.Generic;
using System.Text;

namespace TransferService
{
    public class Account
    {
        public class AccountOverdrawnException: System.Exception
        {}

        private class BalanceValue
        {
            private double m_value;
            public double Value
            {
                get { return m_value; }
                set
                {
                    if (value < 0.0)
                    {
                        throw new AccountOverdrawnException();
                    }
                    m_value = value;
                }
            }
        }
        private readonly BalanceValue balance = new BalanceValue();
        private int accountNumber;

        public Account(int accountNumber, double balance)
        {
            this.balance.Value = balance;
            this.accountNumber = accountNumber;
        }
        public double Balance
        {
            get { return this.balance.Value; }
        }
        public void Withdraw(double amount)
        {
            this.balance.Value -= amount;
        }
        public void Deposit(double amount)
        {
            this.balance.Value += amount;
        }
    }
}

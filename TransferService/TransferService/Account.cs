using System;
using System.Collections.Generic;
using System.Text;

namespace TransferService
{
    public class Account
    {
        private double balance;
        private int accountNumber;

        public Account(int accountNumber, double balance)
        {
            this.balance = balance;
            this.accountNumber = accountNumber;
        }
        public double Balance
        {
            get { return this.balance; }
        }
        public void Withdraw(double amount)
        {
            this.balance -= amount;
        }
        public void Deposit(double amount)
        {
            this.balance += amount;
        }
    }
}

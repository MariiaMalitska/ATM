using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Account
    {
        public string ClientName { get; }
        private string TelNumb { get; }//not string but pointer to Telephone object?
        public decimal Cash { get; private set; }

        public Account(string clientName, string telNumb, decimal cash)
        {
            ClientName = clientName;
            TelNumb = telNumb;
            Cash = cash;
        }

        //add money to cash
        public void AddToCash(decimal money) {
            if (money < 0) throw new ArgumentException("Sum of money to transfer cannot be less than zero");
            Cash += money;
        }

        //wisdraw money, if not enough throw an exception
        public void WithdrawMoney(decimal money){
            if (money < 0) throw new ArgumentException("Sum of money to transfer cannot be less than zero");
            if (money > Cash)
                throw new NoEnoughMoneyException("Not enough money on balance to withdraw the specified sum.");
             Cash -= money;
        }
    }

    class NoEnoughMoneyException : Exception
    {
        public NoEnoughMoneyException() : base() { }
        public NoEnoughMoneyException(string message) : base(message) { }
        public NoEnoughMoneyException(String message, Exception innerException)
            : base(message, innerException) { }
        protected NoEnoughMoneyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}

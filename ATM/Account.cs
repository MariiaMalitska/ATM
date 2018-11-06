using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Account
    {
        public string ClientName { get; }
        private string TelNumb { get; }//not string but pointer to Telephone object?
        public double Cash { get; private set; }

        public Account(string clientName, string telNumb, double cash)
        {
            ClientName = clientName;
            TelNumb = telNumb;
            Cash = cash;
        }

        //add money to cash
        public void AddToCash(double money) {
            if (money < 0) throw new ArgumentException("Sum of money to transfer cannot be less than zero");
            Cash += money;
        }

        //if enough money wisdraw specified sum and return true, else just return false
        public bool TryWisdrawMoney(double money){
            if (money < 0) throw new ArgumentException("Sum of money to transfer cannot be less than zero");
            if (money > Cash)
                return false;
            Cash -= money;
            return true;
        }

    }
}

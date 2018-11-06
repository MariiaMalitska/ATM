using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Telephone
    {
        public string Number { get; private set; }
        public double Balance { get; private set; }

        //adds money to telephone balance
        public void AddToBalance(double money)
        {
            if (money < 0)
                throw new ArgumentException("Money to put on telephone balance can't be value less than 0");
            Balance += money;
        }
    }
}

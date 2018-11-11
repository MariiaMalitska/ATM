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
        public decimal Balance { get; private set; }

        public Telephone(string telNumb,decimal balance)
        {
            if (balance < 0)
                throw new ArgumentException("Telephone balance cannot be less than 0");
            if (!HasCorrFormat(telNumb))
                throw new ArgumentException("Telephone number can contain only digits.");
            Number = telNumb;
            Balance = balance;
        }

        //adds money to telephone balance
        public void AddToBalance(decimal money)
        {
            if (money < 0)
                throw new ArgumentException("Money to put on telephone balance can't be value less than 0");
            Balance += money;
        }

        private static bool HasCorrFormat(string telNumb)
        {
            foreach (char ch in telNumb)
                if (!Char.IsDigit(ch))
                    return false;
            return true;
        }
    }
}

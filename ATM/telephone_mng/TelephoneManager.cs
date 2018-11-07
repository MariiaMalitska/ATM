using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class TelephoneManager//manages all operations with telephones
    {
        Dictionary<double, double> commisions = new Dictionary<double, double>()
        {
            {50, 1},
            {100, 2},
            {150, 3},
            {200, 4},
            {int.MaxValue, 5}

        };//for example, if add to balance [1,50] grn, commision 1 grn, [51-100] - 2grn...

        List<Telephone> telephones = new List<Telephone>();

        public TelephoneManager()
        {
            AddTestTelNumbers();
        }

        //put money on telephone balance, if telephone is not valid, throw exception
        public void AddToTelephonebalance(double money, string telNumb)
        {
            if (!TelephoneValid(telNumb))
                throw new ArgumentException("Telephone is not valid");
            telephones.Find((Telephone t) => { return t.Number == telNumb; }).AddToBalance(money);
        }

        //return true if telephone valid( ATM stores information about it)
        public bool TelephoneValid(string telNumb)
        {
            return telephones.Exists((Telephone t) => { return t.Number == telNumb; });
        }

        //returns commision for adding money to telephone balance
        public double GetTelephoneCommision(double money)
        {
            if (commisions.TryGetValue(money, out double comm))
                return comm;
            else return 0;
        }

        //problem to consider: how to link numbers of accounts stored in bank with telephone manager?
        private void AddTestTelNumbers()
        {
            telephones.Add(new Telephone("0123456789",43));
            telephones.Add(new Telephone("0987654321",5));
            telephones.Add(new Telephone("0992345455", 1));
            telephones.Add(new Telephone("0111111111", 0));
        }
    }
}

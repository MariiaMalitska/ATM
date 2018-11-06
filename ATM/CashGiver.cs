using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class CashGiver
    {
        Dictionary<Banknote, int> banknotes = new Dictionary<Banknote, int>();

        public CashGiver(int amountOf10, int amountOf20, int amountOf50, int amountOf100, int amountOf200, int amountOf500)
        {
            SetAmountForBanknotes(amountOf10, amountOf20, amountOf50, amountOf100, amountOf200, amountOf500);
        }

        /*in order not to write  
            banknotes.Add(Banknote.TEN, amountOf10);
            banknotes.Add(Banknote.TWENTY, amountOf20);
            ..... 6 times
            */
        private void SetAmountForBanknotes(params int[] values)
        {
            Banknote[] bn = (Banknote[])Enum.GetValues(typeof(Banknote));
            for (int i = 0; i < bn.Length; i++)
            {
                if (values[i] < 0) throw new ArgumentException("Amount of baknotes can't be less than 0.");
                banknotes.Add(bn[i], values[i]);
            }
        }

        //checks if ATM has enough amount of necessary banknotes to give money
        public bool CanGive(int money)
        {
           return TryGiveMoney(money).Count > 0;
        }

        //return money, using banknotes in ATM, decreases amount of banknotes in ATM
        public Dictionary<Banknote,int> GiveMoney(int money)
        {
            Dictionary<Banknote, int> bs = TryGiveMoney(money);
            foreach(KeyValuePair<Banknote,int> b in bs)//decrease amount of banknotes in ATM
            {
                banknotes.TryGetValue(b.Key, out int amount);
                banknotes[b.Key] = amount - b.Value;
            }
            return bs;
        }

        //returns list of sums to suggest user to wisdraw
        public int[] GetAvailableSums()
        {
            //maybe, inside the class we can have list of suggested sums and in this method we check 
            // what of them can be givn right now and return?
            throw new System.NotImplementedException();
        }

        //if ATM can give specified sum, returns neccessary amount of each banknote,othervise returns empty dictionary
        private Dictionary<Banknote, int> TryGiveMoney(int money)
        {
            throw new System.NotImplementedException();
        }

    }
}

             using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class CashGiver
    {
        private Dictionary<Banknote, int> banknotes = new Dictionary<Banknote, int>();//banknote type and its amount in ATM
        private List<int> sumsToSuggest = new List<int>() { 50, 100, 150, 200, 250, 500 };//list of sums to suggest client
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

        //return money, using banknotes in ATM, decreases amount of banknotes in ATM; if cannot give money throws an error
        public Dictionary<Banknote, int> GiveMoney(int money)
        {
            Dictionary<Banknote, int> bs = TryGiveMoney(money);
            if (bs.Count == 0)
                throw new NoBanknotesException("Cannot give specified amount of money.");
            foreach (KeyValuePair<Banknote, int> b in bs)//decrease amount of banknotes in ATM
            {
                banknotes.TryGetValue(b.Key, out int amount);
                banknotes[b.Key] = amount - b.Value;
            }
            return bs;
        }

        //returns list of sums to suggest user to wisdraw
        public List<int> GetAvailableSums()
        {
            List<int> res = new List<int>();
            foreach (int sum in sumsToSuggest)
            {
                if (CanGive(sum))
                    res.Add(sum);
            }
            return res;
        }

        //if ATM can give specified sum, returns neccessary amount of each banknote,othervise returns empty dictionary
        private Dictionary<Banknote, int> TryGiveMoney(int money)
        {
            if (money < 0)
                throw new ArgumentException("Amout of money to be given by ATN cannot be less than 0.");
            Dictionary<Banknote, int> toGive = new Dictionary<Banknote, int>();//banknotes to give
            Banknote[] bn = GetBanknotesInDescendingOrder();//types of banknotes in descending order, from bigger to smaller
            int bAmount = 0, howMuch = 0;
            int monToGiv = money;
            Banknote b;
            for (int i = 0; i < bn.Length && monToGiv > 0; i++)
            {
                b = bn[i];
                bAmount = banknotes[b];
                howMuch = monToGiv / (int)b;
                if (howMuch > 0 && howMuch <= bAmount)
                {
                    toGive.Add(b, howMuch);
                    monToGiv -= (int)b * howMuch;
                }
            }
            if (monToGiv > 0)
                toGive.Clear();
            return toGive;
        }

        private static Banknote[] GetBanknotesInDescendingOrder()
        {
            Banknote[] bn = (Banknote[])Enum.GetValues(typeof(Banknote));
            Comparison<Banknote> comp = new Comparison<Banknote>((x, y) => y.CompareTo(x));
            Array.Sort<Banknote>(bn, comp);
            return bn;
        }
    }

    class NoBanknotesException : Exception
    {
        public NoBanknotesException() : base() { }
        public NoBanknotesException(string message) : base(message) { }
        public NoBanknotesException(String message, Exception innerException)
            : base(message, innerException) { }
        protected NoBanknotesException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    //make class static or not?
     class CardTypeManager
    {
        //information about commisions fro wisdrawing money for each type of cards
        private   Dictionary<CardType, double> wisdrawCommisions = new Dictionary<CardType, double>();
        //information about commisions for transfering money for each type of cards
        private  Dictionary<CardType, double> transferCommisions = new Dictionary<CardType, double>();

        public CardTypeManager()
        {
            wisdrawCommisions.Add(CardType.VISA,1);
            wisdrawCommisions.Add(CardType.MASTERCARD, 1.5);
            transferCommisions.Add(CardType.VISA,2);
            transferCommisions.Add(CardType.MASTERCARD,1.5);
        }

        //returns wisdraw commision for specified type of cards
        public  double GetWisdrawCommision(CardType type)
        {
            if (wisdrawCommisions.TryGetValue(type, out double comm))
                return comm;
            else return 0;
        }

        //returns transfer commision for specified type of cards
        public  double GetTransferCommision(CardType type)
        {
            if (transferCommisions.TryGetValue(type, out double comm))
                return comm;
            else return 0;
        }
    }
}

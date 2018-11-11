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
        private   Dictionary<CardType, decimal> wisdrawCommisions = new Dictionary<CardType, decimal>();
        //information about commisions for transfering money for each type of cards
        private  Dictionary<CardType, decimal> transferCommisions = new Dictionary<CardType, decimal>();

        public CardTypeManager()
        {
            wisdrawCommisions.Add(CardType.VISA,(decimal)1.0);
            wisdrawCommisions.Add(CardType.MASTERCARD, (decimal)1.5);
            transferCommisions.Add(CardType.VISA, (decimal)2.0);
            transferCommisions.Add(CardType.MASTERCARD, (decimal)1.5);
        }

        //returns wisdraw commision for specified type of cards
        public  decimal GetWisdrawCommision(CardType type)
        {
            if (wisdrawCommisions.TryGetValue(type, out decimal comm))
                return comm;
            else return decimal.Zero;
        }

        //returns transfer commision for specified type of cards
        public  decimal GetTransferCommision(CardType type)
        {
            if (transferCommisions.TryGetValue(type, out decimal comm))
                return comm;
            else return decimal.Zero;
        }
    }
}

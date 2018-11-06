using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{

    enum CardType
    {
        VISA,
        MASTERCARD
    }

    class Card
    {
        public const int maxPinAttempts = 3;//maximum amount of typing incorrect pin
        public const int blockedDays = 1;//amount of days on which card is blocked after attempts become equal maxPinAttempts
        private readonly Account myAccount;//reference to account to which card is attached

        public string CardNumber { get; }
        public CardType Type { get; }
        public string Pin { private get; set; }
        public int IncorrPinCount { get; private set; } = 0; //counter for attempts for current card
        public DateTime BlockedUntilDate { get; private set; } = DateTime.MinValue;//date until which card is blocked

        public Card(Account account, string cardNumber, CardType type, string pin)
        {
            myAccount = account;
            CardNumber = cardNumber;
            Type = type;
            Pin = pin;
        }

        //if given pin equals card pin returns true, else increases counter of attempts,
        //if it is equal or bigger than maximum amount of attempts, blocks card and sets counter of attempts to 0
        public bool IsMyPin(string pin)
        {
            if (Pin == pin)
                return true;
            else
            {
                if (++IncorrPinCount >= maxPinAttempts)
                {
                    BlockedUntilDate = DateTime.Now.AddDays(blockedDays);
                    IncorrPinCount = 0;
                }  
                return false;
            }

        }
       
        //if card is currently blockes returns true, else false
        public bool IsBlocked()
        {
           return DateTime.Now.CompareTo(BlockedUntilDate) < 0;
        }

        public double GetCash()
        {
            return myAccount.Cash;
        }

        public string GetClientName()
        {
            return myAccount.ClientName;
        }

        //add money to cash of account to which card is attached
        public void AddMoney(double money)
        {
            myAccount.AddToCash(money);
        }

        //if enough money wisdraws and return truue, else just return false
        public bool TryWisdrawMoney(double money)
        {
            return myAccount.TryWisdrawMoney(money);
        }
    }
}

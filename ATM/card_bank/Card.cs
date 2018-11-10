using System;

namespace ATM
{

    enum CardType
    {
        VISA,
        MASTERCARD
    }

    class Card
    {
        public const int cardNumbLength = 16;
        public const int pinLength = 4;
        public const int maxPinAttempts = 3;//maximum amount of typing incorrect pin
        public const int blockedDays = 1;//amount of days on which card is blocked after attempts become equal maxPinAttempts
        private readonly Account myAccount;//reference to account to which card is attached

        public string CardNumber { get; }
        public CardType Type { get; }
        private string Pin { get; set; }
        public int IncorrPinCount { get; private set; } = 0; //counter for attempts for current card
        public DateTime BlockedUntilDate { get; private set; } = DateTime.MinValue;//date until which card is blocked

        public Card(Account account, string cardNumber, CardType type, string pin)
        {
            if(!CardNumbOfCorrectFormat(cardNumber))
                throw new ArgumentException("Incorrect card format");
            if (!PinOfCorrectFormat(pin))
                throw new ArgumentException("Incorrect pin format");
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

        public void SetPin(string newPin)
        {
            if (!PinOfCorrectFormat(newPin))
                throw new ArgumentException("Incorrect pin format");
            Pin = newPin;
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

        // wisdraw money, if not enough throw an exception
        public void WithdrawMoney(double money)
        {
             myAccount.WithdrawMoney(money);
        }

        public static bool PinOfCorrectFormat(string pin)
        {
            return HasCorrFormat(pin, pinLength);
        }

        public static bool CardNumbOfCorrectFormat(string cardNumb)
        {
            return HasCorrFormat(cardNumb, cardNumbLength);
        }

        //return true if specified string has correct length and contains only of digits
        private static bool HasCorrFormat(string toCheck,int corrLength)
        {
            if (toCheck.Length > corrLength)
                return false;
            foreach (char ch in toCheck)
                if (!Char.IsDigit(ch))
                    return false;
            return true;
        }
    }
}

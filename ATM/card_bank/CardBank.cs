using System;
using System.Collections.Generic;

namespace ATM
{
    /*
     * 1) if consider only ATMs of one bank, then all infornation is  the same for all ATMs
     * in that case, maybe better to make static class?
     * 2)information about accounts and cards is stored in list
     * how then guarantee uniqueness? is it neccessary to guarantee it?
     * Mabe to think how to use HashSet for accounts and cards' storage and what comparators define for them?
    */
    class CardBank
    {
        private CardTypeManager cardTypeMng = new CardTypeManager(); //gets commision for different cards
        private List<Account> accounts = new List<Account>();//all accounts information stored in bank
        private List<Card> cards = new List<Card>();//all balk cards information
        private Card currCard = null; // card currently inserted

        public CardBank()
        {
            GenTestCardsAndAccounts();
        }

        //returns true if cardNumber is in CardBank database
        public bool IsCardValid(string cardNumb)
        {
            return cards.Exists((Card c) => { return c.CardNumber == cardNumb; });
        }

        public void RetrieveCard()
        {
            currCard = null; 
        }

        //if for specified card pin matches, sets the card as current in cardBank and returns true, else returns false
        public bool VerifyCard(string cardNumb, string pin)
        {
            Card insertedCard = GetCard(cardNumb);
            if (insertedCard.IsMyPin(pin))
            {
                currCard = insertedCard;
                return true;
            }
            return false;
        }

        //returns transfer commision for currently inserted card
        public double GetTransferCommision()
        {
            return cardTypeMng.GetTransferCommision(currCard.Type);
        }

        //returns wisdraw commision for currently inserted card
        public double GetWisdrawCommision()
        {
            return cardTypeMng.GetWisdrawCommision(currCard.Type);
        }

        public static double GetCommInMoney(double money, double CommInPercentage)
        {
            double commMon = money * CommInPercentage / 100;
            double commMonRounded = Math.Round(commMon * 100) / 100;
            return commMonRounded;
        }

        //if enough money on accont withdraws money else throw exception
        public void WithdrawMoney(double money, double commisionMoney)
        {
            if (commisionMoney < 0) throw new ArgumentException("Withdraw commision cannot be less tha 0.");
            double sum = money + commisionMoney;
            currCard.WithdrawMoney(sum);
        }
        
        //return true if it is possible to transfer money to the card, that is, when information about it is stored in ATM
        //and it is attached to account other than current card and it is not blocked
        public bool CardCorrForTransfer(string cardNumb)
        {
            return IsCardValid(cardNumb) && (GetCard(cardNumb).GetClientName() != currCard.GetClientName()) && !CardBlocked(cardNumb);
        }


        //transfers money, if not enough on card throws an NotEnoughMoneyException
        public void TransferMoney(double transfSum, string cardNumb )
        {
            if (transfSum < 0)
                throw new ArgumentException("Sum for transfering cannot be less than 0.");
            if (!CardCorrForTransfer(cardNumb))
                throw new ArgumentException("Card number not available for transfering.");
            //double commMon = transfSum * GetTransferCommision() / 100;
            //double commMonRounded = Math.Round(commMon * 100) / 100;
            double commMon = GetCommInMoney(transfSum, GetTransferCommision());
            WithdrawMoney(transfSum, commMon);
            GetCard(cardNumb).AddMoney(transfSum);
        }

        public string GetCurrCardNumber()
        {
            return currCard.CardNumber;
        }

        public string GetCurrCardClientName()
        {
            return currCard.GetClientName();
        }

        public double GetCurrCardBalance()
        {
            return currCard.GetCash();
        }

        public bool CardBlocked(string cardNumb)
        {
            return GetCard(cardNumb).IsBlocked();
        }

        public string GetCardClientName(string cardNumb)
        {
            return GetCard(cardNumb).GetClientName();
        }

        public void ChangePin(string newPin)
        {
            currCard.SetPin(newPin);
        }

        //return card by card number
        private Card GetCard(string cardNumb)
        {
            if (!IsCardValid(cardNumb))
                throw new ArgumentException("Not valid card.");
            return cards.Find((Card c) => { return c.CardNumber == cardNumb; });
        }

        //generate some tester values for bank clients
        private void GenTestCardsAndAccounts()
        {
            //not very good, how to make better initialization of information stored in bank?
            Account a1 = new Account("Rob Stark", "0123456789", 1000);
            Account a2 = new Account("Kate Brown", "0987654321", 235);
            Account a3 = new Account("TassleHoff BareFoot", "0992345455", 2347);
            accounts.Add(a1);
            accounts.Add(a2);
            accounts.Add(a3);

            //Rob has 2 cards
            cards.Add(new Card(a1,"1111222233334444",CardType.MASTERCARD,"1234"));
            cards.Add(new Card(a1, "1111111111111111", CardType.VISA, "5555"));

            cards.Add(new Card(a2,"2222222222222222", CardType.MASTERCARD, "2222"));
            cards.Add(new Card(a3, "3333333333333333", CardType.VISA, "3333"));

        }
    }
}

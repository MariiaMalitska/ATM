using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private CardTypeManager cardTypeMng; //gets commision for different cards
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
            Card insertedCard = cards.Find((Card c) => { return c.CardNumber == cardNumb; });
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

        //if enough money on accont wisdraws money and return true else jusst  return false
        public bool TryWisdrawMoney(double money, double comm)
        {
            double sum = money + money * GetWisdrawCommision()/100;
            return currCard.TryWisdrawMoney(sum);
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
            cards.Add(new Card(a1,"1111 2222 3333 4444",CardType.MASTERCARD,"1234"));
            cards.Add(new Card(a1, "1111 1111 1111 1111", CardType.VISA, "5555"));

            cards.Add(new Card(a2,"2222 2222 2222 2222", CardType.MASTERCARD, "2222"));
            cards.Add(new Card(a3, "3333 3333 3333 3333", CardType.VISA, "3333"));

        }
    }
}

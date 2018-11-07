using System;
using System.Collections.Generic;

namespace ATM
{
    class ATMFunc : ATMInterface
    {
        private CardBank cardBank = new CardBank();//generates cards and performs all operations with cards and accounts
        private CashGiver cashGiver = new CashGiver(100, 100, 100, 100, 100, 100);//gives cash
        private TelephoneManager telMng = new TelephoneManager();//manage operations on telephone numbers
        private ReceiptPrinter printer = new ReceiptPrinter(10);//print receipts
        
        public bool CanGive(int sum)
        {
            return cashGiver.CanGive(sum);
        }

        public bool CardBlocked(string cardNumb)
        {
           return cardBank.CardBlocked(cardNumb);
        }

        //set new pin of current card, throw exception if pin has incorrect format
        public void ChangePin(string newPin)
        {
            cardBank.ChangePin(newPin);
        }

        public List<int> GetAvailableSums()
        {
            return cashGiver.GetAvailableSums();
        }

        public string GetBalanceReceipt()
        {
            return printer.PrintBalanceReceipt(cardBank.GetCurrCardNumber(), cardBank.GetCurrCardClientName(), DateTime.Now, cardBank.GetCurrCardBalance());
        }

        public string GetClientNameByCard(string cardNumb)
        {
            return cardBank.GetCardClientName(cardNumb);
        }

        public double GetBalance()
        {
            return cardBank.GetCurrCardBalance();
        }

        public double GetTransferCommision()
        {
            return cardBank.GetTransferCommision();
        }

        public double GetWisdrawCommision()
        {
            return cardBank.GetWisdrawCommision();
        }

        public string GetWisdrawReceipt(int wisdrMon)
        {
            return printer.PrintWisdrawReceipt(cardBank.GetCurrCardNumber(), cardBank.GetCurrCardClientName(),
                DateTime.Now, GetWisdrawCommision(), wisdrMon);
        }

        public bool InsertCard(string cardNumb)
        {
            return cardBank.IsCardValid(cardNumb);
        }

        public void RetrieveCard()
        {
            cardBank.RetrieveCard();
        }

        public bool CardCorrForTransfer(string cardNumb)
        {
            return cardBank.CardCorrForTransfer(cardNumb);
        }

        //return operation receipt if transfer successfully done, otherwise - throw NotEnoughMoneyEsception
        public string TransferMoney(double money, string cardNumb)
        {
            cardBank.TransferMoney(money,cardNumb);
                return printer.PrintTransferReceipt(cardBank.GetCurrCardNumber(),cardBank.GetCurrCardClientName(),DateTime.Now,GetTransferCommision(),money);
        }

        public bool VerifyCard(string cardNumb, string pin)
        {
            return cardBank.VerifyCard(cardNumb,pin);
        }

        //return banknote and their amount, if not enough money on card throws NotENoughMoneyException,
        //if not enough banknotes in ATM, throws NoMoneyException
        public Dictionary<Banknote, int> WisdrawMoney(int money)
        {
            double commMon = money * GetWisdrawCommision() / 100;
            cardBank.WithdrawMoney(money, commMon);
            return cashGiver.GiveMoney(money);
        }

        public bool TelephoneValid(string telNumb)
        {
            return telMng.TelephoneValid(telNumb);
        }

        public void AddToTelBalance(double mon, string tel)
        {
            telMng.AddToTelephonebalance(mon, tel);
            cardBank.WithdrawMoney(mon, GetTelCommision(mon));
        }

        public double GetTelCommision(double money)
        {
            return telMng.GetTelephoneCommision(money);
        }
    }
}

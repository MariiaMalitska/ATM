using System.Collections.Generic;

namespace ATM
{
    class ATMFunc : ATMInterface
    {
        private CardBank cardBank = new CardBank();//generates cards and performs all operations with cards and accounts
        private CashGiver cashGiver = new CashGiver(100, 100, 100, 100, 100, 100);//gives cash
        private TelephoneManager telMng = new TelephoneManager();//manage operations on telephone numbers
        private ReceiptPrinter priter = new ReceiptPrinter();//print receipts
        
        public bool AddToTelBalance(double mon, string tel)
        {
            throw new System.NotImplementedException();
        }

        public bool CanGive(int sum)
        {
            throw new System.NotImplementedException();
        }

        public bool CardBlocked(string cardNumb)
        {
            throw new System.NotImplementedException();
        }

        public bool CardCorrForTransfer()
        {
            throw new System.NotImplementedException();
        }

        public void ChangePin(string newPin)
        {
            throw new System.NotImplementedException();
        }

        public int[] GetAvailableSums()
        {
            throw new System.NotImplementedException();
        }

        public string GetBalanceReceipt()
        {
            throw new System.NotImplementedException();
        }

        public string GetClientNameByCard(string cardNumb)
        {
            throw new System.NotImplementedException();
        }

        public double GetBalance()
        {
            throw new System.NotImplementedException();
        }

        public double GetTelCommision()
        {
            throw new System.NotImplementedException();
        }

        public double GetTransferCommision()
        {
            throw new System.NotImplementedException();
        }

        public double GetWisdrawCommision()
        {
            throw new System.NotImplementedException();
        }

        public string GetWisdrawReceipt(int wisdrMon)
        {
            throw new System.NotImplementedException();
        }

        public bool InsertCard(string cardBumb)
        {
            throw new System.NotImplementedException();
        }

        public bool RetrieveCard()
        {
            throw new System.NotImplementedException();
        }

        public bool TelephoneValid(string telNumb)
        {
            throw new System.NotImplementedException();
        }

        public string TransferMoney(double money, string cardNumb)
        {
            throw new System.NotImplementedException();
        }

        public bool VerifyCard(string cardNumb, string pin)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<Banknote, int> WisdrawMoney()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<Banknote, int> WisdrawMoney(int money)
        {
            throw new System.NotImplementedException();
        }

        public bool CardCorrForTransfer(string cardNumb)
        {
            throw new System.NotImplementedException();
        }

        public double GetTelCommision(double money)
        {
            throw new System.NotImplementedException();
        }
    }
}

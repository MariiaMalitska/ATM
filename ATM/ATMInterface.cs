using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    //interface for usage in graphic part of project
    interface ATMInterface
    {
        bool InsertCard(string cardBumb);//return true if card number is valid, else false (valid card == card is stored in CardBank)
        bool VerifyCard(string cardNumb, string pin);//if pin for specified card valid true, else false
        bool CardBlocked(string cardNumb);//true if card is blocked, else false
        void RetrieveCard();//take card out of ATM (set curr card in CardBank to null)
        void ChangePin(string newPin);//change pin for curr card to the new one
        double GetBalance();//get balance on curr card
        string GetBalanceReceipt();//return string representation of card balance
        List<int> GetAvailableSums();//sums that ATM suggests client to wisdraw
        bool CanGive(int sum);//true id ATM has enough amount of neccessary banknotes to give money
        double GetWisdrawCommision();//return wisdraw commision for currently inserted card
        Dictionary<Banknote, int> WisdrawMoney(int money);//wisdraw money including commision and give them to client in banknotes
        string GetWisdrawReceipt(int wisdrMon);//get string representation of operation of wisdrawing money
        double GetTransferCommision();//get transfer commission for currently inserted card
        bool CardCorrForTransfer(string cardNumb);//true if card number is valid and not the same as inserted card and not attached to the same account
        string TransferMoney(double money, string cardNumb);//transfer money from currently inserted card to another card including commision
        bool TelephoneValid(string telNumb);//true if telephone valid, that is, it is stored in TelephoneManager
        double GetTelCommision(double money);//get commision for adding money to telephone balance, it depends on amount of money we want to add to balance
        bool AddToTelBalance(double mon, string tel);//add money to telephone balance including commision
        string GetClientNameByCard(string cardNumb);//return client name by card number
    }
}

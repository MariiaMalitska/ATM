using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    /*
     * Receipt for each  operation (wisdraw,transfer,getBlance) contains lots of values 
     * specific for exactly this operation. 
     * If we just make all receipts as strings, we will not be able to garantee that every receipt
     * got all necessary values? and we will not be able to tell them apart?
     * If this is true, then is it neccessary to make separate classes for each type of receipt? 
     * Or better leave them as strings?
     * 
     */
    class ReceiptPrinter//prints receipts for different ATM transactions
    {
        private int PaperSheetCount { get; set; }//amount of papaper in ATM

        public ReceiptPrinter(int paperSheets = 0)
        {
            if (paperSheets < 0)
                throw new ArgumentException("Amount of paper sheets in ATM cannot be less than 0.");
            PaperSheetCount = paperSheets;
        }

        //return true if has some paper to print receipts else false
        public bool AbleToPrint()
        {
            return PaperSheetCount > 0;
        }


        public string PrintWisdrawReceipt(string cardNumb, string ownerName, DateTime dateTime, double wisdrawComm, double wisdrawnSum)
        {
            --PaperSheetCount;
            if (!AbleToPrint())
                throw new NoPaperException("No paper left in ATM");
            return "Card number: " + cardNumb + "\nClient name: " + ownerName +
                "\nWisdrawn sum: " + wisdrawnSum + "\nWisdrawn commision: " + wisdrawComm + "\nIncluding commision: " +
                (wisdrawnSum + wisdrawnSum * (wisdrawComm / 100)) + "\nTime: " + dateTime.ToString();
        }

        public string PrintTransferReceipt(string cardNumb, string ownerName, DateTime dateTime, double transferComm, double transferSum)
        {
            --PaperSheetCount;
            if (!AbleToPrint())
                throw new NoPaperException("No paper left in ATM");
            return "Card number: " + cardNumb + "\nClient name: " + ownerName +
                "\nTransfer sum: " + transferSum + "\nTransfer commision: " + transferComm + "\nIncluding commision: " +
                (transferSum + transferSum * (transferComm / 100)) + "\nTime: " + dateTime.ToString();
        }

        public string PrintBalanceReceipt(string cardNumb, string ownerName, DateTime dateTime, double balance)
        {
            --PaperSheetCount;
            if (!AbleToPrint())
                throw new NoPaperException("No paper left in ATM");
            return "Card number: " + cardNumb + "\nClient name: " + ownerName +
                "\nBalance: " + balance + "\nTime: " + dateTime.ToString();
        }
    }


    class NoPaperException : Exception
    {
        public NoPaperException() : base() { }
        public NoPaperException(string message) : base(message) { }
        public NoPaperException(String message, Exception innerException)
            : base(message, innerException) { }
        protected NoPaperException(SerializationInfo info, StreamingContext context)
            : base(info, context){ }
    }
}

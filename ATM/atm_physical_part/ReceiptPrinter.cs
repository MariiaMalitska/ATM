using System;
using System.Runtime.Serialization;

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


        public string PrintWisdrawReceipt(string cardNumb, string ownerName, DateTime dateTime, double wisdrawComm, double wisdrawnSum, double balance)
        {
            --PaperSheetCount;
            if (!AbleToPrint())
                throw new NoPaperException("No paper left in ATM");
            return "Card number: " + cardNumb + 
                "\r\nClient name: " + ownerName +
                "\r\nWithdrawn sum: " + wisdrawnSum + 
                "\r\nWithdrawn commision: " + wisdrawComm +"%" + 
                "\r\nIncluding commision: " + (wisdrawnSum + wisdrawnSum * (wisdrawComm / 100)) +
                "\r\nCurrent balance: " + balance +
                "\r\nTime: " + dateTime.ToString();
        }

        public string PrintTransferReceipt(string cardNumb, string ownerName, DateTime dateTime, double transferComm, double transferSum, double balance)
        {
            --PaperSheetCount;
            if (!AbleToPrint())
                throw new NoPaperException("No paper left in ATM");
            return "Card number: " + cardNumb + 
                "\r\nClient name: " + ownerName +
                "\r\nTransfer sum: " + transferSum + 
                "\r\nTransfer commision: " + transferComm + "%" + 
                "\r\nIncluding commision: " + (transferSum + transferSum * (transferComm / 100)) +
                "\r\nCurrent balance: " + balance +
                "\r\nTime: " + dateTime.ToString();
        }

        public string PrintBalanceReceipt(string cardNumb, string ownerName, DateTime dateTime, double balance)
        {
            --PaperSheetCount;
            if (!AbleToPrint())
                throw new NoPaperException("No paper left in ATM");
            return "Card number: " + cardNumb + 
                "\r\nClient name: " + ownerName +
                "\r\nBalance: " + balance + 
                "\r\nTime: " + dateTime.ToString();
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

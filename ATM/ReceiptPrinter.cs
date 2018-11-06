using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

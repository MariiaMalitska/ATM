using System;
using System.Collections.Generic;
using System.Windows.Forms;

//зв"язок інтерфейсу та класу
namespace ATM
{
    public partial class Form1 : Form
    {
        private int mode = -1;
        private string input = "";
        private string cardNum = "";

        List<int> sums = new List<int>();
        private int sum = 0;

        private string inputCache = "";

        private string receiptCache = "";

        ATMFunc atm = new ATMFunc();

        public Form1()
        {
            InitializeComponent();
        }

        //shows buttonCard
        private void ShowButtonCard()
        {
            buttonCard.Enabled = true;
            buttonCard.Visible = true;
        }

        //hides buttonCard
        private void HideButtonCard()
        {
            buttonCard.Enabled = false;
            buttonCard.Visible = false;
        }

        //turns off the atm
        //mode -1
        private void TurnOff()
        {
            atm.RetrieveCard();
            displayBox.Text = "Банкомат вимкнено";
            HideButtonCard();
            buttonTurn.Text = "Turn On";
        }

        //turns on the atm
        //mode 0
        private void TurnOn()
        {
            cardNum = "";
            displayBox.Text = "Будь ласка, вставте картку";
            ShowButtonCard();
            buttonTurn.Text = "Turn Off";
        }

        private void buttonExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {

            switch (mode)
            {
                case 1:
                case 2:
                    ChangeMode(0);
                    break;
                case -1:
                case 0:
                case 3:
                case 31:
                case 32:
                case 33:
                case 4:
                case 41:
                case 42:
                case 43:
                case 5:
                case 51:
                case 52:
                case 53:
                case 6:
                case 7:
                case 71:
                case 100:
                    ChangeMode(2);
                    break;
            }
        }

        private void buttonTurn_Click(object sender, System.EventArgs e)
        {
            if (mode>-1)
                ChangeMode(-1);
            else
                ChangeMode(0);
        }

        private void buttonCard_Click(object sender, System.EventArgs e)
        {
            string cardNumber = Microsoft.VisualBasic.Interaction.InputBox("Введіть номер картки", "Картка", "");
            CheckCard(cardNumber);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            InputChar(1);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            InputChar(2);
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            InputChar(3);
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            InputChar(4);
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            InputChar(5);
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            InputChar(6);
        }

        private void button7_Click(object sender, System.EventArgs e)
        {
            InputChar(7);
        }

        private void button8_Click(object sender, System.EventArgs e)
        {
            InputChar(8);
        }

        private void button9_Click(object sender, System.EventArgs e)
        {
            InputChar(9);
        }

        private void button0_Click(object sender, System.EventArgs e)
        {
            InputChar(0);
        }

        private void buttonBack_Click(object sender, System.EventArgs e)
        {
            if (input.Length > 0)
            {
                input = input.Remove(input.Length - 1);
                displayBox.Text = displayBox.Text.Remove(displayBox.Text.Length - 1);
            }
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            switch (mode)
            {
                case 1:
                    //verifying input pin
                    PinCheck();
                    break;
                case 33:
                    //check input custom sum to withdraw
                    if (input.Length > 0)
                        CustomWithdrawInputCheck();
                    break;
                case 4:
                    //check input card to transfer
                    TransferCardCheck();
                    break;
                case 41:
                    //check input sum to transfer
                    if (input.Length > 0)
                        TransferInputCheck();
                    break;
                case 5:
                    //check input telephone number
                    TelephoneCheck();
                    break;
                case 51:
                    //check input sum to top up telephone account
                    if (input.Length > 0)
                        TelephoneInputCheck();
                    break;
                case 7:
                    //check new pin
                    NewPinCheck();
                    break;
                case 71:
                    //check repeated pin (verify new pin)
                    PinChange();
                    break;
            }
        }

        private void ChangeMode(int i)
        {
            if (i >= -1) mode = i;
            input = "";
            switch (mode)
            {
                case -1:
                    TurnOff();
                    break;
                case 0:
                    TurnOn();
                    break;
                case 1:
                    //inserted card
                    CardMenu();
                    break;
                case 2:
                    //main menu
                    sum = 0;
                    ShowMenu();
                    break;
                case 3:
                    //choose '1' withdraw cash
                    WithdrawMenu();
                    break;
                case 31:
                    //show commision for withdraw
                    WithdrawCommision();
                    break;
                case 32:
                    //successful withdraw
                    WithdrawReceipt();
                    break;
                case 33:
                    //input custom amount to withdraw
                    CustomWithdrawInput();
                    break;
                case 4:
                    //choose '2' transer cash to another card
                    //input card number
                    inputCache = "";
                    receiptCache = "";
                    TransferMenu();
                    break;
                case 41:
                    //input amount to transfer
                    TransferInput();
                    break;
                case 42:
                    //show commision for transfer
                    TransferCommision();
                    break;
                case 43:
                    //successful transfer
                    TransferReceipt();
                    break;
                case 5:
                    //choose '3' telephone account
                    inputCache = "";
                    TelephoneMenu();
                    break;
                case 51:
                    //input amount to put on telephone account
                    TelephoneInput();
                    break;
                case 52:
                    //show commision for telephone
                    TelephoneCommision();
                    break;
                case 53:
                    //successful telephone operation
                    TelephoneReceipt();
                    break;
                case 6:
                    //choose '4' show balance
                    BalanceMenu();
                    break;
                case 7:
                    //choose '5' change pin
                    inputCache = "";
                    PinMenu();
                    break;
                case 71:
                    //repeat new pin
                    NewPinProceed();
                    break;

            }
        }

        private void InputChar(int c)
        {
            switch (mode)
            {
                case 1:
                    //input pin to verify card
                    if (!VerifyBlocked(cardNum))
                    {
                        PassInput(c);
                    }
                    break;
                case 2:
                    //choose menu option
                    ChooseMenuOption(c);
                    break;
                case 3:
                    //choose amount to withdraw
                    WithdrawProceed(c);
                    break;
                case 31:
                    //agree to commision for withdraw
                    WithdrawWithCommision(c);
                    break;
                case 32:
                    //agree to print receipt for withdraw
                    PrintWithdrawReceipt(c);
                    break;
                case 33:
                    //input custom amount to withdraw
                    SumInput(c);
                    break;
                case 4:
                    //input number of card to transfer to
                    NumInput(c);
                    break;
                case 41:
                    //input amount to transfer
                    SumInput(c);
                    break;
                case 42:
                    //agree to commision for transfer
                    TransferWithCommision(c);
                    break;
                case 43:
                    //agree to print receipt for transfer
                    PrintTransferReceipt(c);
                    break;                        
                case 5:
                    //input telephone number
                    NumInput(c);
                    break;
                case 51:
                    //input amount to telephone
                    SumInput(c);
                    break;
                case 52:
                    //agree to commision
                    TelephoneWithCommision(c);
                    break;
                case 6:
                    //agree to print receipt for balance
                    BalanceProceed(c);
                    break;
                case 7:
                    //input new pin
                    PassInput(c);
                    break;
                case 71:
                    //repeat new pin
                    PassInput(c);
                    break;
            }
        }

        //for sums input
        private void SumInput(int c)
        {
            if (input.Length == 0 && c == 0) { }
            else
            {
                displayBox.Text += c;
                input += c;
            }
        }

        //for secret passwords input
        private void PassInput(int c)
        {
            displayBox.Text += "*";
            input += c;
        }

        //for numbers input
        private void NumInput(int c)
        {
            displayBox.Text += c;
            input += c;
        }

        //check inserted card
        //mode 0
        private void CheckCard(string cardNumber)
        {
            if (VerifyCardNum(cardNumber))
            {
                cardNum = cardNumber;
                //if (VerifyBlocked(cardNum))
                //    displayBox.Text = "Картка заблокована. Будь ласка, вставте іншу картку";
                //else ChangeMode(1);
                ChangeMode(1);
            }
            else
            {
                displayBox.Text = "Помилка: картка пошкоджена або недійсна. Будь ласка, вставте дійсну картку";
            }
        }

        //mode 1
        private void CardMenu()
        {
            displayBox.Text = "Будь ласка, введіть PIN-код \r\n\r\n\r\n\r\n\r\n";
            HideButtonCard();
        }

        //verify pin for card
        //"ok" for mode 1
        private void PinCheck()
        {
            if (VerifyPin(input)) ChangeMode(2);
            else
            {
                if (VerifyBlocked(cardNum))
                    displayBox.Text = "Картка заблокована. Будь ласка, вставте іншу картку";
                else
                    displayBox.Text = "Введено неправильний PIN-код. Будь ласка, введіть правильний PIN-код \r\n\r\n\r\n\r\n\r\n";
            }
            input = "";
        }

        //mode 2
        private void ChooseMenuOption(int c)
        {
            switch (c)
            {
                case 1:
                    ChangeMode(3);
                    break;
                case 2:
                    ChangeMode(4);
                    break;
                case 3:
                    ChangeMode(5);
                    break;
                case 4:
                    ChangeMode(6);
                    break;
                case 5:
                    ChangeMode(7);
                    break;
            }
        }

        //mode 2
        private void ShowMenu()
        {
            displayBox.Text = "Будь ласка, виберіть тип транзакції:\r\n\r\n" +
                "1) Зняти готівку\r\n" +
                "2) Зробити переказ на іншу картку\r\n" +
                "3) Поповнити баланс телефонного рахунку\r\n" +
                "4) Переглянути баланс\r\n" +
                "5) Змінити PIN-код\r\n\r\n" +
                "Натисніть \"Cаncel\" для завершення роботи з карткою";
        }

        //mode 3
        private void WithdrawMenu()
        {
            displayBox.Text = "К-ть готівки для виводу: \r\n\r\n";
            sums = atm.GetAvailableSums();
            for (int i = 0; i < sums.Count; i++)
                displayBox.Text += (i + 1) + ") " + sums[i] + "\r\n";

            displayBox.Text += (sums.Count+1) + ") Ввести значення самостійно\r\n\r\nНатисніть \"Cаncel\" для повернення в головне меню";
        }

        //input for mode 3
        private void WithdrawProceed(int c)
        {
            if (c > 0 && c <= sums.Count)
            {
                sum = sums[c - 1];
                ChangeMode(31);
            }
            if (c == sums.Count+1)
                ChangeMode(33);
        }

        //mode 31
        private void WithdrawCommision()
        {
            displayBox.Text = "Комісія для зняття з цієї картки: " + atm.GetWisdrawCommision() + "% - " + atm.GetWisdrawCommision() * sum / 100 + " UAH\r\n\r\n" +
                "1) Зняти готівку з комісією\r\n" +
                "2) Повернутись до головного меню";
        }

        //input for mode 31
        private void WithdrawWithCommision(int c)
        {
            if (c == 1)
            {
                try
                {
                    Dictionary<Banknote, int> temp = atm.WisdrawMoney(sum);
                    printBox.Text = "";
                    foreach (KeyValuePair<Banknote, int> entry in temp)
                    {
                        printBox.Text += "Купюри " + entry.Key + " UAH - " + entry.Value + " шт.\r\n";
                    }
                    ChangeMode(32);
                }
                catch (NoEnoughMoneyException e)
                {
                    displayBox.Text = "Недостатньо коштів на балансі. Натисність \"Cancel\" для повернення в головне меню";
                }
            }
            if (c == 2)
            {
                ChangeMode(100);
                displayBox.Text = "Відміна операції. Натисність \"Cancel\" для повернення в головне меню";
            }
        }

        //mode 32
        private void WithdrawReceipt()
        {
            displayBox.Text = "Знято: " + (sum + atm.GetWisdrawCommision() * sum / 100) + " UAH\r\n" +
                "Видано: " + sum + " UAH\r\n\r\nДрукувати чек?\r\n\r\n" +
                        "1) Так\r\n" +
                        "2) Ні\r\n";
        }

        //input for mode 32
        private void PrintWithdrawReceipt(int c)
        {
            if (c == 1)
                try
                {
                    printBox.Text = atm.GetWisdrawReceipt(sum);
                    ChangeMode(2);
                }
                catch (NoPaperException e)
                {
                    displayBox.Text = "Відсутній папір для друку. Натисність \"Cancel\" для повернення в головне меню";
                }
            if (c == 2) ChangeMode(2);
        }

        //mode 33
        private void CustomWithdrawInput()
        {
            displayBox.Text = "Введіть суму для зняття готівки\r\n або натисніть \"Cаncel\" для повернення в головне меню\r\n\r\n\r\n\r\n\r\n";
        }

        //"ok" for mode 33
        private void CustomWithdrawInputCheck()
        {
            sum = Int32.Parse(input);
            if (sum == 0)
            {
                ChangeMode(100);
                displayBox.Text = "Сума не введена. Натисність \"Cancel\" для повернення в головне меню";
            }
            if (atm.CanGive(sum))
            {
                ChangeMode(31);
            }
            else
            {
                ChangeMode(100);
                displayBox.Text = "Відсутня потрібна кількість купюр. Натисність \"Cancel\" для повернення в головне меню";
            }
        }

        //mode 4
        private void TransferMenu()
        {
            displayBox.Text = "Введіть комер картки, на який відбудеться переказ коштів\r\n або натисніть \"Cаncel\" для повернення в головне меню\r\n\r\n\r\n\r\n\r\n";
        }

        //"ok" for mode 4
        private void TransferCardCheck()
        {
            if (atm.CardCorrForTransfer(input))
            {
                inputCache = input;
                ChangeMode(41);
            }
            else
            {
                ChangeMode(100);
                displayBox.Text = "Неможливо здійснити переказ за вказаним номером карти. Натисність \"Cancel\" для повернення в головне меню";
            }
        }

        //mode 41
        private void TransferInput()
        {
            displayBox.Text = "Введіть суму переказу\r\n або натисніть \"Cаncel\" для повернення в головне меню\r\n\r\n\r\n\r\n\r\n";
        }

        //"ok" for 41
        private void TransferInputCheck()
        {
            sum = Int32.Parse(input);
            if (sum == 0)
            {
                ChangeMode(100);
                displayBox.Text = "Сума не введена. Натисність \"Cancel\" для повернення в головне меню";
            }
            ChangeMode(42);
        }

        //mode 42
        private void TransferCommision()
        {
            displayBox.Text = "Комісія для переказу з цієї картки: " + atm.GetTransferCommision() + "% - " + atm.GetTransferCommision() * sum / 100 + " UAH\r\n\r\n" +
                "1) Переказати кошти з комісією\r\n" +
                "2) Повернутись до головного меню";
        }

        //input for 42
        private void TransferWithCommision(int c)
        {
            if (c == 1)
            {
                try
                {
                    receiptCache = atm.TransferMoney(sum, inputCache);
                    ChangeMode(43);
                }
                catch (NoEnoughMoneyException e)
                {
                    displayBox.Text = "Недостатньо коштів на балансі. Натисність \"Cancel\" для повернення в головне меню";
                }
            }
            if (c == 2)
            {
                ChangeMode(100);
                displayBox.Text = "Відміна операції. Натисність \"Cancel\" для повернення в головне меню";
            }
        }

        //mode 43
        private void TransferReceipt()
        {
            displayBox.Text = "Знято: " + (sum + atm.GetWisdrawCommision() * sum / 100) + " UAH\r\n" +
                "Переказано: " + sum + " UAH\r\n\r\nДрукувати чек?\r\n\r\n" +
                        "1) Так\r\n" +
                        "2) Ні\r\n";
        }

        //input for mode 43
        private void PrintTransferReceipt(int c)
        {
            if (c == 1)
                try
                {
                    printBox.Text = receiptCache;
                    ChangeMode(2);
                }
                catch (NoPaperException e)
                {
                    displayBox.Text = "Відсутній папір для друку. Натисність \"Cancel\" для повернення в головне меню";
                }
            if (c == 2) ChangeMode(2);
        }

        //mode 5
        private void TelephoneMenu()
        {
            displayBox.Text = "Введіть комер телефону, на рахунок якого відбудеться поповнення коштів\r\n або натисніть \"Cаncel\" для повернення в головне меню\r\n\r\n\r\n\r\n\r\n+38 ";
        }

        //"ok" for mode 5
        private void TelephoneCheck()
        {
            if (atm.TelephoneValid(input))
            {
                inputCache = input;
                ChangeMode(51);
            }
            else
            {
                ChangeMode(100);
                displayBox.Text = "Номер недійсний. Натисність \"Cancel\" для повернення в головне меню";
            }
        }

        //mode 51
        private void TelephoneInput()
        {
            displayBox.Text = "Введіть суму поповнення\r\n або натисніть \"Cаncel\" для повернення в головне меню\r\n\r\n\r\n\r\n\r\n";
        }

        //"ok" for 51
        private void TelephoneInputCheck()
        {
            sum = Int32.Parse(input);
            if (sum == 0)
            {
                ChangeMode(100);
                displayBox.Text = "Сума не введена. Натисність \"Cancel\" для повернення в головне меню";
            }
            ChangeMode(52);
        }

        //mode 52
        private void TelephoneCommision()
        {
            displayBox.Text = "Комісія для поповнення: " + atm.GetTelCommision(sum) + "% - " + atm.GetTelCommision(sum) * sum / 100 + " UAH\r\n\r\n" +
                "1) Поповнити з комісією\r\n" +
                "2) Повернутись до головного меню";
        }

        //input for 52
        private void TelephoneWithCommision(int c)
        {
            if (c == 1)
            {
                try
                {
                    atm.AddToTelBalance(sum, inputCache);
                    ChangeMode(53);
                }
                catch (NoEnoughMoneyException e)
                {
                    displayBox.Text = "Недостатньо коштів на балансі. Натисність \"Cancel\" для повернення в головне меню";
                }
            }
            if (c == 2)
            {
                ChangeMode(100);
                displayBox.Text = "Відміна операції. Натисність \"Cancel\" для повернення в головне меню";
            }
        }

        //mode 53
        private void TelephoneReceipt()
        {
            displayBox.Text = "Знято: " + (sum + atm.GetTelCommision(sum) * sum / 100) + " UAH\r\n" +
                "Поповнено: " + sum + " UAH\r\n\r\nНатисність \"Cancel\" для повернення в головне меню";
        }

        //mode 6
        private void BalanceMenu()
        {
            displayBox.Text = "Баланс на картці: " + atm.GetBalance() + " UAH";
            displayBox.Text += "\r\n\r\nДрукувати чек?\r\n\r\n" +
                "1) Так\r\n" +
                "2) Ні\r\n";
        }

        //input for mode 6
        private void BalanceProceed(int c)
        {
            if (c == 1)
                try
                {
                    printBox.Text = atm.GetBalanceReceipt();
                    ChangeMode(2);
                }
                catch (NoPaperException e)
                {
                    displayBox.Text = "Відсутній папір для друку. Натисність \"Cancel\" для повернення в головне меню";
                }
            if (c == 2)
                ChangeMode(2);
        }

        //mode 7
        private void PinMenu()
        {
            displayBox.Text = "Будь ласка, введіть новий 4-значний PIN-код \r\nабо натисніть \"Cаncel\" для повернення в головне меню\r\n\r\n\r\n\r\n\r\n";
        }

        //"ok" for mode 7
        private void NewPinCheck()
        {
            if (input.Length == 4)
            {
                inputCache = input;
                ChangeMode(71);
            }
            else
                displayBox.Text = "Введено некорректний PIN-код. Будь ласка, введіть новий 4-значний PIN-код \r\nабо натисніть \"Cаncel\" для повернення в головне меню\r\n\r\n\r\n\r\n\r\n";
            input = "";
        }

        //mode 71
        private void NewPinProceed()
        {
            displayBox.Text = "Будь ласка, підтвердіть ввід \r\nабо натисніть \"Cаncel\" для повернення в головне меню\r\n\r\n\r\n\r\n\r\n";
            
        }

        //"ok" for mode 71
        private void PinChange()
        {
            string temp = "";
            temp = input;
            ChangeMode(100);
            if (temp.Equals(inputCache))
            {
                atm.ChangePin(inputCache);
                displayBox.Text = "PIN-код успішно змінено. Натисніть \"Cаncel\" для повернення в головне меню";
            }
            else
            {
                ChangeMode(100);
                displayBox.Text = "Значення не співпадає з попередньо введеним. Натисніть \"Cаncel\" для повернення в головне меню";
            }
            
        }


        private bool VerifyCardNum(string cardNumber)
        {
            return atm.InsertCard(cardNumber);
        }

        private bool VerifyPin(string s)
        {
            return atm.VerifyCard(cardNum, s);
        }

        private bool VerifyBlocked(string cardNumber)
        {
            return atm.CardBlocked(cardNumber);
        }
    }
}

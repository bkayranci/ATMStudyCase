using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMStudyCase
{
    class Withdrawal : Transaction
    {
        // kullanmaya gerek kalmadi
        private decimal amount;

        private const int CANCELLED = 6;
        private CashDispenser cashDispenser;
        private Keypad keypad;

        enum Menu
        {
            AMOUNT10 = 1,
            AMOUNT20 = 2,
            AMOUNT50 = 3,
            AMOUNT100 = 4,
            AMOUNT200 = 5,
            AMOUNT500 = 6,
            AMOUNT2000 = 7,
            OTHER = 8,
            GO_BACK = 9
        }

        public Withdrawal(int userAccountNumber, Screen atmScreen, BankDatabase atmBankDatabase, Keypad atmKeypad, CashDispenser atmCashDispenser)
            : base(userAccountNumber, atmScreen, atmBankDatabase)
        {
            keypad = atmKeypad;
            cashDispenser = atmCashDispenser;
        }
        // islem yap
        public override void Execute()
        {
            BankDatabase bankDatabase = Database;
            Screen screen = UserScreen;

            bool isContinue = true;

            while (isContinue)
            {
                bool display = DisplayMenu();
                if (display == true)
                {
                    UserScreen.DisplayMessageLine("\nOperation success!");
                    isContinue = DoYouWantToContinue();
                }
                else if (display == false)
                {
                    isContinue = false;
                }
            }
        }

        // devam etmek istiyor musun
        private bool DoYouWantToContinue()
        {
            UserScreen.DisplayMessageLine("If you want to continue, press for 1.");

            return (keypad.GetInput(true) == 1) ? true : false;
        }

        private bool DisplayMenu()
        {
            UserScreen.Clear();
            UserScreen.DisplayMessageLine("\nWithdrawal menu\n");
            UserScreen.DisplayMessageLine("1->\t$10");
            UserScreen.DisplayMessageLine("2->\t$20");
            UserScreen.DisplayMessageLine("3->\t$50");
            UserScreen.DisplayMessageLine("4->\t$100");
            UserScreen.DisplayMessageLine("5->\t$200");
            UserScreen.DisplayMessageLine("6->\t$500");
            UserScreen.DisplayMessageLine("7->\t2000");
            UserScreen.DisplayMessageLine("8->\tOther");
            UserScreen.DisplayMessageLine("9->\tCancel transaction");
            UserScreen.DisplayMessage("\nChoose a with drawal amount: ");

            switch (keypad.GetInput())
            {
                case (int)Menu.AMOUNT10: return IsWithrawal(10);
                case (int)Menu.AMOUNT20: return IsWithrawal(20);
                case (int)Menu.AMOUNT50: return IsWithrawal(50);
                case (int)Menu.AMOUNT100: return IsWithrawal(100);
                case (int)Menu.AMOUNT200: return IsWithrawal(200);
                case (int)Menu.AMOUNT500: return IsWithrawal(500);
                case (int)Menu.AMOUNT2000: return IsWithrawal(2000);
                case (int)Menu.OTHER:
                    UserScreen.DisplayMessage("\nEnter a value: ");
                    return IsWithrawal(keypad.GetInput());
                case (int)Menu.GO_BACK:
                    return false;
                default:
                    UserScreen.DisplayMessageLine("\nInvalid selection! Try again.");
                    break;
            }
            return false;
        }

        // islem yapilabiliyor mu
        private bool IsWithrawal(int val)
        {
            // cekebilecegin para var mi?
            if (val <= Database.getAvaibleBalance(AccountNumber))
            {
                // ATM de para var mi
                if (cashDispenser.IsSufficiantCashAvaible(val))
                {
                    Database.Debit(AccountNumber, val);
                    cashDispenser.DispenseCash(val);
                    return true;
                }
                else
                {
                    UserScreen.DisplayMessageLine("ERROR: money in ATM is not enough!");
                    UserScreen.Sleep(3000);
                }
            }
            else
            {
                UserScreen.DisplayMessage("\nERROR: your avaible balance: ");
                UserScreen.DisplayDollarAmount(Database.getAvaibleBalance(AccountNumber));
                UserScreen.Sleep(2000);
                
            }

            return false;
        }
    }
}

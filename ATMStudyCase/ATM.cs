using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMStudyCase
{
    class ATM
    {
        private BankDatabase bankDatabase;
        private CashDispenser cashDispenser;
        private int currentAccountNumber;
        private DepositSlot depositSlot;
        private Keypad keypad;
        private Screen screen;
        private bool userAuthenticated;

        enum MenuOption
        {
            BALANCE_INQUIRY,
            WITHDRAWAL,
            DEPOSIT,
            EXIT_ATM
        }

        public ATM()
        {
            userAuthenticated = false;
            currentAccountNumber = 0;
            screen = new Screen();
            keypad = new Keypad();
            cashDispenser = new CashDispenser();
            depositSlot = new DepositSlot();
            bankDatabase = new BankDatabase();
        }

        private void AuthenticateUser()
        {
            screen.DisplayMessageLine("Account Number: ");
            int accountNumber = keypad.GetInput();
            screen.DisplayMessageLine("Enter your PIN: ");
            int pin = keypad.GetInput();

            // giris yaparsa hesap numarasini al
            userAuthenticated = bankDatabase.AuthenticateUser(accountNumber, pin);

            if (userAuthenticated)
            {
                currentAccountNumber = accountNumber;
            }
            else
            {
                screen.DisplayMessageLine("Wrong information. Please try again.");
            }
        }

        // islem olustur
        private Transaction CreateTransaction(MenuOption type)
        {
            Transaction temp = null;

            switch (type)
            {
                case MenuOption.BALANCE_INQUIRY:
                    temp = new BalanceInquiry(currentAccountNumber, screen, bankDatabase);
                    break;
                case MenuOption.WITHDRAWAL:
                    temp = new Withdrawal(currentAccountNumber, screen, bankDatabase, keypad, cashDispenser);
                    break;
                case MenuOption.DEPOSIT:
                    temp = new Deposit(currentAccountNumber, screen, bankDatabase, keypad, depositSlot);
                    break;
                default:
                    break;
            }

            return temp;
        }

        // islemi gerceklestir
        private void PerformTransaction()
        {
            Transaction transaction = null;
            bool userExited = false;

            while( !userExited )
            {
                switch (DisplayMainMenu())
                {
                    case MenuOption.BALANCE_INQUIRY:
                        transaction = CreateTransaction(MenuOption.BALANCE_INQUIRY);
                        transaction.Execute();
                        screen.Back();
                        break;
                    case MenuOption.WITHDRAWAL:
                        transaction = CreateTransaction(MenuOption.WITHDRAWAL);
                        transaction.Execute();
                        
                        break;
                    case MenuOption.DEPOSIT:
                        transaction = CreateTransaction(MenuOption.DEPOSIT);
                        transaction.Execute();
                        break;
                    case MenuOption.EXIT_ATM:
                        userExited = true;
                        screen.DisplayMessageLine("Good Bye!");
                        screen.Sleep(5000);
                        break;
                    default:
                        screen.DisplayMessageLine("Invalid selection. Try again.");
                        break;
                }
            }

        }

        // main menu secenegini dondur
        private MenuOption DisplayMainMenu()
        {
            screen.Clear();
            screen.DisplayMessageLine("\nMain menu\n");
            screen.DisplayMessageLine("1- View my balance");
            screen.DisplayMessageLine("2- Withdraw cash");
            screen.DisplayMessageLine("3- Deposit funds");
            screen.DisplayMessageLine("4- Exit");

            screen.DisplayMessageLine("\nEnter a choice: ");

            switch (keypad.GetInput())
            {
                case 1: return MenuOption.BALANCE_INQUIRY;
                case 2: return MenuOption.WITHDRAWAL;
                case 3: return MenuOption.DEPOSIT;
                case 4: return MenuOption.EXIT_ATM;
                default:
                    screen.DisplayMessageLine("Invalid selection. Try again.");
                    break;
            }

            return 0;
        }

        public void Run()
        {
            while (true)
            {
                while ( !userAuthenticated )
                {
                    screen.DisplayMessageLine("Welcome");
                    AuthenticateUser();
                }

                // islem gerceklestir
                PerformTransaction();

                // cikis yap
                userAuthenticated = false;
                currentAccountNumber = 0;
                screen.DisplayMessageLine("Good Bye!");
                screen.Clear();
            }
        }
    }
}

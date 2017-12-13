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
                        screen.Back();
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

        private MenuOption DisplayMainMenu()
        {
            screen.Clear();
            screen.DisplayMessageLine("1- Balance Inquiry");
            screen.DisplayMessageLine("2- Withdrawal");
            screen.DisplayMessageLine("3- Deposit");
            screen.DisplayMessageLine("4- Exit ATM");

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

                PerformTransaction();

                userAuthenticated = false;
                currentAccountNumber = 0;
                screen.DisplayMessageLine("Good Bye!");
                screen.Clear();
            }
        }
    }
}

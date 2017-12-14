using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMStudyCase
{
    class Deposit : Transaction
    {
        private decimal amount;
        private const int CANCELLED = 0;
        private DepositSlot depositSlot;
        private Keypad keypad;

        public Deposit(int userAccountNumber, Screen atmScreen, BankDatabase atmBankDatabase, Keypad atmKeypad, DepositSlot atmDepositSlot)
            : base(userAccountNumber, atmScreen, atmBankDatabase)
        {
            keypad = atmKeypad;
            depositSlot = atmDepositSlot;
        }

        public override void Execute()
        {
            amount = PromptForDepositAmount();

            if (amount != CANCELLED)
            {
                // tek seferde yatirilabilecek miktar
                bool envelopeReceived = depositSlot.IsDepositEnvelopeReceived(amount);

                if (envelopeReceived)
                {
                    UserScreen.DisplayMessageLine("\nAmount: " + amount);
                    UserScreen.DisplayMessageLine("\nPress for 1 to cancel. Press enter for continue.");

                    // yatirmak icin engel yok yatirmak icin onayla
                    if (keypad.GetInput(true) != 1)
                    {
                        Database.Credit(AccountNumber, amount);
                        UserScreen.DisplayMessageLine("\nDeposit operation success");
                        UserScreen.Sleep(4000);
                    }
                    else
                    {
                        UserScreen.DisplayMessageLine("\nOperation cancelled!");
                        UserScreen.Sleep(4000);
                    }
                    
                }
                else
                {
                    UserScreen.DisplayMessageLine(
                        "\nSlot is not enough. Operation cancelled!"
                        );
                    UserScreen.Sleep(4000);
                }
            }
            else
            {
                UserScreen.DisplayMessageLine(
                    "\nCancelling operation..."
                    );
                UserScreen.Sleep(4000);
                UserScreen.Sleep(2000);
            }
        }

        // yatirilacak parayi gir
        private decimal PromptForDepositAmount()
        {
            UserScreen.DisplayMessage(
                "\nEnter amount for deposit (or 0 to cancel): "
                );

            int input = keypad.GetInput();

            if (input == CANCELLED)
            {
                return CANCELLED;
            }
            else
            {
                return input;
            }
        }
    }
}

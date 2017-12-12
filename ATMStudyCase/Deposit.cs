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
                UserScreen.DisplayMessage("\nPlease insert a deposit envelope containing ");
                UserScreen.DisplayDollarAmount(amount);
                UserScreen.DisplayMessageLine(" in the deposit slot.");

                bool envelopeReceived = depositSlot.IsDepositEnvelopeReceived();

                if (envelopeReceived)
                {
                    UserScreen.DisplayMessageLine(
                        "\nYour envelope has been received.\n" +
                        "The money just deposited will not be avaible" +
                        "until we \nverify the amount of any" +
                        "enclosed cash, and any enclosed check clear."
                        );
                    Database.Credit(AccountNumber, amount);
                }
                else
                {
                    UserScreen.DisplayMessageLine(
                        "\nYou did not insert an envelope, so the ATM has " +
                        "cancelled your transaction."
                        );
                }
            }
            else
            {
                UserScreen.DisplayMessageLine(
                    "\nCancelling transaction..."
                    );
            }
        }

        private decimal PromptForDepositAmount()
        {
            UserScreen.DisplayMessage(
                "\nPlease input a deposit amount in CENTS (or 0 to cancel): "
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

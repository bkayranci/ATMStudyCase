using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMStudyCase
{
    class BalanceInquiry : Transaction
    {
        public BalanceInquiry(int userAccountNumber, Screen atmScreen, BankDatabase atmBankDatabase)
            : base(userAccountNumber, atmScreen, atmBankDatabase)
        {
        }

        public override void Execute()
        {
            BankDatabase bankDatabase = Database;
            Screen screen = UserScreen;

            decimal avaibleBalance = bankDatabase.getAvaibleBalance(AccountNumber);
            decimal totalBalance = bankDatabase.getTotalBalance(AccountNumber);

            // hesaptaki bilgileri ekrana yazar
            screen.DisplayMessage("\nAvaible Balance: ");
            screen.DisplayDollarAmount(avaibleBalance);
            screen.DisplayMessage("\nTotal Balance: ");
            screen.DisplayDollarAmount(totalBalance);
        }
    }
}

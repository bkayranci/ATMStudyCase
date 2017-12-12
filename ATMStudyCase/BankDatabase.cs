namespace ATMStudyCase
{
    public class BankDatabase
    {
        private Account[] accounts;

        public BankDatabase()
        {
            accounts = new Account[5];
            accounts[0] = new Account(1, 1, 10000, 100000);
            accounts[1] = new Account(2, 2, 20000, 200000);
            accounts[2] = new Account(3, 3, 30000, 300000);
            accounts[3] = new Account(4, 4, 40000, 400000);
            accounts[4] = new Account(5, 5, 50000, 500000);
        }

        public bool AuthenticateUser(int userAccountNumber, int userPin)
        {
            Account account = GetAccount(userAccountNumber);

            return (account != null) ? account.ValidatePin(userPin) : false;
        }

        public void Credit(int userAccountNumber, decimal amount)
        {
            Account account = GetAccount(userAccountNumber);

            if (account != null)
                account.Debit(amount);
        }

        public void Debit(int userAccountNumber, decimal amount)
        {
            Account account = GetAccount(userAccountNumber);

            if (account != null)
                account.Credit(amount);
        }

        // eger verilen account bulunamazsa null doner
        private Account GetAccount(int accountNumber)
        {
            foreach (var account in accounts)
            {
                if (account.AccountNumber == accountNumber) return account;
            }

            return null;
        }

        // eger verilen user account number bulunamazsa -1 doner
        public decimal getAvaibleBalance(int userAccountNumber)
        {
            return (GetAccount(userAccountNumber) != null) ? GetAccount(userAccountNumber).AvaibleBalance : -1;
        }

        // eger verilen user account number bulunamazsa -1 doner
        public decimal getTotalBalance(int userAccountNumber)
        {
            return (GetAccount(userAccountNumber) != null) ? GetAccount(userAccountNumber).TotalBalance : -1;
        }

    }
}
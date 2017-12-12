namespace ATMStudyCase
{
    class Account
    {
        public int AccountNumber { get; set; }
        public decimal AvaibleBalance { get; set; }
        private int Pin { get; set; }
        public decimal TotalBalance { get; set; }

        public Account(int AccountNumber, int Pin, decimal TotalBalance, decimal AvaibleBalance)
        {
            this.AccountNumber = AccountNumber;
            this.Pin = Pin;
            this.AvaibleBalance = AvaibleBalance;
            this.TotalBalance = TotalBalance;
        }

        public void Credit(decimal amount)
        {
            TotalBalance += amount;
        }

        public void Debit(decimal amount)
        {
            AvaibleBalance -= amount;
            TotalBalance -= amount;
        }

        public bool ValidatePin(int userPin)
        {
            return (Pin == userPin) ? true : false;
        }
    }
}
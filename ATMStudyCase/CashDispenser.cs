namespace ATMStudyCase
{
    class CashDispenser
    {
        private int billCount;
        private const int INITIAL_COUNT = 20000;

        public CashDispenser()
        {
            billCount = INITIAL_COUNT;
        }

        public void DispenseCash(decimal amount)
        {
            billCount = billCount - (int)(amount / 20);
        }
        
        public bool IsSufficiantCashAvaible(decimal amount)
        {
            return (billCount >= (amount / 20)) ? true : false;
        }

    }
}
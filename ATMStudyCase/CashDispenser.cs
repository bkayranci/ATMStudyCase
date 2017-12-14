namespace ATMStudyCase
{
    class CashDispenser
    {
        private int billCount;
        private const int INITIAL_COUNT = 50000;

        public CashDispenser()
        {
            billCount = INITIAL_COUNT;
        }

        public void DispenseCash(decimal amount)
        {
            billCount = billCount - (int)amount;
        }
        
        public bool IsSufficiantCashAvaible(decimal amount)
        {
            return (billCount >= amount) ? true : false;
        }

    }
}
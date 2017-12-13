namespace ATMStudyCase
{
    class DepositSlot
    {
        public const decimal avaibleSlot = 10000;

        public bool IsDepositEnvelopeReceived(decimal val)
        {
            return (val <= avaibleSlot) ? true : false;
        }
    }
}
namespace ATMStudyCase
{
    class Keypad
    {
        public int GetInput(bool isOneMoreTime = false)
        {

            string cStr;
            int result = -1;
            bool isMoreTry = false;

            do
            {
                if (isOneMoreTime && isMoreTry)
                {
                    return result;
                }

                if (isMoreTry)
                {
                    System.Console.WriteLine("ERROR: input must be an integer value! Try again.");
                }

                // eger integer deger girmezse 2.iterasyonda hata aldigini söylesin diye ekledim
                isMoreTry = true;

                cStr = System.Console.ReadLine();

            } while (!int.TryParse(cStr, out result));

            return result;
        }
    }
}
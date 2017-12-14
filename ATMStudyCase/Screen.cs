using System;

namespace ATMStudyCase
{
    public class Screen
    {
        internal void DisplayMessage(string message)
        {
            Console.Write(message);
        }

        internal void DisplayMessageLine(string message)
        {
            Console.WriteLine(message);
        }

        // dolar olarak yazdirmak icin kullandim
        internal void DisplayDollarAmount(decimal amount)
        {
            Console.Write("${0:N}", amount);
        }

        // ekrani temizlemek icin kullandim
        internal void Clear()
        {
            System.Console.Clear();
        }

        // islemi uyutmak icin kullandim
        internal void Sleep(int millisecondsTimeout)
        {
            System.Threading.Thread.Sleep(millisecondsTimeout);
        }

        // islemi bekletmek icin kullandim
        internal void Back()
        {
            DisplayMessageLine("\n\nPress a key for back");
            System.Console.ReadLine();
        }
    }
}
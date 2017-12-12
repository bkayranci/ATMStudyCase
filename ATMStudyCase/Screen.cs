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

        internal void DisplayDollarAmount(decimal amount)
        {
            Console.Write("${0:N}", amount);
        }

        internal void Clear()
        {
            System.Console.Clear();
        }

        internal void Sleep(int millisecondsTimeout)
        {
            System.Threading.Thread.Sleep(millisecondsTimeout);
        }

        internal void Back()
        {
            DisplayMessageLine("\n\nPress a key for back");
            System.Console.ReadLine();
        }
    }
}
/**
 * @author Turkalp Burak KAYRANCIOGLU <bkayranci@gmail.com> 150101011
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMStudyCase
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * Deposit islemi para yatirir ve her defasinda belli belli bir miktar fazlasini yatiramaz.
             * Ayrica yatirilan para ATM ye gitmez yani yatirilan para cekilmez.
             * withdrawal para cekme olayini temsil eder ve atm de ki para miktarini belirler.
             * Account icin total balance suanki paramizi temsil eder ve avaible balance ise suanki paramiz dahil cekebilecegimiz tum parayi temsil eder.
             */

            ATM theATM = new ATM();
            theATM.Run();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Lib
{
    internal class clsInputValidate
    {
        public static bool IsNumberBetween(int Num, int From, int To)
        {
            if (Num >= From && Num <= To)
            {
                return true;
            }
            else
                return false;
        }


       public static int ReadNumberBetween(int From, int To, string ErrorMessage = "Invalide Number, Enter again\n")
        {
            int Number=0;
            while (!int.TryParse(Console.ReadLine(), out Number) || !IsNumberBetween(Number, From, To))
            {
               Console.WriteLine(ErrorMessage) ;
               Number = int.Parse(Console.ReadLine());
            }
            return Number;
        }
  


    }
}

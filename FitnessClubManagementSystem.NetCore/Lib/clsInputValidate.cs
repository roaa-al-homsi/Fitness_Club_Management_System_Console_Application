using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static int ReadNumberBetween(int From, int To, string ErrorMessage = "Invalid Number, Enter again\n")
        {
            int Number = 0;

            while (!int.TryParse(Console.ReadLine(), out Number)
                   || !IsNumberBetween(Number, From, To))
            {
                Console.WriteLine(ErrorMessage);
            }

            return Number;
        }

        // Date formats accepted from the user and from the data files: dd/MM/yyyy is what the screens ask for
        // (older records were saved that way too), yyyy-MM-dd is what the files are written in now.
        // Parsing with InvariantCulture keeps a date's meaning the same on every machine.
        static readonly string[] _DateFormats = { "d/M/yyyy", "d/M/yyyy H:mm:ss", "yyyy-M-d" };

        public static bool TryParseDate(string Text, out DateTime Date)
        {
            return DateTime.TryParseExact(Text?.Trim(), _DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out Date);
        }

        // Amounts use '.' as the decimal point on every machine, the same way they are stored in the files
        // (with the machine's culture, "1250.5" could be read as 12505 or rejected).
        public static bool TryParseDouble(string Text, out double Number)
        {
            return double.TryParse(Text, NumberStyles.Float, CultureInfo.InvariantCulture, out Number);
        }

    }
}

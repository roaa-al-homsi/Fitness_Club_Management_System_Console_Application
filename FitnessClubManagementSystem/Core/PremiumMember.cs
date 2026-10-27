using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core
{
    public class PremiumMember:Member
    {
        public bool HasPersonalTraining { get; set; }
        public int GuestPasses { get; set; }

        public PremiumMember(int id, string name, string email , DateTime joinDate , bool hasPersonalTraining , int guestPasses):base(id, name, email,joinDate)
        {
          this.HasPersonalTraining = hasPersonalTraining;
          this.GuestPasses = guestPasses;
 
        }
        public override double CalculateMonthlyFee()
        {
            double Mainfee = 5000;
            double feeGeustPassesForOneGeust = 500;
            double feeForAllGeustPasses = feeGeustPassesForOneGeust * GuestPasses;
            double feeIfHasPersonalTraining =(HasPersonalTraining)?2000 : 0 ;

            Mainfee = Mainfee + feeForAllGeustPasses + feeIfHasPersonalTraining; //fee is 3000 and discount is 10% 
            return Mainfee;
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            string TextHasPersonalTraining = (HasPersonalTraining) ? "Yes" : "NO";
            Console.WriteLine($"Has Personal Training ?  {TextHasPersonalTraining}");
            Console.WriteLine($"Number Guest Passes : {GuestPasses}");
        }



    }
}

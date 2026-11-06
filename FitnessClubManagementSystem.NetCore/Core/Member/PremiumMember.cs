using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core
{
    public class PremiumMember:Member
    {
        public bool HasPersonalTrainer { get; set; }
        public int GuestPasses { get; set; }

        public PremiumMember(int id, string name, string email , DateTime joinDate , bool hasPersonalTraining , int guestPasses):base(id, name, email,joinDate)
        {
          this.HasPersonalTrainer = hasPersonalTraining;
          this.GuestPasses = guestPasses;
 
        }
        public override double CalculateMonthlyFee()
        {
            const double Mainfee = 70;
            double fee = 0;

            double feeGeustPassesForOneGeust = 5;
            double feeForAllGeustPasses = feeGeustPassesForOneGeust * GuestPasses;
            double feeIfHasPersonalTrainer =(HasPersonalTrainer)?20 : 0 ;

            fee = Mainfee + feeForAllGeustPasses + feeIfHasPersonalTrainer; //fee is 70 
            return fee;
        }
        public override void PrintInfo()
        {
            base.PrintInfo();

            string textHasPersonalTraining = HasPersonalTrainer ? "Yes" : "No";

            Console.WriteLine(
                $" | Type: {"PREMIUM",-8} | " +
                $"Has Personal Training: {textHasPersonalTraining,-5} | " +
                $"Number Guest Passes: {GuestPasses,-3}"
            );
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core
{
    public class StandardMember : Member
    {
        public int FreezeDaysUsed { get; set; } = 0;

        public StandardMember(int Id, string name, string email, DateTime joinDate, int freezeDaysUsed) : base(Id, name, email, joinDate)
        {
            this.FreezeDaysUsed = freezeDaysUsed;


        }

        public override double CalculateMonthlyFee()
        {
            const double mainFee = 30;

            double fee = (FreezeDaysUsed == 0) ? mainFee - 5 : mainFee;//fee is 30 and discount is 5% 
            return fee;
        }
        public override void PrintInfo()
        {
            base.PrintInfo();

            Console.WriteLine(
                $" | Type: {"STANDARD",-8} | " +
                $"Freeze Days Used: {FreezeDaysUsed,-3}"
            );
        }
    }
}

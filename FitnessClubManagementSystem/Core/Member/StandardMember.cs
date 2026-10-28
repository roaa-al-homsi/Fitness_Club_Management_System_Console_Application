using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core
{
    public class StandardMember:Member
    {
        public int FreezeDaysUsed { get; set; } = 0;

        public StandardMember (int Id,string name, string email, DateTime joinDate,int freezeDaysUsed) :base(Id, name, email, joinDate)
        {
            this.FreezeDaysUsed=freezeDaysUsed;


        }
       
        public override double CalculateMonthlyFee()
        {
            double fee = 3000;
            fee = (FreezeDaysUsed == 0) ? fee * 0.9 : fee;//fee is 3000 and discount is 10% 
            return fee;
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Type Member : STANDARD.");
            Console.WriteLine($"Freeze Days Used is : {FreezeDaysUsed}");

        }
    }
} 

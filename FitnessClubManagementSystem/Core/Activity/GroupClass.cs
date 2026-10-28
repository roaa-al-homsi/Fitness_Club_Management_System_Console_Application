using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core.Activity
{
    public class GroupClass:Activity
    {
        public string InstructorName { get; set; }
        public string DifficultyLevel { get; set; }//Beginner  , Advanced

        public GroupClass(int id, string name, TimeSpan durationMinutes, int capacity,string instructorName, string difficultyLevel):base(id,name,durationMinutes,capacity)
        {
            this.InstructorName = instructorName;
            this.DifficultyLevel = difficultyLevel;
        }

        public override double CalculateSessionCost()
        {
            return 0;

        }
        public override void PrintInfo()
        {
            base.PrintInfo();

            Console.WriteLine($"Type: Group ");
            Console.WriteLine($"Instructor Name: {InstructorName}");
            Console.WriteLine($"Difficulty Level: {DifficultyLevel}");
            Console.WriteLine("*******************************");
        }





    }
}

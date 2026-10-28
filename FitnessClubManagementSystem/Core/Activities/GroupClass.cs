using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core.Activities
{
    public class GroupClass:Activity
    {
        public string InstructorName { get; set; }
        public string DifficultyLevel { get; set; }//Beginner  , Advanced

        public GroupClass(int id, string name, int durationMinutes, int capacity,string instructorName, string difficultyLevel):base(id,name,durationMinutes,capacity)
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
            Console.Write($"Type: {"GROUP CLASS",-20} | " + $"Instructor: {InstructorName,-15} | " + $"Difficulty: {DifficultyLevel,-10}");
        }





    }
}

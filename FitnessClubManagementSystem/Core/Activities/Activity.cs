using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core.Activities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationMinutes { get; set; }
        public int Capacity { get; set; }


        public Activity(int id, string name, int durationMinutes,int capacity)
        {
            this.Id= id;
            this.Name= name;
            this.DurationMinutes= durationMinutes;
            this.Capacity= capacity;

        }
        public virtual double CalculateSessionCost()
        {
            return 0;

        }

        public virtual void PrintInfo()
        {
          Console.Write($"ID: {Id,-3} | " + $"Name: {Name,-20} | " + $"Duration: {DurationMinutes,-3} min | " + $"Capacity: {Capacity,-3} | " + $"Session Cost: {CalculateSessionCost(),-10}");
        }


    }
}

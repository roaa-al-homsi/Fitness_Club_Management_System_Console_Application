using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core.Activity
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
            Console.WriteLine("********* Activity Info *********");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Duration Minutes: {DurationMinutes}");
            Console.WriteLine($"Capacity: {Capacity}");
            Console.WriteLine($"Session Cost: {CalculateSessionCost()}");
           
        }


    }
}

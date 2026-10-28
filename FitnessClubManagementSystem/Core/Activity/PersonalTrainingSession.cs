using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core.Activity
{
    public class PersonalTrainingSession :Activity
    {
        public int TrainerId { get; set; }
        public int  HourlyRate { get; set; }

        public PersonalTrainingSession(int id, string name, TimeSpan durationMinutes, int capacity, int trainerId, int hourlyRate) : base(id, name, durationMinutes, capacity)
        {
            this.TrainerId = trainerId;
            this.HourlyRate = hourlyRate;

        }
        public override double CalculateSessionCost()
        {
            return 0;

        }
        public override void PrintInfo()
        {
            base.PrintInfo();

            Console.WriteLine($"Type: Personal Training Session ");
            Console.WriteLine($"Trainer Id: {TrainerId}");
            Console.WriteLine($"Hourly Rate: {HourlyRate}");
            Console.WriteLine("*******************************");
        }

    }
}

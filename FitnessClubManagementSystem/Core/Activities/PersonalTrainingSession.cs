using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core.Activities
{
    public class PersonalTrainingSession :Activity
    {
        public int TrainerId { get; set; }
        public double  HourlyRate { get; set; }

        public PersonalTrainingSession(int id, string name, int durationMinutes, int capacity, int trainerId, double hourlyRate) : base(id, name, durationMinutes, capacity)
        {
            this.TrainerId = trainerId;
            this.HourlyRate = hourlyRate;

        }
        public override double CalculateSessionCost()
        {
            double cost = HourlyRate * (DurationMinutes /60.0);
          
            return cost;

        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.Write($"Type: {"PERSONAL TRAINING",-20} | " + $"Trainer ID: {TrainerId,-10} | " + $"Hourly Rate: {HourlyRate,-10}");
        }



    }
}

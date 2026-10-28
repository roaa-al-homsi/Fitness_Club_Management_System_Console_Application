using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core
{
    public class Trainer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public double Salary { get; set; }

        int[] PersonalTrainingSessionsAsigned;

        public Trainer(int id, string name , string specialty, double salary)
        {
            this.Id = id;
            this.Name = name;
            this.Specialty = specialty;
            this.Salary = salary;  
        }
        
        public void PrintInfo()
        {

            Console.Write($"ID: {Id,-3} | " + $"Name: {Name,-20} | " + $"Specialty: {Specialty,-20} min | " + $"Salary: {Salary,-3}");

        }

    }
}

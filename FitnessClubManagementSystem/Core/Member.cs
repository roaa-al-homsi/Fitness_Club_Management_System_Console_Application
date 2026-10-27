using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessClubManagementSystem;

namespace FitnessClubManagementSystem.Core
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }


        public Member(int Id, string name, string email,DateTime joinDate)
        {
            this.Id = Id;
            this.Name = name;
            this.Email = email;
            this.JoinDate = joinDate;
        }
        public virtual double CalculateMonthlyFee()
        {
            return 0;
        }
        public virtual void PrintInfo()
        {
            double MonthlyFee = this.CalculateMonthlyFee();
            //  Console.WriteLine($"ID: {this.Id}\n , Name: {this.Name}, Email: {this.Email} , JoinDate: {this.JoinDate} , MonthlyFee : {MonthlyFee}");
            Console.WriteLine("********* Member Info *********");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"JoinDate: {JoinDate}");
            Console.WriteLine($"MonthlyFee: {MonthlyFee}");
            Console.WriteLine("*******************************");

        }

     




    }
}

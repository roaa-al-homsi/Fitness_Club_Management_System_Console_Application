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
            Console.Write(
                $"ID: {Id,-3} | " +
                $"Name: {Name,-15} | " +
                $"Email: {Email,-30} | " +
                $"Join Date: {JoinDate.ToShortDateString(),-10} | " +
                $"Monthly Fee: {CalculateMonthlyFee(),-6}"
            );
        }






    }
}

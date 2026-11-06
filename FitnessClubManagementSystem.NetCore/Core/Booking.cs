using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FitnessClubManagementSystem.Core
{
    public class Booking
    {
        public int Id { get; set; }
        public int MemberId { get; set; }//composition
        public int ActivityId { get; set; }//compositon
        public DateTime Date { get; set; }
     
        private string _status;

        public string Status
        {
            get { return _status; }

            set
            {
                if (value == "Cancelled" ||
                    value == "Confirmed" ||
                    value == "InProgress")
                {
                    _status = value;
                }
                else
                {
                    throw new ArgumentException("Invalid booking status!");
                }
            }
        }

        public Booking(int id, int memberId,int activityId,DateTime date, string status)
        {
            this.Id = id;
            this.ActivityId = activityId;
            this.MemberId = memberId;
            this.Date= date;
            this.Status = status;

        }

        public void PrintInfo()
        {
            Console.Write($"ID: {Id,-3} | " + $"Member Id: {MemberId,-3} | " + $"Activity Id: {ActivityId,-3} | " + $"Date: {Date,-30} | "+ $"Status: {Status}");

        }

    }
}

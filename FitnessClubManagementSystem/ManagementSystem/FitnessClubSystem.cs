using FitnessClubManagementSystem.ManagementSystem;
using FitnessClubManagementSystem.Core.Activities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Core
{
    public class FitnessClubSystem
    {
        private ClubStaff _clubStaff;

        public FitnessClubSystem()
        {
            _clubStaff = new ClubStaff(1, "Admin", "Manager");
        }

        // AddMember() => _clubStaff.AddMember()
        // AddActivity() => _clubStaff.AddActivity()
        //......

        public const int MaxMembers = 50;
        public static int MemberCount = 0;//Number Member In Array     to detect how many member in array members because size of array is 50 but we don't use all this capacity.
        public static Member[] Members = new Member[MaxMembers];//to storing all members that exist in file. 

        public const int MaxActivity = 50;
        public static int ActivityCount = 0;//  Number Activity In Array to detect how many member in array activities because size of array is 50 but we don't use all this capacity.
        public static Activity[] Activities = new Activity[MaxActivity];//to storing all activities that exist in file. 

        public const int MaxTrainers = 50;
        public static int TrainerCount = 0;
        public static Trainer[] Trainers = new Trainer[MaxTrainers];

        public const int MaxBookings = 50;
        public static int BookingCount = 0;//to detect how many Bookings in array Bookings because size of array is 50 but we don't use all this capacity.
        public static Booking[] Bookings = new Booking[MaxBookings];//to storing all Bookings that exist in file. 



        public void LoadingDataApplication()
        {
            //in method LoadDataFromFileToArrayMembers it's load all members from file to members array an update to NumberMembersInArray.
            Members = FileManager.LoadMembers();

            //in method LoadDataFromFileToArrayTrainers it's load all Trainers from file to Trainers array an update to NumberTrainersInArray
            Trainers = FileManager.LoadTrainers();

            //in method LoadDataFromFileToArrayActivities it's load all activities from file to activites array an update to NumberActivitiesInArray.
            Activities = FileManager.LoadActivities();


            //in method LoadDataFromFileToArrayTrainers it's load all Trainers from file to Trainers array an update to NumberTrainersInArray
            Bookings = FileManager.LoadBookings();
           
            _clubStaff.UpdateArraysReferences();
            //Her responsibility goes through the activities, looking for personal training sessions, and then assigning each session to the trainer who owns the TrainerId.
            AssignSessionsToTrainers();

            //ShowAssignedSessionsForAllTrainers();  
        }

        //Her responsibility goes through the activities, looking for personal training sessions, and then assigning each session to the trainer who owns the TrainerId.
        public  void AssignSessionsToTrainers()
        {
            for (int i = 0; i < TrainerCount; i++)
            {
                Trainers[i].NumSessionsAssignedInArray = 0;
            }

            for (int i = 0; i < ActivityCount; i++)
            {
                if (Activities[i] is PersonalTrainingSession)
                {
                    PersonalTrainingSession session =
                        (PersonalTrainingSession)Activities[i];

                    Trainer trainer = _clubStaff.FindTrainer(session.TrainerId);

                    if (trainer != null)
                    {
                        if (!trainer.AssignSession(session))
                        {
                            Console.WriteLine(
                                $"Cannot assign session {session.Id} to trainer {trainer.Id}. Trainer is full.");
                        }
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Trainer with ID {session.TrainerId} was not found for session {session.Id}.");
                    }
                }
            }
        }

        //public static void ShowAssignedSessionsForAllTrainers()
        //{
        //    for (int i = 0; i < NumberTrainersInArray; i++)
        //    {
        //        Trainer trainer = Trainers[i];

        //        Console.WriteLine($"\nTrainer ID: {trainer.Id} - Name: {trainer.Name}");

        //        if (trainer.NumSessionsAssignedInArray == 0)
        //        {
        //            Console.WriteLine("No sessions assigned.");
        //            continue;
        //        }

        //        Console.WriteLine("Assigned Sessions IDs:");

        //        for (int j = 0; j < trainer.NumSessionsAssignedInArray; j++)
        //        {
        //            Console.WriteLine($"Session ID: {trainer.SessionsAssigned[j].Id}");
        //        }
        //    }
        //}

        /* Member */
        public void ShowAllMembers()
        {
            _clubStaff.ShowAllMembers();
        }

        public void AddMember()
        {
            _clubStaff.AddMember();

        }

        public bool IsMemberExist(int Id)
        {
            return _clubStaff.IsMemberExist(Id);
        }

        public void FindMemberById()
        {
            _clubStaff.FindMemberById();
        }


        /* Activity */
        public void ShowAllActivity()
        {
            _clubStaff.ShowAllActivity();
        }

        public void AddActivity()
        {
            _clubStaff.AddActivity();
        }
        public bool IsActivityExist(int Id)
        {
            return _clubStaff.IsActivityExist(Id);
        }

        public void FindActivityById()
        {
            _clubStaff.FindActivityById();
        }


        /*TRAINERS*/
        public void ShowAllTrainers()
        {
            _clubStaff.ShowAllTrainers();
        }

        public void AddTrainer()
        {
            _clubStaff.AddTrainer();

        }

        bool IsTrainerExist(int Id)
        {
            return _clubStaff.IsTrainerExist(Id);
        }

        /*BOOKINGS*/
        public void ShowAllBookings()
        {
            _clubStaff.ShowAllBookings();
        }

        public void AddBooking()
        {
            _clubStaff.AddBooking();
        }

        /**/
        public double GetProjectedMonthlyRevenue()
        {
            double monthlyRevenue = 0;
            for (int i = 0; i < MemberCount; i++)
            {
                monthlyRevenue += Members[i].CalculateMonthlyFee();

            }
            return monthlyRevenue;
        }
      

    }
}

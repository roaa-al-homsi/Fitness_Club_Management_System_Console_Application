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
    public static class FitnessClubSystem
    {
        public const int MaxMembers = 50;
        public static int NumberMembersInArray = 0;//to detect how many member in array members because size of array is 50 but we don't use all this capacity.
        public static Member[] Members = new Member[MaxMembers];//to storing all members that exist in file. 

        public const int MaxActivity = 50;
        public static int NumberActivityInArray = 0;//to detect how many member in array activities because size of array is 50 but we don't use all this capacity.
        public static Activity[] Activities = new Activity[MaxActivity];//to storing all activities that exist in file. 

        public const int MaxTrainers = 50;
        public static int NumberTrainersInArray = 0;
        public static Trainer[] Trainers = new Trainer[MaxTrainers];
        

        
        
        public static void LoadingDataApplication()
        { 
            //in method LoadDataFromFileToArrayMembers it's load all members from file to members array an update to NumberMembersInArray.
            Members = FileManager.LoadDataFromFileToArrayMembers();

            //in method LoadDataFromFileToArrayActivities it's load all activities from file to activites array an update to NumberActivitiesInArray.
            Activities = FileManager.LoadDataFromFileToArrayActivities();

            //in method LoadDataFromFileToArrayTrainers it's load all Trainers from file to Trainers array an update to NumberTrainersInArray
            Trainers = FileManager.LoadDataFromFileToArrayTrainers();
        }

        /* Member */
        public static void ShowAllMembers()
        {
           // Members = FileManager.LoadDataFromFileToArrayMembers();

            for(int i = 0;i<=NumberMembersInArray-1;i++ )
            {
                if (Members[i] is StandardMember)
                {
                    StandardMember standardMember = (StandardMember)Members[i];//convert to standard object because localMembers it's type member and this member will be is standard or premium . 

                    // Console.WriteLine($"ID: {standardMember.Id} | Name: {standardMember.Name}| Email : {standardMember.Email}  |  Join Date : {standardMember.JoinDate} | Type : STANDARD  | Freeze Days Used : {standardMember.FreezeDaysUsed} | Monthly Fee : {standardMember.CalculateMonthlyFee()}");
                    Console.WriteLine(
                                      $"ID: {standardMember.Id,-3} | " +
                                      $"Name: {standardMember.Name,-15} | " +
                                      $"Email: {standardMember.Email,-30} | " +
                                      $"Join Date: {standardMember.JoinDate.ToShortDateString(),-10} | " +
                                      $"Type: {"STANDARD",-8} | " +
                                      $"Freeze Days Used: {standardMember.FreezeDaysUsed,-3} | " +
                                      $"Monthly Fee: {standardMember.CalculateMonthlyFee()}"
                                      );

                }

                else if (Members[i] is PremiumMember)
                { 
                    PremiumMember premiumMember = (PremiumMember)Members[i];
                    //  Console.WriteLine($"ID: {premiumMember.Id} | Name: {premiumMember.Name}   | Email : {premiumMember.Email}  |  Join Date : {premiumMember.JoinDate} | Type : PREMIUM  | Has Personal Training : {premiumMember.HasPersonalTraining} | Number Guest Passes : {premiumMember.GuestPasses} | Monthly Fee : {premiumMember.CalculateMonthlyFee()}");
                    Console.WriteLine(
                                      $"ID: {premiumMember.Id,-3} | " +
                                      $"Name: {premiumMember.Name,-15} | " +
                                      $"Email: {premiumMember.Email,-30} | " +
                                      $"Join Date: {premiumMember.JoinDate.ToShortDateString(),-10} | " +
                                      $"Type: {"PREMIUM",-8} | " +
                                      $"Has Personal Training: {premiumMember.HasPersonalTraining,-5} | " +
                                      $"Number Guest Passes: {premiumMember.GuestPasses,-3} | " +
                                      $"Monthly Fee: {premiumMember.CalculateMonthlyFee()}"
                                     );
                }

            }

            Console.WriteLine("\n                                     **************************************");
            Console.WriteLine("                                       TOTAL MEMBER IN SYSTEM: " + FitnessClubSystem.NumberMembersInArray);

        }
        static Member _ReadInfoNewMember( )
        {
           
            string type, name, email = "";
            DateTime joinDate = DateTime.Now;

            int freezeDaysUsed, guestPasses = 0;
            bool hasPersonalTrainer= false;
            int newId = NumberMembersInArray + 1;

            Console.WriteLine("What type of new member is it  [STANDARD OR PREMIUM]?");
            type=Console.ReadLine();

            Console.WriteLine("Enter member name : ");
            name = Console.ReadLine();

            Console.WriteLine("Enter member email : ");
            email = Console.ReadLine();

            Console.WriteLine("Enter join date for member (dd/MM/yyyy): ");
            string inputDate = Console.ReadLine();

            while (!DateTime.TryParse(inputDate, out joinDate))
            {
                Console.WriteLine("Invalid date! Please enter a valid date:");
                inputDate = Console.ReadLine();
            }

            switch (type)
            {
                case "STANDARD":

                     Console.WriteLine("Enter number freeze days used : ");
                     freezeDaysUsed = int.Parse(Console.ReadLine());
                     
                     StandardMember standardMember = new StandardMember(newId,name, email,joinDate,freezeDaysUsed);
                    
                     return standardMember;

                case "PREMIUM":
                    Console.WriteLine("Has personal training? [Yes Or No]");
                    string textHasPersonalTraining = Console.ReadLine();

                    hasPersonalTrainer = textHasPersonalTraining == "Yes" || textHasPersonalTraining == "yes" ? true : false;

                    Console.WriteLine("Enter number guest passes: ");
                    guestPasses = int.Parse(Console.ReadLine());


                    PremiumMember premiumMember = new PremiumMember(newId, name, email, joinDate, hasPersonalTrainer, guestPasses);

                    return premiumMember;

                default:

                    return null;
            } 
        }
        public static void  AddMember()
        {
            if(NumberMembersInArray>=MaxMembers)
            {
                Console.WriteLine("YOU CAN'T ADDING A NEW MEMBER TO FILE, FILE IS FULLING");
                return; 
            }


           Member NewMember = _ReadInfoNewMember();
            if (NewMember == null)
            {
                Console.WriteLine("INVALID INFO MEMBER, MEMBER IS NULL.");
                return;
            }
           if ( FileManager.SaveMember(FileManager.ConvertMemberObjectToStLine(NewMember)))
            {
                Members[NumberMembersInArray] = NewMember;
                ++NumberMembersInArray;
                Console.WriteLine("Success Saving Member To File..");
            }
           else
            {
                Console.WriteLine("Fail Saving Member To File!!!");

            }

        }
        static Member _FindMember(int id)//Find Member By Id
        {
            for (int i = 0; i <= NumberMembersInArray - 1; i++)
            {
                if (Members[i].Id == id)
                {
                    return Members[i];

                }
            }

            return null;
        }
        static int _ReadIdMember()
        {
            Console.WriteLine($"Please, Enter Id member : ");
            int id = 0;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Enter correct number please..., try again: ");
                Console.WriteLine($"Please, Enter Id member : ");
            }
            return id;
        }
        public static void FindMemberById ()
        {
            Member member = _FindMember(_ReadIdMember());
         
            if(member == null )
            {
              Console.WriteLine($"There is no exist member with this Id ");
                return;
            }
            //Here member is exist and fulling .
            member.PrintInfo();
        }
        

        /* Activity */

       
        public static void ShowAllActivity()
        {
            for (int i = 0; i < NumberActivityInArray; i++)
            {
                Activities[i].PrintInfo();
                Console.WriteLine(" ");

            }
            Console.WriteLine("\n                                     **************************************");
            Console.WriteLine("                                       TOTAL ACTIVITY IN SYSTEM: " + FitnessClubSystem.NumberActivityInArray);

        }
       
        static Activity _ReadInfoNewActivity()
        {
            string type, name, instructorName, difficultyLevel = "";
            int durationMinutes, capacity, trainerId = 0;
            double hourlyRate = 0;
            int newId = NumberActivityInArray + 1;

            Console.WriteLine("Enter type activity (Group or Personal Training) (g|pt) ?");
            type = Console.ReadLine();
            while (type != "g" && type != "pt")
            {
                Console.WriteLine("Invalid input!! , Please enter g or pt only.");
                type = Console.ReadLine();
            }

            Console.WriteLine("Enter activity name : ");
            name = Console.ReadLine();

            Console.WriteLine("Enter duration minutes : ");
            string inputeDurationMinutes = Console.ReadLine();
            while (!int.TryParse(inputeDurationMinutes, out durationMinutes))
            {
                Console.WriteLine("Invalid input!! , Please enter a correct input as number.");
                inputeDurationMinutes = Console.ReadLine();
            }

            Console.WriteLine("Enter capacity : ");
            string inputCapacity = Console.ReadLine();
            while (!int.TryParse(inputCapacity, out capacity))
            {
                Console.WriteLine("Invalid input!! , Please enter a correct input as number.");
                inputCapacity = Console.ReadLine();
            }

            switch (type)
            {
                case "g":
                    Console.WriteLine("Enter an instructor name : ");
                    instructorName = Console.ReadLine();

                    Console.WriteLine("Enter difficulty level Beginner or Advanced [beg | adv] ?");
                    difficultyLevel = Console.ReadLine();
                    while (difficultyLevel != "beg" && difficultyLevel != "adv")
                    {
                        Console.WriteLine("Invalid input!! , Please enter beg or adv only.");
                        difficultyLevel = Console.ReadLine();
                    }

                    return new GroupClass(newId, name, durationMinutes, capacity, instructorName, difficultyLevel);

                case "pt":
                    Console.WriteLine("Enter trainer Id : ");
                    string inputTrainerId = Console.ReadLine();
                    while (!int.TryParse(inputTrainerId, out trainerId))
                    {
                        Console.WriteLine("Invalid input!! , Please enter a correct input as number.");
                        inputTrainerId = Console.ReadLine();
                    }
                    // must method check if this trainer id exist or not . 


                    Console.WriteLine("Enter hourly rate  : ");
                    string inputHourlyRate = Console.ReadLine();
                    while (!double.TryParse(inputHourlyRate, out hourlyRate))
                    {
                        Console.WriteLine("Invalid input!! , Please enter a correct input as number.");
                        inputHourlyRate = Console.ReadLine();
                    }

                    return new PersonalTrainingSession(newId, name, durationMinutes, capacity, trainerId, hourlyRate);

                default:
                    return null;
            }

        }
        public static void AddActivity()
        {
            if(NumberActivityInArray>=MaxActivity)
            {
                Console.WriteLine("YOU CAN'T ADDING A NEW ACTIVITY TO FILE, FILE IS FULLING");
                return;
            }

            Activity NewActivity = _ReadInfoNewActivity();
        
            if (NewActivity == null)
            {
                Console.WriteLine("There is a wrong , an activity is null");
                return;
            }

            if (FileManager.SaveActivity(FileManager.ConvertActivityObjectToStLine(NewActivity)))
            {
                Activities[NumberActivityInArray] = NewActivity;
                ++NumberActivityInArray;
                Console.WriteLine("Success Saving Activity To File..");
            }
            else
            {
                Console.WriteLine("Fail Saving Activity To File!!!");

            }
        }

        static int _ReadIdActivity()
        {
            Console.WriteLine($"Please, Enter Id activity : ");
            int id = 0;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Enter correct number please..., try again: ");
                Console.WriteLine($"Please, Enter Id activity : ");
            }
            return id;
        }
        static Activity _FindActivity(int ActivityId)
        {
            for(int i = 0;i<NumberActivityInArray; i++)
            {
                if (Activities[i].Id== ActivityId)
                {
                    return Activities[i];
                }
            }
            return null;
        }
        public static void FindActivityById()
        {
            Activity activity = _FindActivity(_ReadIdActivity());
            if(activity == null)
            {
                Console.WriteLine($"There is no exist activity with this Id ");
                return;
            }
            //here we found the activity and print info for this activity.
            activity.PrintInfo();
        }

        /*TRAINERS*/

        public static void ShowAllTrainers()
        {
            for (int i = 0; i < NumberTrainersInArray; i++)
            {
                Trainers[i].PrintInfo();
                Console.WriteLine(" ");

            }
            Console.WriteLine("\n                                     **************************************");
            Console.WriteLine("                                       TOTAL TRAINERS IN SYSTEM: " + FitnessClubSystem.NumberTrainersInArray);

        }


    }
}

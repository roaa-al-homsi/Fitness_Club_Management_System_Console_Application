using FitnessClubManagementSystem.Core;
using FitnessClubManagementSystem.Core.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.ManagementSystem
{
    public class ClubStaff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public Member[] Members;
        public Activity[] Activities;
        public Trainer[] Trainers;
        public Booking[] Bookings;

        public ClubStaff(int Id, string name, string position)
        {
            this.Id = Id;
            this.Name = name;
            this.Position = position;

            Members = FitnessClubSystem.Members;
            Activities = FitnessClubSystem.Activities;
            Trainers = FitnessClubSystem.Trainers;
            Bookings = FitnessClubSystem.Bookings;
        }

        public void UpdateArraysReferences()
        {
            Members = FitnessClubSystem.Members;
            Activities = FitnessClubSystem.Activities;
            Trainers = FitnessClubSystem.Trainers;
            Bookings = FitnessClubSystem.Bookings;
        }
        /* Members */
        public void ShowAllMembers()
        {
            for (int i = 0; i < FitnessClubSystem.MemberCount; i++)
            {
                Members[i].PrintInfo();
            }

            Console.WriteLine("\n                                     **************************************");
            Console.WriteLine("                                       TOTAL MEMBER IN SYSTEM: " + FitnessClubSystem.MemberCount);
        }

        private Member _ReadInfoNewMember()
        {

            string type, name, email = "";
            DateTime joinDate = DateTime.Now;

            int freezeDaysUsed, guestPasses = 0;
            bool hasPersonalTrainer = false;
            int newId = FitnessClubSystem.MemberCount + 1;

            Console.WriteLine("What type of new member is it STANDARD OR PREMIUM [s | p]?");
            type = Console.ReadLine();
            while (type != "s" && type != "p")
            {
                Console.WriteLine("INVALID INPUT!!! ,ENTER s OR p ONLY ");
                type = Console.ReadLine();
            }


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
                case "s":

                    Console.WriteLine("Enter number freeze days used : ");
                    string textfreezeDaysUsed = Console.ReadLine();
                    while (!int.TryParse(textfreezeDaysUsed, out freezeDaysUsed) || freezeDaysUsed < 0)
                    {
                        Console.WriteLine("INVALID INPUT!!!, ENTER A CORRECT NUMBER.");
                        textfreezeDaysUsed = Console.ReadLine();
                    }

                    StandardMember standardMember = new StandardMember(newId, name, email, joinDate, freezeDaysUsed);

                    return standardMember;

                case "p":
                    Console.WriteLine("Has personal training? [y Or n]");
                    string textHasPersonalTraining = Console.ReadLine();

                    while (textHasPersonalTraining != "y" && textHasPersonalTraining != "n")
                    {
                        Console.WriteLine("INVALID INPUT!!!,ENTER y OR n ONLY..");
                        textHasPersonalTraining = Console.ReadLine();
                    }

                    hasPersonalTrainer = textHasPersonalTraining == "y" ? true : false;

                    Console.WriteLine("Enter number guest passes: ");
                    string textguestPasses = Console.ReadLine();

                    while (!int.TryParse(textguestPasses, out guestPasses) || guestPasses < 0)
                    {
                        Console.WriteLine("INVALID INPUT!!!,ENTER A CORRECT NUMBER!!");
                        textguestPasses = Console.ReadLine();
                    }

                    PremiumMember premiumMember = new PremiumMember(newId, name, email, joinDate, hasPersonalTrainer, guestPasses);

                    return premiumMember;

                default:

                    return null;
            }
        }
        public void AddMember()
        {
            if (FitnessClubSystem.MemberCount >= FitnessClubSystem.MaxMembers)
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
            if (FileManager.SaveMember(FileManager.ConvertMemberObjectToStLine(NewMember)))
            {
                Members[FitnessClubSystem.MemberCount] = NewMember;
                ++FitnessClubSystem.MemberCount;
                Console.WriteLine("Success Saving Member To File..");
            }
            else
            {
                Console.WriteLine("Fail Saving Member To File!!!");

            }

        }
        public bool IsMemberExist(int Id)
        {
            Member member = _FindMember(Id);
            return (member != null);
        }
        private Member _FindMember(int id)//Find Member By Id
        {
            for (int i = 0; i <= FitnessClubSystem.MemberCount - 1; i++)
            {
                if (Members[i].Id == id)
                {
                    return Members[i];

                }
            }

            return null;
        }
        private int _ReadIdMember()
        {
            int id = -1;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Enter correct number please..., try again: ");
                Console.WriteLine($"Please, Enter Id member : ");
            }
            return id;
        }
        public void FindMemberById()
        {
            Console.WriteLine($"Please, Enter Id member : ");

            int memberId = _ReadIdMember();
            if (memberId == -1)
            {
                Console.WriteLine("INVALID INPUT!!! ");
                return;
            }
            Member member = _FindMember(memberId);
            if (member == null)
            {
                Console.WriteLine($"There is no exist member with this Id ");
                return;
            }
            else
            {
                Console.WriteLine("Member Is Found..");
            }
            //Here member is exist and fulling .
            member.PrintInfo();
        }


        /* Activity */
        public void ShowAllActivity()
        {
            for (int i = 0; i < FitnessClubSystem.ActivityCount; i++)
            {
                Activities[i].PrintInfo();
                Console.WriteLine(" ");

            }
            Console.WriteLine("\n                                     **************************************");
            Console.WriteLine("                                       TOTAL ACTIVITY IN SYSTEM: " + FitnessClubSystem.ActivityCount);

        }
        private Activity _ReadInfoNewActivity()
        {
            string type, name, instructorName, difficultyLevel = "";
            int durationMinutes, capacity, trainerId = 0;
            double hourlyRate = 0;
            int newId = FitnessClubSystem.ActivityCount + 1;

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
            while (!int.TryParse(inputeDurationMinutes, out durationMinutes) || durationMinutes <= 0)

            {
                Console.WriteLine("Invalid input!! , Please enter a correct input as number.");
                inputeDurationMinutes = Console.ReadLine();
            }

            Console.WriteLine("Enter capacity : ");
            string inputCapacity = Console.ReadLine();
            while (!int.TryParse(inputCapacity, out capacity) || capacity <= 0)
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

                    if (FitnessClubSystem.TrainerCount == 0)
                    {
                        Console.WriteLine("THERE IS NO ANY TRAINER IN SYSTEM");
                        return null;
                    }
                    Console.WriteLine("Enter trainer Id : ");
                    trainerId = _ReadIdTrainer();

                    // must method check if this trainer id exist or not . 
                    while (!IsTrainerExist(trainerId))
                    {
                        Console.WriteLine($"There is no trainer with this Id {trainerId}");
                        Console.WriteLine("Enter trainer Id : ");
                        trainerId = _ReadIdTrainer();
                    }
                    Trainer trainer = FindTrainer(trainerId);
                    if (trainer == null)
                    {
                        return null;
                    }
                    if (!trainer.HasAvailableSession())
                    {
                        Console.WriteLine("This trainer has no available session.");
                        return null;
                    }


                    Console.WriteLine("Enter hourly rate  : ");
                    string inputHourlyRate = Console.ReadLine();
                    while (!double.TryParse(inputHourlyRate, out hourlyRate) || hourlyRate <= 0)
                    {
                        Console.WriteLine("Invalid input!! , Please enter a correct input as number.");
                        inputHourlyRate = Console.ReadLine();
                    }

                    return new PersonalTrainingSession(newId, name, durationMinutes, capacity, trainerId, hourlyRate);

                default:
                    return null;
            }

        }
        public void AddActivity()
        {
            if (FitnessClubSystem.ActivityCount >= FitnessClubSystem.MaxActivity)
            {
                Console.WriteLine("YOU CAN'T ADDING A NEW ACTIVITY TO FILE, FILE IS FULLING");
                return;
            }

            Activity NewActivity = _ReadInfoNewActivity();

            if (NewActivity == null)
            {
                Console.WriteLine("INVALID ACTIVITY!!!");
                return;
            }

            if (FileManager.SaveActivity(FileManager.ConvertActivityObjectToStLine(NewActivity)))
            {
                Activities[FitnessClubSystem.ActivityCount] = NewActivity;
                ++FitnessClubSystem.ActivityCount;
                Console.WriteLine("Success Saving Activity To File..");
                if (NewActivity is PersonalTrainingSession)
                {
                    PersonalTrainingSession personalTraining = (PersonalTrainingSession)NewActivity;
                    Trainer trainer = FindTrainer(personalTraining.TrainerId);
                    if (trainer != null)
                    {
                        if (!trainer.AssignSession(personalTraining))
                        {
                            Console.WriteLine("Failed to assign session to trainer.");

                        }

                    }

                }
            }
            else
            {
                Console.WriteLine("Fail Saving Activity To File!!!");

            }
        }

        public bool IsActivityExist(int Id)
        {
            Activity activity = _FindActivity(Id);
            return (activity != null);
        }
        private int _ReadIdActivity()
        {
            int id = 0;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Enter correct number please..., try again: ");
                Console.WriteLine($"Please, Enter Id activity : ");
            }
            return id;
        }
        private Activity _FindActivity(int ActivityId)
        {
            for (int i = 0; i < FitnessClubSystem.ActivityCount; i++)
            {
                if (Activities[i].Id == ActivityId)
                {
                    return Activities[i];
                }
            }
            return null;
        }
        public void FindActivityById()
        {
            Console.WriteLine($"Please, Enter Id activity : ");
            int activityId = _ReadIdActivity();
            if (!IsActivityExist(activityId))
            {
                Console.WriteLine($"There is no exist activity with this Id ");
                return;
            }

            Activity activity = _FindActivity(activityId);
            if (activity == null)
            {
                Console.WriteLine($"INVALID ACTIVITY");
                return;
            }
            else
            {
                Console.WriteLine("Activity Is Found..");
            }
            //here we found the activity and print info for this activity.
            activity.PrintInfo();
        }



        /*TRAINERS*/
        public void ShowAllTrainers()
        {
            for (int i = 0; i < FitnessClubSystem.TrainerCount; i++)
            {
                Trainers[i].PrintInfo();
                Console.WriteLine(" ");

            }
            Console.WriteLine("\n                                     **************************************");
            Console.WriteLine("                                       TOTAL TRAINERS IN SYSTEM: " + FitnessClubSystem.TrainerCount);

        }
        Trainer _ReadInfoTrainer()
        {
            string name, specialty = "";
            double salary = 0;
            int newId = FitnessClubSystem.TrainerCount + 1;

            Console.WriteLine("Enter trainer name : ");
            name = Console.ReadLine();

            Console.WriteLine("Enter specialty : ");
            specialty = Console.ReadLine();

            Console.WriteLine("Enter salary: ");
            string inputSalary = Console.ReadLine();
            while (!double.TryParse(inputSalary, out salary) || salary < 0)
            {
                Console.WriteLine("Invalid salary! Enter a non-negative number:");
                inputSalary = Console.ReadLine();
            }
            return new Trainer(newId, name, specialty, salary);
        }
        int _ReadIdTrainer()
        {
            int trainerId = 0;
            string inputTrainerId = Console.ReadLine();
            while (!int.TryParse(inputTrainerId, out trainerId))
            {
                Console.WriteLine("Invalid input!! , Please enter a correct input as number.");
                inputTrainerId = Console.ReadLine();
            }
            return trainerId;
        }
        public void AddTrainer()
        {
            if (FitnessClubSystem.TrainerCount >= FitnessClubSystem.MaxTrainers)
            {
                Console.WriteLine("YOU CAN'T ADDING A NEW TRAINERS TO FILE, FILE IS FULLING");
                return;
            }

            Trainer newTrainer = _ReadInfoTrainer();

            if (FileManager.SaveTrainer(FileManager.ConvertTrainerObjectToStLine(newTrainer)))
            {
                Trainers[FitnessClubSystem.TrainerCount] = newTrainer;
                ++FitnessClubSystem.TrainerCount;
                Console.WriteLine("Success Saving Trainer To File..");
            }
            else
            {
                Console.WriteLine("Fail Saving Trainer To File!!!");

            }

        }

        public Trainer FindTrainer(int Id)
        {
            for (int i = 0; i < FitnessClubSystem.TrainerCount; i++)
            {
                if (Trainers[i].Id == Id)
                    return Trainers[i];
            }
            return null;
        }


        public bool IsTrainerExist(int Id)
        {
            Trainer trainer = FindTrainer(Id);
            return (trainer != null);
        }

        /*BOOKINGS*/
        public void ShowAllBookings()
        {
            for (int i = 0; i < FitnessClubSystem.BookingCount; i++)
            {
                Bookings[i].PrintInfo();
                Console.WriteLine(" ");

            }
            Console.WriteLine("\n                                     **************************************");
            Console.WriteLine("                                       TOTAL BOOKINGS IN SYSTEM: " + FitnessClubSystem.BookingCount);

        }

        Booking _ReadInfoBooking()
        {
            string status = "";
            DateTime date = DateTime.Now;
            int newId = FitnessClubSystem.BookingCount + 1;
            int memberId, activityId = 0;

            Console.WriteLine("Enter Member ID: ");
            memberId = _ReadIdMember();

            if (memberId == -1)//this check is not necessary beacause _ReadIdMember with this logic will return an right number but this check if in the futhure change logic in method _ReadIdMember.
            {
                Console.WriteLine("INVALID INPUT..");
                return null;
            }

            while (!IsMemberExist(memberId))
            {
                Console.WriteLine($"There is no member with this Id {memberId} in membership, please try again.");
                Console.WriteLine("Enter ID Member:");
                memberId = _ReadIdMember();
            }


            Console.WriteLine("Enter Activity ID: ");
            activityId = _ReadIdActivity();
            if (activityId == -1)//this check is not necessary beacause _ReadIdActivity with this logic will return an right number but this check if in the futhure change logic in method _ReadIdActivity.
            {
                Console.WriteLine("INVALID INPUT..");
                return null;
            }

            while (!IsActivityExist(activityId))
            {
                Console.WriteLine($"There is no activity with this Id {activityId}, please try again.");
                Console.WriteLine("Enter ID Activity:");
                activityId = _ReadIdActivity();
            }

            Console.WriteLine("Enter Date (dd/MM/yyyy):");
            string inputDate = Console.ReadLine();
            while (!DateTime.TryParse(inputDate, out date))
            {
                Console.WriteLine("Invalid date! Please enter a valid date:");
                inputDate = Console.ReadLine();
            }

            Console.WriteLine("Enter Status:");
            status = Console.ReadLine();
            while (status != "Confirmed" && status != "Cancelled" && status != "InProgress")
            {
                Console.WriteLine("Invalid Input, Please Enter a Right Input!!!");
                Console.WriteLine("Enter Status:");
                status = Console.ReadLine();
            }

            return new Booking(newId, memberId, activityId, date, status);
        }
        public void AddBooking()
        {
            if (FitnessClubSystem.BookingCount >= FitnessClubSystem.MaxBookings)
            {
                Console.WriteLine("YOU CAN'T ADDING A NEW BOOKING TO FILE, FILE IS FULLING");
                return;
            }

            Booking newBooking = _ReadInfoBooking();
            if (newBooking == null)
            {
                Console.WriteLine("INVALID BOOKING");
                return;
            }

            if (FileManager.SaveBooking(FileManager.ConvertBookingObjectToStLine(newBooking)))
            {
                Bookings[FitnessClubSystem.BookingCount] = newBooking;
                ++FitnessClubSystem.BookingCount;
                Console.WriteLine("Success Saving Booking To File..");
            }
            else
            {
                Console.WriteLine("Fail Saving Booking To File!!!");

            }





        }


    }
}

using FitnessClubManagementSystem.Core;
using FitnessClubManagementSystem.Core.Activities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.ManagementSystem
{
    public static class FileManager
    {
        const string MembersFile = @"Data\MEMBERS.txt";
        const string ActivitiesFile = @"Data\ACTIVITIES.txt";
        const string TrainersFile = @"Data\TRAINERS.txt";
        const string BookingsFile = @"Data\BOOKINGS.txt";

        /* Member */
        static Member _ConvertStLineToMemberObject(string stLine, char seperatore = ';')
        {
            string[] lineInMembersFile = stLine.Split(seperatore);
            if (lineInMembersFile[4] == "STANDARD")
            {
                return new StandardMember(Convert.ToInt32(lineInMembersFile[0]), lineInMembersFile[1], lineInMembersFile[2], Convert.ToDateTime(lineInMembersFile[3]), Convert.ToInt32(lineInMembersFile[5]));

            }
            else
            {
                return new PremiumMember(Convert.ToInt32(lineInMembersFile[0]), lineInMembersFile[1], lineInMembersFile[2], Convert.ToDateTime(lineInMembersFile[3]), Convert.ToBoolean(lineInMembersFile[5]), Convert.ToInt32(lineInMembersFile[6]));
            }

        }

        public static string ConvertMemberObjectToStLine(Member member, string seperator = ";")
        {
            string textObjectMember = " ";
            textObjectMember = member.Id.ToString() + seperator;
            textObjectMember += member.Name + seperator;
            textObjectMember += member.Email + seperator;
            textObjectMember += member.JoinDate.ToShortDateString() + seperator;

            string type = "";
            if (member is StandardMember)
            {
                StandardMember standardMember = (StandardMember)member;
                type = "STANDARD";
                textObjectMember += type + seperator;
                textObjectMember += standardMember.FreezeDaysUsed.ToString() + seperator + "0";
            }
            else if (member is PremiumMember)
            {
                PremiumMember premiumMember = (PremiumMember)member;
                type = "PREMIUM";
                string textHasPersonalTrainer = premiumMember.HasPersonalTrainer ? "True" : "False";
                textObjectMember += type + seperator;
                textObjectMember += textHasPersonalTrainer + seperator;
                textObjectMember += premiumMember.GuestPasses.ToString();
            }
            return textObjectMember;
        }

        //Load Data From File To Array Members 
        public static Member[] LoadMembers()
        {
            StreamReader fr = null;
            Member[] localMembers = new Member[FitnessClubSystem.MaxMembers];
            try
            {
                fr = new StreamReader(MembersFile);

                string line = fr.ReadLine();
                FitnessClubSystem.MemberCount = 0;

                while (line != null)
                {
                    if (FitnessClubSystem.MemberCount >= FitnessClubSystem.MaxMembers)
                    {
                        break;
                    }

                    localMembers[FitnessClubSystem.MemberCount] = _ConvertStLineToMemberObject(line);
                    FitnessClubSystem.MemberCount++;

                    line = fr.ReadLine();
                }

                fr.Close();
            }
            catch (NullReferenceException e)
            {
                string s = "";
                s = s + e;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return localMembers;
        }

        public static bool SaveMember(string memberRecord)//Add member record to file.
        {
            StreamWriter fw = null;

            try
            {
                // Console.WriteLine("Save path: " + Path.GetFullPath(FileNameMembers));

                fw = new StreamWriter(MembersFile, true);

                fw.WriteLine(memberRecord);

                fw.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /* Activity */
        static Activity _ConvertStLineToActivityObject(string stLine, char seperatore = ';')
        {
            string[] lineInActivityFile = stLine.Split(seperatore);
            switch (lineInActivityFile[4])
            {
                case "GROUP":
                    return new GroupClass(Convert.ToInt32(lineInActivityFile[0]), lineInActivityFile[1], Convert.ToInt32(lineInActivityFile[2]), Convert.ToInt32(lineInActivityFile[3]), lineInActivityFile[5], lineInActivityFile[6]);

                case "PT":
                    return new PersonalTrainingSession(Convert.ToInt32(lineInActivityFile[0]), lineInActivityFile[1], Convert.ToInt32(lineInActivityFile[2]), Convert.ToInt32(lineInActivityFile[3]), Convert.ToInt32(lineInActivityFile[5]), Convert.ToDouble(lineInActivityFile[6]));
                default:
                    return null;
            }
        }

        //Load Data From File To Array Activities
        public static Activity[] LoadActivities()
        {
            StreamReader fr = null;
            Activity[] localActivities = new Activity[FitnessClubSystem.MaxActivity];
            try
            {
                fr = new StreamReader(ActivitiesFile);

                string line = fr.ReadLine();
                FitnessClubSystem.ActivityCount = 0;

                while (line != null)
                {
                    if (FitnessClubSystem.ActivityCount >= FitnessClubSystem.MaxActivity)
                    {
                        break;
                    }
                    Activity activity = _ConvertStLineToActivityObject(line);
                    if (activity == null)
                    {   //still localActivities is empty there is no any activity inside it.
                        return localActivities;
                    }
                    localActivities[FitnessClubSystem.ActivityCount] = activity;
                    FitnessClubSystem.ActivityCount++;

                    line = fr.ReadLine();
                }

                fr.Close();
            }
            catch (NullReferenceException e)
            {
                string s = "";
                s = s + e;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return localActivities;
        }

        public static string ConvertActivityObjectToStLine(Activity activity, string seperator = ";")
        {
            string activityLine = "";
            activityLine = activity.Id.ToString() + seperator;
            activityLine += activity.Name + seperator;
            activityLine += activity.DurationMinutes.ToString() + seperator;
            activityLine += activity.Capacity.ToString() + seperator;


            if (activity is GroupClass)
            {
                GroupClass groupClass = (GroupClass)activity;
                activityLine += "GROUP" + seperator;
                activityLine += groupClass.InstructorName + seperator;
                activityLine += groupClass.DifficultyLevel == "beg" ? "Beginner" : "Advanced";

                return activityLine;
            }
            else if (activity is PersonalTrainingSession)
            {
                PersonalTrainingSession personalTrainingSession = (PersonalTrainingSession)activity;
                activityLine += "PT" + seperator;
                activityLine += personalTrainingSession.TrainerId.ToString() + seperator;
                activityLine += personalTrainingSession.HourlyRate.ToString();

                return activityLine;
            }
            return " ";

        }

        public static bool SaveActivity(string activityRecord)//Add activity record to file.
        {
            StreamWriter fw = null;

            try
            {
                // Console.WriteLine("Save path: " + Path.GetFullPath(FileNameMembers));

                fw = new StreamWriter(ActivitiesFile, true);

                fw.WriteLine(activityRecord);

                fw.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /* Trainers */
        static Trainer _ConvertStLineToTrainerObject(string stLine, char seperatore = ';')
        {
            string[] lineInTrainersFile = stLine.Split(seperatore);
            return new Trainer(Convert.ToInt32(lineInTrainersFile[0]), lineInTrainersFile[1], lineInTrainersFile[2], Convert.ToDouble(lineInTrainersFile[3]));
        }
        //Load Data From File To Array Trainers
        public static Trainer[] LoadTrainers()
        {
            StreamReader fr = null;

            Trainer[] localTrainers = new Trainer[FitnessClubSystem.MaxTrainers];

            try
            {
                fr = new StreamReader(TrainersFile);

                string line = fr.ReadLine();

                FitnessClubSystem.TrainerCount = 0;

                while (line != null)
                {
                    if (FitnessClubSystem.TrainerCount >= FitnessClubSystem.MaxTrainers)
                    {
                        break;
                    }

                    Trainer trainer = _ConvertStLineToTrainerObject(line);

                    localTrainers[FitnessClubSystem.TrainerCount] = trainer;

                    FitnessClubSystem.TrainerCount++;

                    line = fr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (fr != null)
                {
                    fr.Close();
                }
            }

            return localTrainers;
        }

        public static string ConvertTrainerObjectToStLine(Trainer trainer, char seperatore = ';')
        {
            string trainerLine = "";
            trainerLine = trainer.Id.ToString() + seperatore;
            trainerLine += trainer.Name + seperatore;
            trainerLine += trainer.Specialty + seperatore;
            trainerLine += trainer.Salary.ToString();
            return trainerLine;
        }
        public static bool SaveTrainer(string TrainerRecord)//Add new trainer to file.
        {
            StreamWriter fw = null;

            try
            {
                fw = new StreamWriter(TrainersFile, true);

                fw.WriteLine(TrainerRecord);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                if (fw != null)
                {
                    fw.Close();
                }
            }
        }

        /*BOOKING*/

        static Booking _ConvertStLineToBookingObject(string stLine, char seperatore = ';')
        {
            string[] stObject = stLine.Split(';');
            return new Booking(Convert.ToInt32(stObject[0]), Convert.ToInt32(stObject[1]), Convert.ToInt32(stObject[2]), Convert.ToDateTime(stObject[3]), stObject[4]);
        }
        public static string ConvertBookingObjectToStLine(Booking booking, char seperatore = ';')
        {
            string StLBooking = "";
            StLBooking = booking.Id.ToString() + seperatore;
            StLBooking += booking.MemberId.ToString() + seperatore;
            StLBooking += booking.ActivityId.ToString() + seperatore;
            StLBooking += booking.Date.ToShortDateString() + seperatore;
            StLBooking += booking.Status;

            return StLBooking;
        }
        //Load Data From File To Array Bookings
        public static Booking[] LoadBookings()
        {
            StreamReader fr = null;

            Booking[] localBookings = new Booking[FitnessClubSystem.MaxBookings];

            try
            {
                fr = new StreamReader(BookingsFile);

                string line = fr.ReadLine();

                FitnessClubSystem.BookingCount = 0;

                while (line != null)
                {
                    if (FitnessClubSystem.BookingCount >= FitnessClubSystem.MaxBookings)
                    {
                        break;
                    }

                    Booking booking = _ConvertStLineToBookingObject(line);

                    localBookings[FitnessClubSystem.BookingCount] = booking;

                    FitnessClubSystem.BookingCount++;

                    line = fr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (fr != null)
                {
                    fr.Close();
                }
            }

            return localBookings;
        }
        public static bool SaveBooking(string BookingRecord)//Add new Booking to file.
        {
            StreamWriter fw = null;

            try
            {
                fw = new StreamWriter(BookingsFile, true);

                fw.WriteLine(BookingRecord);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                if (fw != null)
                {
                    fw.Close();
                }
            }
        }

    }
}

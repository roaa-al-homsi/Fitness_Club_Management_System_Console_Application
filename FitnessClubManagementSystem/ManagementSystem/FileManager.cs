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
        public const string FileNameMembers = @"Data\MEMBERS.txt";
        public const string FileNameActivities = @"Data\ACTIVITIES.txt";
        
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
            textObjectMember  = member.Id.ToString()+ seperator;
            textObjectMember += member.Name + seperator;
            textObjectMember += member.Email + seperator;
            textObjectMember += member.JoinDate.ToShortDateString() + seperator;

            string type = "";
            if (member is StandardMember)
            {
                StandardMember standardMember = (StandardMember)member;
                type = "STANDARD";
                textObjectMember += type + seperator;
                textObjectMember += standardMember.FreezeDaysUsed.ToString()+seperator+"0";
            }
            else if (member is PremiumMember)
            {
                PremiumMember premiumMember = (PremiumMember)member;
                type = "PREMIUM";
                string textHasPersonalTrainer = premiumMember.HasPersonalTraining ? "True" : "False";
                textObjectMember += type + seperator;
                textObjectMember += textHasPersonalTrainer + seperator;
                textObjectMember += premiumMember.GuestPasses.ToString();
            }
          return textObjectMember;
        }
        
        public static Member[] LoadDataFromFileToArrayMembers()
        {
            StreamReader fr = null;
            Member[] localMembers = new Member[FitnessClubSystem.MaxMembers] ; 
            try
            {
                fr = new StreamReader(FileNameMembers);

                string line = fr.ReadLine();
                FitnessClubSystem.NumberMembersInArray = 0;

                while (line != null)
                {
                    if (FitnessClubSystem.NumberMembersInArray >= FitnessClubSystem.MaxMembers)
                    {
                        break;
                    }

                    localMembers[FitnessClubSystem.NumberMembersInArray] = _ConvertStLineToMemberObject(line);
                    FitnessClubSystem.NumberMembersInArray++;

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

                fw = new StreamWriter(FileNameMembers, true);

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

        public static Activity[] LoadDataFromFileToArrayActivities()
        {
            StreamReader fr = null;
            Activity[] localActivities = new Activity[FitnessClubSystem.MaxActivity];
            try
            {
                fr = new StreamReader(FileNameActivities);

                string line = fr.ReadLine();
                FitnessClubSystem.NumberActivityInArray = 0;

                while (line != null)
                {
                    if (FitnessClubSystem.NumberActivityInArray >= FitnessClubSystem.MaxActivity)
                    {
                        break;
                    }

                    localActivities[FitnessClubSystem.NumberActivityInArray] = _ConvertStLineToActivityObject(line);
                    FitnessClubSystem.NumberActivityInArray++;

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


    }
}

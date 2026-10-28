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
        public static int NumberMembersInArray = 0;
        public static Member[] Members = new Member[MaxMembers];//to storing all members that exist in file. 

      


        public static void LoadingDataApplication()
        {
            Members= FileManager.LoadDataFromFileToArrayMembers();
            //in method LoadDataFromFileToArrayMembers will update to NumberMembersInArray.

        }
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

            //Console.WriteLine("Enter join date for member : ");
            //joinDate = Convert.ToDateTime( Console.ReadLine());

            Console.WriteLine("Enter join date for member (dd/MM/yyyy): ");
            string inputDate = Console.ReadLine();

            while (!DateTime.TryParse(inputDate, out joinDate))
            {
                Console.WriteLine("Invalid date! Please enter a valid date:");
                inputDate = Console.ReadLine();
            }

            if (type== "STANDARD")
            {
                Console.WriteLine("Enter number freeze days used : ");
                freezeDaysUsed = int.Parse(Console.ReadLine());
              
                StandardMember standardMember = new StandardMember(newId,name, email,joinDate,freezeDaysUsed);
                return standardMember;

            }
            else if (type== "PREMIUM")
            {
                Console.WriteLine("Has personal training? [Yes Or No]");
                string textHasPersonalTraining = Console.ReadLine();

                hasPersonalTrainer = textHasPersonalTraining == "Yes" || textHasPersonalTraining == "yes" ? true : false;

                Console.WriteLine("Enter number guest passes: ");
                guestPasses = int.Parse(Console.ReadLine());


                PremiumMember premiumMember = new PremiumMember(newId, name, email, joinDate,hasPersonalTrainer,guestPasses);
               
                return premiumMember;
            }
            return null;
        }
        
        public static void  AddMember()
        {
            if(NumberMembersInArray>=MaxMembers)
            {
                Console.WriteLine("YOU CAN'T ADDING A NEW MEMBER TO FILE, FILE IS FULLING");
                return; 
            }


           Member NewMember = _ReadInfoNewMember();
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
        




    }
}

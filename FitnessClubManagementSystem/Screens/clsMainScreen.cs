using FitnessClubManagementSystem.Core;
using FitnessClubManagementSystem.Lib;
using FitnessClubManagementSystem.ManagementSystem;
using FitnessClubManagementSystem.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClubManagementSystem.Screens
{
    public  class clsMainScreen
    {
        enum enMainMenuOptions
        {
            AllMembers=1, AllActivity=2, AllTrainers=3,
            AllBookings=4, FindMember=5, FindActivity=6, 
            AddMember=7, AddActivity=8,AddTrainer=9,
            AddBooking=10,ShowProjectedMonthlyRevenue=11,
            Exit=12
        }
        
        static short _ReadMainMenuOption()
        {
            short Choice = 0;
            Console.WriteLine($"{" ",-37}Please Chose What Do You Want To Do [1 To 12]?");

            Choice = ((short)clsInputValidate.ReadNumberBetween(1, 12));
            return Choice;

        }
        static void _GoBackToMainMenu()
        {
            Console.WriteLine("\nPress any key to go main menu....");
            Console.ReadKey();
             ShowMainMenu();
        }
        static void _DrawHeaderScreen(string Title, string SubTitle = " ")
        {
            Console.WriteLine("                                     **************************************");

            Console.WriteLine("                                        " + Title);

            if (SubTitle != " ")
            {
                Console.WriteLine("                                     **************************************");
                Console.WriteLine("                                           " + SubTitle);
            }

            Console.WriteLine("                                     **************************************");
        }


       public static void ShowMainMenu()
        {
           FitnessClubSystem.LoadingDataApplication();


            Console.Clear();
            _DrawHeaderScreen("Fitness Club Management System");

            Console.WriteLine("                                     **************************************");
            Console.WriteLine("                                                 Main Menu ");
            Console.WriteLine("                                     **************************************");

            Console.WriteLine("                     [1]:  Show all members.");
            Console.WriteLine("                     [2]:  Show all activity.");
            Console.WriteLine("                     [3]:  Show all trainers.");
            Console.WriteLine("                     [4]:  Show all bookings.");
            Console.WriteLine("                     [5]:  Search for a member by ID.");
            Console.WriteLine("                     [6]:  Search for a activity by ID.");
            Console.WriteLine("                     [7]:  Add a new member.");
            Console.WriteLine("                     [8]:  Add a new an activity.");
            Console.WriteLine("                     [9]:  Add a new an trainer.");
            Console.WriteLine("                     [10]: Add a new an booking.");
            Console.WriteLine("                     [11]: Show projected monthly revenue.");
            Console.WriteLine("                     [12]: Exit");
            Console.WriteLine("                                     **************************************");

            _PerformMainMenuOptions(_ReadMainMenuOption());


        }
        static void _ShowAllMembers()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "VIEW ALL MEMBER SCREEN");
            FitnessClubSystem.ShowAllMembers(); 

        }
        static void _ShowAllActivity()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "VIEW ALL ACTIVITIES SCREEN");
            FitnessClubSystem.ShowAllActivity();
        }

        static void _ShowAllTrainers()
        {

        }
        static void _ShowAllBookings()
        {

        }
        static void _FindMemberById()
        {
            _DrawHeaderScreen("Fitness Club Management System", "FIND A MEMBER Screen");
            FitnessClubSystem.FindMemberById();
        }
        static void _FindActivity()
        {

        }
        static void _AddMember()
        {
            _DrawHeaderScreen("Fitness Club Management System", "ADDING NEW MEMBER SCREEN");
            FitnessClubSystem.AddMember();
        }
        static void _AddActivity()
        {
            _DrawHeaderScreen("Fitness Club Management System", "ADDING NEW ACTIVITY SCREEN");
            FitnessClubSystem.AddActivity();
        }
        static void _AddTrainer()
        {

        }
        static void _AddBooking()
        {

        }
        static void _ShowProjectedMonthlyRevenue()
        {

        }
        static void _Exit()
        {

        }

        static void _PerformMainMenuOptions( short Choice)
        {
            switch (Choice)
            {
                case 1:
                    Console.Clear();
                    _ShowAllMembers();
                    _GoBackToMainMenu();
                    break;

                case 2:
                    Console.Clear();
                    _ShowAllActivity();
                    _GoBackToMainMenu();
                    break;
                case 3:
                     Console.Clear();
                    _ShowAllTrainers();
                    _GoBackToMainMenu();
                    break;
                case 4:
                     Console.Clear();
                    _ShowAllBookings();
                    _GoBackToMainMenu();
                    break;
                case 5:
                     Console.Clear();
                    _FindMemberById();
                    _GoBackToMainMenu();
                    break;
                case 6:
                     Console.Clear();
                    _FindActivity();
                    _GoBackToMainMenu();
                    break;
                case 7:
                     Console.Clear();
                    _AddMember();
                    _GoBackToMainMenu();
                    break;
                case 8:
                     Console.Clear();
                    _AddActivity();
                    _GoBackToMainMenu();
                    break;
                case 9:
                     Console.Clear();
                    _AddTrainer();
                    _GoBackToMainMenu();
                    break;
                case 10:
                     Console.Clear();
                    _AddBooking();
                    _GoBackToMainMenu();
                    break;
                case 11:
                     Console.Clear();
                    _ShowProjectedMonthlyRevenue();
                    _GoBackToMainMenu();
                    break;
                case 12:
                    _Exit();
                    _GoBackToMainMenu();
                    break;



                default:
                    break;
            }



        }




    }
}

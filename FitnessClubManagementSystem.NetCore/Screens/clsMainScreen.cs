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
    // Handles the console user interface and main menu navigation.
    public class clsMainScreen
    {
        //create an instance from FitnessClubSystem class to calling all methods that do all operations 
        FitnessClubSystem system = new FitnessClubSystem();

        // Reads and validates the user's main menu selection.
        short _ReadMainMenuOption()
        {
            short Choice = 0;
            Console.WriteLine($"{" ",-37}Please Chose What Do You Want To Do [1 To 12]?");

            Choice = ((short)clsInputValidate.ReadNumberBetween(1, 12));
            return Choice;

        }

        // Displays a formatted screen header with an optional subtitle.
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

        public void ShowMainMenu()
        {
            // Load data only once when the program starts.
            system.LoadingDataApplication();

            short choice = 0;

            do
            {
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

                choice = _ReadMainMenuOption();

                _PerformMainMenuOptions(choice);

                if (choice != 12)
                {
                    Console.WriteLine("\nPress any key to go to main menu....");
                    Console.ReadKey();
                }

            } while (choice != 12);
        }

        // Displays all members by calling the corresponding system operation.
        void _ShowAllMembers()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "VIEW ALL MEMBER SCREEN");
            system.ShowAllMembers();

        }

        // Displays all activities.
        void _ShowAllActivities()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "VIEW ALL ACTIVITIES SCREEN");
            system.ShowAllActivity();
        }

        // Displays all trainers.
        void _ShowAllTrainers()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "VIEW ALL TRAINERS SCREEN");
            system.ShowAllTrainers();
        }

        // Displays all bookings.
        void _ShowAllBookings()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "VIEW ALL BOOKINGS SCREEN");
            system.ShowAllBookings();
        }

        // Searches for a member by ID.
        void _FindMemberById()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "FIND A MEMBER SCREEN");
            system.FindMemberById();
        }

        // Searches for an activity by ID.
        void _FindActivityById()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "FIND A ACTIVITY SCREEN");
            system.FindActivityById();
        }

        // Opens the screen for adding a new member.
        void _AddMember()
        {
            _DrawHeaderScreen("Fitness Club Management System", "ADDING NEW MEMBER SCREEN");
            system.AddMember();
        }

        // Opens the screen for adding a new activity.
        void _AddActivity()
        {
            _DrawHeaderScreen("Fitness Club Management System", "ADDING NEW ACTIVITY SCREEN");
            system.AddActivity();
        }

        // Opens the screen for adding a new trainer.
        void _AddTrainer()
        {
            _DrawHeaderScreen("Fitness Club Management System", "ADDING NEW TRAINER SCREEN");
            system.AddTrainer();
        }

        // Opens the screen for adding a new booking.
        void _AddBooking()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "ADD NEW BOOKING SCREEN");
            system.AddBooking();

        }

        // Displays the projected monthly revenue calculated by the system.
        void _GetProjectedMonthlyRevenue()
        {
            _DrawHeaderScreen("FITNESS CLUB MANAGEMENT SYSTEM", "SHOW PROJECTED MONTHLY REVENUE SCREEN");
            Console.WriteLine($"Projected Monthly Revenue : {system.GetProjectedMonthlyRevenue()}");

        }

        static void _Exit()
        {
            Console.WriteLine("Exiting the program...");
            Environment.Exit(0);
        }

        // Executes the operation selected by the user from the main menu.
        void _PerformMainMenuOptions(short Choice)
        {
            switch (Choice)
            {
                case 1:
                    Console.Clear();
                    _ShowAllMembers();
                    break;

                case 2:
                    Console.Clear();
                    _ShowAllActivities();
                    break;
                case 3:
                    Console.Clear();
                    _ShowAllTrainers();
                    break;
                case 4:
                    Console.Clear();
                    _ShowAllBookings();
                    break;
                case 5:
                    Console.Clear();
                    _FindMemberById();
                    break;
                case 6:
                    Console.Clear();
                    _FindActivityById();
                    break;
                case 7:
                    Console.Clear();
                    _AddMember();
                    break;
                case 8:
                    Console.Clear();
                    _AddActivity();
                    break;
                case 9:
                    Console.Clear();
                    _AddTrainer();
                    break;
                case 10:
                    Console.Clear();
                    _AddBooking();
                    break;
                case 11:
                    Console.Clear();
                    _GetProjectedMonthlyRevenue();
                    break;
                case 12:
                    _Exit();
                    break;

                default:
                    break;
            }
        }

    }
}
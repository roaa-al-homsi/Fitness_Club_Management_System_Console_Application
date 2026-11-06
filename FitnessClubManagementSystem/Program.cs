using FitnessClubManagementSystem.ManagementSystem;
using FitnessClubManagementSystem.Screens;
using System;
namespace FitnessClubManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;

            // Display a menu repeatedly until the user enters a valid choice.
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("I was confused about where the files should be created, read, and modified because the assignment instructions did not specify the expected location.");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("1. Inside the project folder (where the .csproj file is located).");
                Console.WriteLine("2. Inside the Debug folder (where the application is executed).");

                Console.WriteLine();

                // Ask the user to choose where the data files should be stored.
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Please choose your preferred location (1 or 2): ");
                Console.ResetColor();

                // Read the user's input and check whether it is a valid integer.
                bool isValid = int.TryParse(Console.ReadLine(), out choice);

                // Validate the input to allow only choices 1 and 2.
                if (!isValid || (choice != 1 && choice != 2))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    Console.ResetColor();

                    Console.WriteLine();
                    choice = 0;
                }

            } while (choice != 1 && choice != 2);

            // Set the root path according to the user's selected data file location.
            // An empty path uses the project folder as the base directory.
            // "../../" moves two levels up from the Debug folder to the project folder.
            FileManager.rootPath = choice == 1 ? "" : @"../../";

            // Create the main screen object and display the application's main menu.
            clsMainScreen mainScreen = new clsMainScreen();
            mainScreen.ShowMainMenu();
        }
    }
}

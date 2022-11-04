using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{
    internal class Interface
    {
        void UserDisplay(User user)
        {
            Console.Clear();
            Console.WriteLine("===== Profile Displayed =====");
            user.display();
        }
        void DisplayDiariesInterface(User user)
        {
            Console.Clear();
            if (user.userDiaries is not null)
            {
                if (user.userDiaries.diaryCount() != 0)
                {
                    Console.WriteLine("==== Diaries Displayed ====\n");
                    user.DisplayDiaries();

                }
                else if (user.userDiaries.diaryCount() == 0)
                {
                    Console.WriteLine("\nNo Diaries available\n");
                }
            }

            else
            {
                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");


            }
        }
        bool DeleteDiaryInterface(User user)
        {
            Console.Clear();
            if (user.userDiaries is not null)
            {
                if (user.userDiaries.diaryCount() != 0)
                {
                    string idInput;
                    do
                    {
                        Console.WriteLine("==== To Delete a Diary ====");
                        Console.WriteLine("\nTo exit at any point Enter 0\n");
                        Console.WriteLine("Which User Do you want to delete?");
                        user.DisplayDiaries();
                        Console.WriteLine("Enter Diary Id:");
                        idInput = Console.ReadLine();
                        if (!Utility.isNumeric(idInput))
                        {
                            Console.WriteLine("Incorrect or Null input!\nPress to try it again\n");
                            Console.ReadKey();
                            Console.Clear();
                        }

                    }
                    while (!Utility.isNumeric(idInput));

                    if (Utility.isNumeric(idInput))
                    {
                        int id = Convert.ToInt32(idInput);

                        if (id == 0) { Console.Clear(); return false; }

                        user.DeleteDiary(id);
                    }
                }
                else if (user.userDiaries.diaryCount() == 0)
                {
                    Console.WriteLine("No Diaries available");

                }
            }
            else
            {
                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");


            }
            return true;
        }

        bool UpdateDiaryInterface(User user)
        {
            Console.Clear();
            if (user.userDiaries is not null)
            {
                if (user.userDiaries.diaryCount() != 0)
                {

                    string idInput;
                    do
                    {
                        Console.WriteLine("==== To Update a Diary ====");
                        Console.WriteLine("\nTo exit at any point Enter 0\n");
                        Console.WriteLine("Which User Do you want to update?");
                        user.DisplayDiaries();
                        Console.WriteLine("Enter Diary Id:");
                        idInput = Console.ReadLine();
                        if (!Utility.isNumeric(idInput))
                        {
                            Console.WriteLine("Incorrect or Null input!\nPress to try it again\n");
                            Console.ReadKey();
                            Console.Clear();
                        }

                    }
                    while (!Utility.isNumeric(idInput));

                    if (Utility.isNumeric(idInput))
                    {
                        int id = Convert.ToInt32(idInput);

                        if (id == 0) { Console.Clear(); return false; }
                        Console.Clear();
                        if (user.FindDiary(id))
                        {
                            //getInput("Enter Diary Name:");
                            Console.WriteLine("Enter Name of the Diary:");
                            string name = Console.ReadLine();
                            if (name == "0") { Console.Clear(); return false; }
                            Console.WriteLine("Enter Content: ");
                            string content = Console.ReadLine();
                            if (content == "0") { Console.Clear(); return false; }

                            user.UpdateDiary(id, name, content);

                        }
                        else { Console.Clear(); Console.WriteLine("\nNo Diary of this ID\n"); }

                    }
                }
                else if (user.userDiaries.diaryCount() == 0)
                {
                    Console.WriteLine("\nNo Diaries available\n");

                }
            }
            else
            {
                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");


            }
            Console.WriteLine("Press to Continue!");
            Console.ReadKey();
            return true;
        }

        bool CreateDiaryInterface(User user)
        {
            Console.Clear();
            if (user.userDiaries is not null)
            {
                Console.WriteLine("==== To Create a Diary ====");
                Console.WriteLine("\nTo exit at any point Enter exit\n");


                string name = Utility.getInput("Enter Diary Name:");
                if (name == "exit") { Console.Clear(); return false; }
                Console.WriteLine("Enter Content: ");
                string content = Console.ReadLine();
                if (content == "exit") { Console.Clear(); return false; }
                user.CreateDiary(name, content);
                Console.Clear();
                Console.WriteLine("\nDiary Created!\n");
            }
            else
            {
                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");

            }
            return true;
        }

        bool UpdateProfileInterface(User user)
        {
            string input;
            Console.Clear();
            Console.WriteLine("==== To Update your Profile ====\n");
            user.display();
            Console.WriteLine("Enter Name if you want to change:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Password if you want to change:");
            string passcode = Console.ReadLine();
            do
            {
                Console.WriteLine("\nEnter Phone Number (03xx-xxxxxxx) if you want to change:");
                input = Console.ReadLine();
                Console.Clear();
                if (input == "exit") { Console.Clear(); break; }
                if (!Utility.ValidatePhone(input) && !string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("\nIncorrect Phone Format\n");
                }
            }
            while (!Utility.ValidatePhone(input) && !string.IsNullOrEmpty(input));
            if (input == "exit") { Console.Clear(); return false; }
            string phone = input;

            do
            {
                Console.WriteLine("Enter Email if you want to change:");
                input = Console.ReadLine();
                Console.Clear();
                if (!Utility.ValidateEmail(input))
                {
                    Console.WriteLine("\nIncorrect Email Format\n");
                }
                if (input == "exit") { Console.Clear(); break; }
            }
            while (!Utility.ValidateEmail(input) && !string.IsNullOrEmpty(input));

            if (input == "exit") { Console.Clear(); return false; }
            string email = input;

            user.UpdateUser(name, passcode, phone, email);
            Console.WriteLine("Press to Continue!");
            Console.ReadKey();
            return true;
        }

    }
}

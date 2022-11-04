using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

/// <summary>
/// Summary description for View
/// Console Interface of the Diaries
/// </summary>
namespace UserDiaryConsole
{
    public class Main
    {
        Cache cache;
        dynamic user;

        public Main()
        {
            cache = Cache.getCache();

            MainInterface();
        }
        enum MainOptions
        {
            Login,
            Register,
            Exit
        }
        void MainInterface()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("===========Welcome to the Diaries===========\n");

                Console.WriteLine("\nDo you want to Login or Register an Account?\n" +
                "- To Login Press 0\n" + "- To Register Press 1\n" + "- To Exit Press 2\n");
                var input = Console.ReadLine();
                int opt;
                while (!Utility.isNumeric(input))
                {
                    Console.WriteLine("Incorrect or Null input!\nPress to try it again\n");
                    Console.ReadKey();
                    break;
                };
                if (Utility.isNumeric(input))
                {
                    opt = Convert.ToInt32(input);
                    if (opt == (int)MainOptions.Login)
                    {
                        Console.Clear();
                        LoginInterface();
                    }
                    else if (opt == (int)MainOptions.Register)
                    {
                        Console.Clear();
                        RegisterInterface();
                    }
                    else if (opt == (int)MainOptions.Exit)
                    {
                        Environment.Exit(0);
                        break;
                    }
                    else { Console.WriteLine("Wrong Input!\nPrss to Try Again"); Console.ReadKey(); }
                }
            } while (user is null);

        }
        void RegisterInterface()
        {
            Console.WriteLine("=========== Register ===========\n");
            bool temp = true;
            Console.WriteLine("\nTo exit at any point Enter exit");
            do
            {
                Console.WriteLine("Enter Name:");
                string name = Utility.getInput();
                if (name == "exit") { Console.Clear(); break; }

                Console.WriteLine("Enter UserName:");
                string input = Utility.getInput();
                while (!Utility.ValidateUsername(input))
                {
                    Console.Clear();
                    Console.WriteLine("\nEnter Username:");
                    input = Utility.getInput();
                };
                if (input == "exit") { Console.Clear(); break; }
                string username = input;

                Console.WriteLine("Enter Password: ");
                string passcode = Utility.getInput();
                if (passcode == "exit") { Console.Clear(); break; }

                Console.Clear();
                do
                {
                    Console.WriteLine("\nIf you want don't want to insert now\nPress Enter\n" +
                        "\nEnter Phone Number (03xx-xxxxxxx): ");
                    input = Console.ReadLine();
                    Console.Clear();
                    if (input == "exit") { Console.Clear(); break; }
                    if (!Utility.ValidatePhone(input))
                    {
                        Console.WriteLine("\nIncorrect Phone Format\n");
                    }
                }
                while (!Utility.ValidatePhone(input) && !string.IsNullOrEmpty(input));
                if (input == "exit") { Console.Clear(); break; }
                string phone = input;

                Console.Clear();
                do
                {
                    Console.WriteLine("\nIf you want don't want to insert now\nPress Enter\n");
                    Console.WriteLine("Enter Email Address: ");
                    input = Console.ReadLine();
                    Console.Clear();
                    if (!Utility.ValidateEmail(input))
                    {
                        Console.WriteLine("\nIncorrect Email Format\n");
                    }
                    if (input == "exit") { Console.Clear(); break; }
                }
                while (!Utility.ValidateEmail(input) && !string.IsNullOrEmpty(input));

                if (input == "exit") { Console.Clear(); break; }
                string email = input;

                
                User emp = new User(username, name, passcode, "user", "pending", phone, email);
                cache.UserList.addUser(emp);
                cache.UpdateUserList();
                Console.Clear();
                emp.display();

                Console.WriteLine($"\nAccount Created!\n");
                Console.WriteLine("Press to Continue!");
                Console.ReadKey();
                break;
            } while (temp);
        }
        void LoginInterface()
        {
            Console.WriteLine("=========== Login ===========\n");
            RunUserLogin(user);
        }

        private void RunUserLogin(User user)
        {
            Console.WriteLine("\nTo exit at any point Enter exit");
            do
            {
                string username = Utility.getInput("Enter your Username: ");
                if (username == "exit") { Console.Clear(); break; }

                string password = Utility.getInput("Enter your Password: ");
                if (password == "exit")
                {
                    Console.Clear();
                    break;
                }

                user = cache.UserLog(username, password);

            } while (user is null);
            if (user is not null && user.Type == Types.user.ToString())
            {
                RunUserFunctions(user);

            }
            else if (user is not null && user.Type == Types.admin.ToString())
            {
                RunAdminFunctions(user);
            }
        }

        enum UserOptions
        {
            Create,
            Update,
            //Find,
            Delete,
            Display_Diaries,
            Update_Profile,
            Display,
            Logout

        }
        void RunUserFunctions(User user)
        {
            while (user is not null)
            {
                Console.Clear();
                Console.WriteLine("========== Welcome ===========");
                Console.WriteLine("\nWhat do you want to do?");
                Console.WriteLine(
                    $"- To Create a Diary Press {((int)UserOptions.Create)}\n" +
                    $"- To Update a Diary Press {((int)UserOptions.Update)}\n" +
                    //$"- To Find a Diary Press {((int)UserOptions.Find)}\n" +
                    $"- To Delete a Diary Press {((int)UserOptions.Delete)}\n" +
                    $"- To Display Diaries Press {((int)UserOptions.Display_Diaries)}\n" +
                    $"- To Update your Profile Press {((int)UserOptions.Update_Profile)}\n" +
                    $"- To Display your Profile Press {((int)UserOptions.Display)}\n" +
                    $"- To Logout Press {((int)UserOptions.Logout)}\n");
                string input = Console.ReadLine();
                int option;
                while (!Utility.isNumeric(input))
                {
                    Console.WriteLine("Incorrect or Null input!\nPress to try it again\n");
                    Console.ReadKey();
                    break;
                };
                if (Utility.isNumeric(input))
                {
                    option = Convert.ToInt32(input);

                    switch (option)
                    {
                        case (int)UserOptions.Create:
                            {
                                CreateDiaryInterface(user);

                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();
                                break;
                            }
                        case (int)UserOptions.Update:
                            {
                                UpdateDiaryInterface(user);
                                break;
                            }
                        //case (int)UserOptions.Find:
                        //{
                        //    Console.Clear();
                        //    if (user.userDiaries is not null)
                        //    {
                        //        if (user.userDiaries.diaryCount() != 0)
                        //        {
                        //            Console.WriteLine("==== To Find a Diary ====");
                        //            //Console.WriteLine("\nTo find the diary by Id, Press 1\n");
                        //            //Console.WriteLine("\nTo find the diary by Name, Press 2\n");
                        //            ////Console.WriteLine("Which User Do you want to find?");
                        //            ////user.DisplayDiaries();
                        //            //if (Console.ReadKey() == '1') { }
                        //            Console.WriteLine("Enter Diary Id:");
                        //            int id = Utility.getIntInput();
                        //            //if (id == 0) { Console.Clear(); break; }
                        //            user.FindDiary(id);
                        //            Console.WriteLine("Press to Continue!");
                        //            Console.ReadKey();
                        //        }
                        //        else if (user.userDiaries.diaryCount() == 0)
                        //        {
                        //            Console.WriteLine("No Diaries available");
                        //            Console.WriteLine("Press to Continue!");
                        //            Console.ReadKey();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                        //        Console.WriteLine("Press to Continue!");
                        //        Console.ReadKey();

                        //    }
                        //    break;
                        //}
                        case (int)UserOptions.Delete:
                            {
                                DeleteDiaryInterface(user);
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();
                                break;
                            }
                        case (int)UserOptions.Display_Diaries:
                            {
                                DisplayDiariesInterface(user);
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();
                                break;
                            }
                        case (int)UserOptions.Update_Profile:
                            {
                                UpdateProfileInterface(user);
                                break;
                            }
                        case (int)UserOptions.Display:
                            UserDisplay(user);
                            Console.WriteLine("Press to Continue!");
                            Console.ReadKey();
                            break;
                        case (int)UserOptions.Logout:
                            Console.Clear();
                            Console.WriteLine("==== Do you really want to logout? ====\n" + "If Yes press y");
                            string response = Utility.getInput();
                            if (response == "y")
                            {
                                cache.Logout();
                                user = null;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
        }
        
        enum AdminOptions
        {

            CreateDiary,
            UpdateDiary,
            //Find,
            DeleteDiary,
            Display_myDiaries,
            Create,
            Delete,
            Find,
            Authorize,
            Unauthorize,
            Find_Diary,
            Display_Users,
            //Add_Admin,
            //Remove_Admin,
            Display_Admins,
            Display_Diaries,
            Update_Profile,
            Display,
            Logout

        }

        void RunAdminFunctions(User user)
        {
            while (user is not null)
            {
                Console.Clear();
                Console.WriteLine("========== Welcome ===========");
                Console.WriteLine("\nWhat do you want to do as Admin?");
                Console.WriteLine(
                    $"- To Create a Diary Press {((int)AdminOptions.CreateDiary)}\n" +
                    $"- To Update a Diary Press {((int)AdminOptions.UpdateDiary)}\n" +
                    //$"- To Find a Diary Press {((int)AdminOptions.FindDiary)}\n" +
                    $"- To Delete a Diary Press {((int)AdminOptions.DeleteDiary)}\n" +
                    $"- To Display Diaries Press {((int)AdminOptions.Display_myDiaries)}\n" +
                    $"- To Create a User Press {(int)AdminOptions.Create}\n" +
                    $"- To Delete a User Press {(int)AdminOptions.Delete}\n" +
                    $"- To Find a User Press {(int)AdminOptions.Find}\n" +
                    $"- To Authorize a User Press {(int)AdminOptions.Authorize}\n" +
                    $"- To UnAuthorize a User Press {(int)AdminOptions.Unauthorize}\n" +
                    $"- To Find User Diary Press {(int)AdminOptions.Find_Diary}\n" +
                    $"- To Display Users Press {(int)AdminOptions.Display_Users}\n" +
                    //$"- To Add an Admin Press {(int)AdminOptions.Add_Admin}\n" +
                    //$"- To Remove an Admin Press {(int)AdminOptions.Remove_Admin}\n" +
                    $"- To Display Admins Press {(int)AdminOptions.Display_Admins}\n" +
                    $"- To Display User Diaries Press {(int)AdminOptions.Display_Diaries}\n" +
                    $"- To Update your Profile Press {(int)AdminOptions.Update_Profile}\n" +
                    $"- To Display your Profile Press {(int)AdminOptions.Display}\n" +
                    $"- To Logout Press {(int)AdminOptions.Logout}\n");

                string input = Console.ReadLine();
                int option;
                while (!Utility.isNumeric(input))
                {
                    Console.WriteLine("Incorrect or Null input!\nPress to try it again\n");
                    Console.ReadKey();
                    break;
                };
                if (Utility.isNumeric(input))
                {
                    option = Convert.ToInt32(input);

                    switch (option)
                    {
                        case (int)AdminOptions.CreateDiary:
                            {
                                CreateDiaryInterface(user);
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();
                                break;
                            }
                        case (int)AdminOptions.UpdateDiary:
                            {
                                UpdateDiaryInterface(user);
                                break;
                            }
                        //case (int)AdminOptions.FindDiary:
                        //{
                        //    Console.Clear();
                        //    if (user.userDiaries is not null)
                        //    {
                        //        if (user.userDiaries.diaryCount() != 0)
                        //        {
                        //            Console.WriteLine("==== To Find a Diary ====");
                        //            //Console.WriteLine("\nTo find the diary by Id, Press 1\n");
                        //            //Console.WriteLine("\nTo find the diary by Name, Press 2\n");
                        //            ////Console.WriteLine("Which User Do you want to find?");
                        //            ////user.DisplayDiaries();
                        //            //if (Console.ReadKey() == '1') { }
                        //            Console.WriteLine("Enter Diary Id:");
                        //            int id = Utility.getIntInput();
                        //            //if (id == 0) { Console.Clear(); break; }
                        //            user.FindDiary(id);
                        //            Console.WriteLine("Press to Continue!");
                        //            Console.ReadKey();
                        //        }
                        //        else if (user.userDiaries.diaryCount() == 0)
                        //        {
                        //            Console.WriteLine("No Diaries available");
                        //            Console.WriteLine("Press to Continue!");
                        //            Console.ReadKey();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                        //        Console.WriteLine("Press to Continue!");
                        //        Console.ReadKey();

                        //    }
                        //    break;
                        //}
                        case (int)AdminOptions.DeleteDiary:
                            {
                                DeleteDiaryInterface(user);
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();
                                break;
                            }
                        case (int)AdminOptions.Display_myDiaries:
                            {
                                DisplayDiariesInterface(user);

                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();
                                break;
                            }
                        case (int)AdminOptions.Create:
                            {
                                Console.Clear();
                                Console.WriteLine("==== To Create a User ====");
                                Console.WriteLine("\nTo exit at any point Enter exit\n");
                                Console.WriteLine("Enter User Type:");
                                string type = Utility.getInput().ToLower();
                                if (type == "exit") { Console.Clear(); break; }
                                Console.WriteLine("Enter User Name:");
                                string name = Utility.getInput();
                                if (name == "exit") { Console.Clear(); break; }
                                Console.WriteLine("Enter User Password: ");
                                string passcode = Utility.getInput();
                                if (passcode == "exit") { Console.Clear(); break; }
                                
                                Console.WriteLine("Enter UserName:");
                                string inputUsername = Utility.getInput();
                                while (!Utility.ValidateUsername(inputUsername))
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nEnter Username:");
                                    inputUsername = Utility.getInput();
                                };
                                if (inputUsername == "exit") { Console.Clear(); break; }
                                string username = inputUsername;

                                user.CreateUser(type, username, name, passcode);
                                Console.WriteLine($"\nUser Created!\n");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();

                                break;
                            }
                        case (int)AdminOptions.Delete:
                            {
                                Console.Clear();
                                if (Cache.getCache().defaultEmpList.Count != 0)
                                {
                                    string idInput;
                                    do
                                    {
                                        Console.WriteLine("==== To Delete a User ====");
                                        Console.WriteLine("\nTo exit at any point Enter 0\n");
                                        Console.WriteLine("Which User Do you want to delete?");
                                        user.DisplayUserLists();
                                        Console.WriteLine("Enter User Id:");

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

                                        if (id == 0) { Console.Clear(); break; }
                                        Console.Clear();

                                        user.DeleteUser(id);
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("No Users Available!");

                                }
                                    Console.WriteLine("\nPress to Continue");
                                    Console.ReadKey();
                                    break;
                            }
                        case (int)AdminOptions.Find:
                            {
                                Console.Clear();
                                if (Cache.getCache().defaultEmpList.Count != 0)
                                {

                                    Console.WriteLine("==== To Find a User ====\n");

                                    Console.WriteLine("\nTo exit at any point Enter exit\n");
                                    Console.WriteLine(
                                "Enter Username:");
                                    string id = Utility.getInput();
                                    if (id == "exit") { Console.Clear(); break; }
                                    if (user.FindUser(id) is null)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\nNot Available!");
                                    }
                                    Console.WriteLine("\nPress to Continue");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("No Users Available!\n Press Continue");

                                    Console.ReadKey();

                                }
                                break;
                            }
                        case (int)AdminOptions.Authorize:
                            {
                                Console.Clear();
                                if (Cache.getCache().defaultEmpList.Count != 0)
                                {
                                    string idInput;
                                    do
                                    {
                                        Console.WriteLine("==== To Authorize a User ====");
                                        Console.WriteLine("\nTo exit at any point Enter 0\n");

                                        Console.WriteLine("Which User Do you want to authorize?");
                                        user.DisplayUserLists();
                                        Console.WriteLine("Enter User Id:");

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

                                        if (id == 0) { Console.Clear(); break; }
                                        Console.Clear();

                                    user.AuthorizeUser(id);
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("No Users Available! ");
                                }
                                    Console.WriteLine("\nPress to Continue");
                                    Console.ReadKey();
                                break;
                            }
                        case (int)AdminOptions.Unauthorize:
                            {
                                Console.Clear();
                                if (Cache.getCache().defaultEmpList.Count != 0)
                                {
                                    string idInput;
                                    do
                                    {
                                        Console.WriteLine("==== To Unauthorize a User ====");
                                        Console.WriteLine("\nTo exit at any point Enter 0\n");
                                        Console.WriteLine("Which User Do you want to unauthorize?");
                                        user.DisplayUserLists();
                                        Console.WriteLine("Enter User Id:");

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

                                        if (id == 0) { Console.Clear(); break; }
                                        Console.Clear();

                                    user.DeleteDiaryList(id);
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("No Users Available! ");
                                }
                                Console.WriteLine("\nPress to Continue");
                                Console.ReadKey();

                                break;
                            }
                        case (int)AdminOptions.Find_Diary:
                            {
                                Console.Clear();
                                if (Cache.getCache().defaultEmpList.Count != 0)
                                {
                                    string idInput;
                                    do
                                    {
                                        Console.WriteLine("==== To Find a User Diary ====");
                                        Console.WriteLine("\nTo exit at any point Enter 0\n");
                                        Console.WriteLine("Which User Diary do you want?");
                                        user.DisplayUserLists();
                                        Console.WriteLine("Enter User Id:");

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

                                        if (id == 0) { Console.Clear(); break; }
                                        Console.Clear();

                                    user.FindDiaryList(id);
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("No Users Available! ");
                                }
                                Console.WriteLine("\nPress to Continue");
                                Console.ReadKey();

                                break;
                            }

                        case (int)AdminOptions.Display_Users:
                            {
                                Console.Clear();
                                if (Cache.getCache().defaultEmpList.Count != 0)
                                {
                                    Console.WriteLine("==== Users Displayed ====\n");
                                    user.DisplayUserLists();
                                    Console.WriteLine("\nPress to Continue");
                                    Console.ReadKey();

                                }
                                else
                                {
                                    Console.WriteLine("No Users Available!\n Press Continue");
                                    Console.ReadKey();
                                }
                                break;
                            }
                        case (int)AdminOptions.Display_Admins:
                            {
                                Console.Clear();
                                if (Cache.getCache().defaultAdminList.Count != 1)
                                {
                                    Console.WriteLine("==== Admins Displayed ====\n");
                                    user.DisplayAdminLists();
                                    Console.WriteLine("\nPress to Continue");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("No Admins Available except you!\n Press Continue");
                                    Console.ReadKey();
                                }

                                break;
                            }
                        case (int)AdminOptions.Display_Diaries:
                            {
                                Console.Clear();
                                if (Cache.getCache().defaultEmpList.Count != 0)
                                {
                                    Console.WriteLine("==== User Diaries Displayed ====");
                                    user.DisplayDiaryLists();
                                    Console.WriteLine("\nPress to Continue");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("No Users Available!\n Press Continue");
                                    Console.ReadKey();
                                }
                                break;
                            }
                        case (int)AdminOptions.Update_Profile:
                            {
                                UpdateProfileInterface(user);

                                break;
                            }
                        case (int)AdminOptions.Display:
                            UserDisplay(user);

                            Console.WriteLine("Press to Continue!");
                            Console.ReadKey();
                            break;
                        case (int)AdminOptions.Logout:
                            Console.Clear();
                            Console.WriteLine("========== Do you really want to logout? =========\n" + "If Yes press y, Else press n");
                            string response = Utility.getInput();
                            if (response == "y")
                            {
                                cache.Logout();
                                user = null;

                            }
                            break;
                        default:
                            break;
                    }

                }
            }
        }
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


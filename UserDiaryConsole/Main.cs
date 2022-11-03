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
        
        public Main(){
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

                Console.WriteLine("\nDo you want to Login or Register an Account?\n"+
                "- To Login Press 0\n" + "- To Register Press 1\n" + "- To Exit Press 2\n");
            int opt = Utility.getIntInput();
            if (opt == (int)MainOptions.Login) {
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
            else { Console.WriteLine("Wrong Input!"); }
            } while (user is null);
        }
    void RegisterInterface() {
        Console.WriteLine("=========== Register ===========\n");
            bool temp = true;
            Console.WriteLine("\nTo exit at any point Enter exit");
            do
            {
                Console.WriteLine("Enter Name:");
                string name = Utility.getInput();
                if (name == "exit") { Console.Clear(); break; }
                
                Console.WriteLine("Enter UserName:");
                string username = Utility.getUsernameInput();
                if (username == "exit") { Console.Clear(); break; }
                
                Console.WriteLine("Enter Password: ");
                string passcode = Utility.getInput();
                if (passcode == "exit") { Console.Clear(); break; }
                
                Console.WriteLine("If you want don't want to insert now\nPress Enter\n" +
                    "\nEnter Phone Number (03xx-xxxxxxx): ");
                string phone = Utility.getPhoneInput();
                if (phone == "exit") { Console.Clear(); break; }
                Console.WriteLine("If you want don't want to insert now\nPress Enter");
                Console.WriteLine("Enter Email Address: ");
                string email = Utility.getEmailInput();
                if (email == "exit") { Console.Clear(); break; }


                User emp = new User(username, name, passcode, "user", "pending", phone, email);
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
                Console.WriteLine("Enter your Username: ");
                string username = Utility.getInput("Enter your Username: ");
                if (username == "exit"){Console.Clear(); break;}
                
                Console.WriteLine("Enter your Password: ");
                string password = Utility.getInput("Enter your Password: ");
                Console.WriteLine(string.Format("{0} {1}", username, password));
                if (password == "exit")
                {
                    Console.Clear();
                    break;
                }

                cache.UserLog(username, password);

            } while (user is null);
            if (user is not null && user.Type == Types.user.ToString())
                {
                RunUserFunctions(user);

                } else if (user is not null && user.Type == Types.admin.ToString())
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
        while (user is not null) {
                Console.Clear();
                Console.WriteLine("========== Welcome ===========");
                Console.WriteLine("\nWhat do you want to do?");
                    Console.WriteLine(
                        $"- To Create a Diary Press {((int)UserOptions.Create)}\n"+
                        $"- To Update a Diary Press {((int)UserOptions.Update)}\n" + 
                        //$"- To Find a Diary Press {((int)UserOptions.Find)}\n" +
                        $"- To Delete a Diary Press {((int)UserOptions.Delete)}\n" +
                        $"- To Display Diaries Press {((int)UserOptions.Display_Diaries)}\n" +
                        $"- To Update your Profile Press {((int)UserOptions.Update_Profile)}\n" +
                        $"- To Display your Profile Press {((int)UserOptions.Display)}\n" +
                        $"- To Logout Press {((int)UserOptions.Logout)}\n");
                    int input = Utility.getIntInput();
                switch (input)
	                {
                        case (int)UserOptions.Create:
                        {
                            Console.Clear();
                            if (user.userDiaries is not null)
                            {
                                Console.WriteLine("==== To Create a Diary ====");
                                Console.WriteLine("\nTo exit at any point Enter exit\n");

                                Console.WriteLine("Enter Diary Name:");

                                string name = Utility.getInput("Enter Diary Name:");
                                if (name == "exit") { Console.Clear(); break; }
                                Console.WriteLine("Enter Content: ");
                                string content = Console.ReadLine();
                                if (content == "exit") { Console.Clear(); break; }
                                user.CreateDiary(name, content);
                                Console.Clear();
                                Console.WriteLine("Diary Created!");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();

                            }
                            break;
                        }
                        case (int)UserOptions.Update:
                        {
                            Console.Clear();
                            if (user.userDiaries is not null)
                            {
                                if (user.userDiaries.diaryCount() != 0)
                                {
                                    Console.WriteLine("==== To Update a Diary ====");
                                    Console.WriteLine("\nTo exit at any point Enter 0\n");
                                    Console.WriteLine("Which User Do you want to update?");
                                    user.DisplayDiaries();
                                    Console.WriteLine("Enter Diary Id:");
                                    int id = Utility.getIntInput();
                                    if (id == 0) { Console.Clear(); break; }
                                    Console.Clear();
                                    if (user.FindDiary(id))
                                    {
                                    //getInput("Enter Diary Name:");
                                    Console.WriteLine("Enter Name of the Diary:");
                                    string name = Console.ReadLine();
                                    if (name == "0") { Console.Clear(); break; }
                                    Console.WriteLine("Enter Content: ");
                                    string content = Console.ReadLine();
                                    if (content == "0") { Console.Clear(); break; }
                                    
                                    user.UpdateDiary(id, name, content);

                                    }
                                    else { Console.Clear(); Console.WriteLine("\nNo Diary of this ID\n"); }
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                                else if (user.userDiaries.diaryCount() == 0)
                                {
                                    Console.WriteLine("No Diaries available");
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();

                            }
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
                            Console.Clear();
                            if (user.userDiaries is not null)
                            {
                                if (user.userDiaries.diaryCount() != 0)
                                {
                                    Console.WriteLine("==== To Delete a Diary ====");
                                    Console.WriteLine("\nTo exit at any point Enter 0\n");
                                    Console.WriteLine("Which User Do you want to delete?");
                                    user.DisplayDiaries();
                                    Console.WriteLine("Enter Diary Id:");
                                    int id = Utility.getIntInput();
                                    if (id == 0) { Console.Clear(); break; }

                                    user.DeleteDiary(id);
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                                else if (user.userDiaries.diaryCount() == 0)
                                {
                                    Console.WriteLine("No Diaries available");
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();

                            }
                            break;
                        }
                        case (int)UserOptions.Display_Diaries:
                        {
                            Console.Clear();
                            if (user.userDiaries is not null)
                            {
                                if (user.userDiaries.diaryCount() != 0)
                                {


                                    Console.WriteLine("==== Diaries Displayed ====\n");
                                    user.DisplayDiaries();
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                                else if (user.userDiaries.diaryCount() == 0)
                                {
                                    Console.WriteLine("No Diaries available");
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                            }

                            else
                            {
                                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();

                            }
                            break;
                        }
                        case (int)UserOptions.Update_Profile:
                        {
                            Console.Clear();
                            Console.WriteLine("==== To Update your Profile ====\n");
                            user.display();
                            Console.WriteLine("Enter Name if you want to change:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Password if you want to change:");
                            string passcode = Console.ReadLine();
                            Console.WriteLine("Enter Phone Number (03xx-xxxxxxx) if you want to change:");
                            string phone = Utility.getPhoneInput();
                            Console.WriteLine("Enter Email if you want to change:");
                            string email = Utility.getEmailInput();

                            user.UpdateUser(name, passcode, phone, email);
                            Console.WriteLine("Press to Continue!");
                            Console.ReadKey();
                            break;
                        }
                        case (int)UserOptions.Display:
                            Console.Clear();
                            Console.WriteLine("===== Profile Displayed =====");
                            user.display();
                            Console.WriteLine("Press to Continue!");
                            Console.ReadKey();
                        break;
                        case (int)UserOptions.Logout:
                            Console.Clear();
                            Console.WriteLine("==== Do you really want to logout? ====\n"+"If Yes press y");
                            string response = Utility.getInput();
                            if (response == "y")
                            {
                                cache.Logout();
                            user = null;
                            }
                                break;
		                default:
                            break;}
	                    
                    
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
        while (user is not null) {
                Console.Clear();
                Console.WriteLine("========== Welcome ===========");
                Console.WriteLine("\nWhat do you want to do as Admin?");
                    Console.WriteLine(
                        $"- To Create a Diary Press {((int)AdminOptions.CreateDiary)}\n" +
                        $"- To Update a Diary Press {((int)AdminOptions.UpdateDiary)}\n" +
                        //$"- To Find a Diary Press {((int)AdminOptions.FindDiary)}\n" +
                        $"- To Delete a Diary Press {((int)AdminOptions.DeleteDiary)}\n" +
                        $"- To Display Diaries Press {((int)AdminOptions.Display_myDiaries)}\n" +
                        $"- To Create a User Press {(int)AdminOptions.Create}\n"+
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
                    int input = Utility.getIntInput();
                switch (input)
	                {
                    case (int)AdminOptions.CreateDiary:
                        {
                            Console.Clear();
                            if (user.userDiaries is not null)
                            {
                                Console.WriteLine("==== To Create a Diary ====");
                                Console.WriteLine("\nTo exit at any point Enter exit\n");

                                Console.WriteLine("Enter Diary Name:");

                                string name = Utility.getInput("Enter Diary Name:");
                                if (name == "exit") { Console.Clear(); break; }
                                Console.WriteLine("Enter Content: ");
                                string content = Console.ReadLine();
                                if (content == "exit") { Console.Clear(); break; }
                                user.CreateDiary(name, content);
                                Console.Clear();
                                Console.WriteLine("Diary Created!");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();

                            }
                            break;
                        }
                    case (int)AdminOptions.UpdateDiary:
                        {
                            Console.Clear();
                            if (user.userDiaries is not null)
                            {
                                if (user.userDiaries.diaryCount() != 0)
                                {
                                    Console.WriteLine("==== To Update a Diary ====");
                                    Console.WriteLine("\nTo exit at any point Enter 0\n");
                                    Console.WriteLine("Which User Do you want to update?");
                                    user.DisplayDiaries();
                                    Console.WriteLine("Enter Diary Id:");
                                    int id = Utility.getIntInput();
                                    if (id == 0) { Console.Clear(); break; }
                                    Console.Clear();
                                    if (user.FindDiary(id))
                                    {
                                        //getInput("Enter Diary Name:");
                                        Console.WriteLine("Enter Name of the Diary:");
                                        string name = Console.ReadLine();
                                        if (name == "0") { Console.Clear(); break; }
                                        Console.WriteLine("Enter Content: ");
                                        string content = Console.ReadLine();
                                        if (content == "0") { Console.Clear(); break; }

                                        user.UpdateDiary(id, name, content);

                                    }
                                    else { Console.Clear(); Console.WriteLine("\nNo Diary of this ID\n"); }
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                                else if (user.userDiaries.diaryCount() == 0)
                                {
                                    Console.WriteLine("No Diaries available");
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();

                            }
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
                            Console.Clear();
                            if (user.userDiaries is not null)
                            {
                                if (user.userDiaries.diaryCount() != 0)
                                {
                                    Console.WriteLine("==== To Delete a Diary ====");
                                    Console.WriteLine("\nTo exit at any point Enter 0\n");
                                    Console.WriteLine("Which User Do you want to delete?");
                                    user.DisplayDiaries();
                                    Console.WriteLine("Enter Diary Id:");
                                    int id = Utility.getIntInput();
                                    if (id == 0) { Console.Clear(); break; }

                                    user.DeleteDiary(id);
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                                else if (user.userDiaries.diaryCount() == 0)
                                {
                                    Console.WriteLine("No Diaries available");
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();

                            }
                            break;
                        }
                    case (int)AdminOptions.Display_myDiaries:
                        {
                            Console.Clear();
                            if (user.userDiaries is not null)
                            {
                                if (user.userDiaries.diaryCount() != 0)
                                {


                                    Console.WriteLine("==== Diaries Displayed ====\n");
                                    user.DisplayDiaries();
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                                else if (user.userDiaries.diaryCount() == 0)
                                {
                                    Console.WriteLine("No Diaries available");
                                    Console.WriteLine("Press to Continue!");
                                    Console.ReadKey();
                                }
                            }

                            else
                            {
                                Console.WriteLine($"\nNo diary space for {user.Name} contact your admin\n");
                                Console.WriteLine("Press to Continue!");
                                Console.ReadKey();

                            }
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
                            Console.WriteLine("Enter UserName: ");
                            string username = Utility.getUsernameInput();
                            if (username == "exit") { Console.Clear(); break; }
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
                            Console.WriteLine("==== To Delete a User ====");
                            Console.WriteLine("\nTo exit at any point Enter 0\n");
                            Console.WriteLine("Which User Do you want to delete?");
                            user.DisplayUserLists();
                            Console.WriteLine("Enter User Id:");
                            int id = Utility.getIntInput();

                            if (id == 0) { Console.Clear(); break; }
                            user.DeleteUser(id);
                            Console.WriteLine("\nPress to Continue");
                                Console.ReadKey();
                            break;

                            }
                            else
                            {
                                Console.WriteLine("No Users Available!\nPress Continue");

                                Console.ReadKey();
                                break;
                            }
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
                                if (user.FindUser(id) is null) Console.Clear(); Console.WriteLine("\nNot Available!");
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
                                Console.WriteLine("==== To Authorize a User ====");
                                Console.WriteLine("\nTo exit at any point Enter 0\n");

                                Console.WriteLine("Which User Do you want to authorize?");
                                user.DisplayUserLists();
                                Console.WriteLine("Enter User Id:");
                                int id = Utility.getIntInput();
                                if (id == 0) { Console.Clear(); break; }
                                user.AuthorizeUser(id);
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
                        case (int)AdminOptions.Unauthorize:
                        {
                            Console.Clear();
                            if (Cache.getCache().defaultEmpList.Count != 0)
                            {
                                Console.WriteLine("==== To Unauthorize a User ====");
                                Console.WriteLine("\nTo exit at any point Enter 0\n");
                                Console.WriteLine("Which User Do you want to unauthorize?");
                                user.DisplayUserLists();
                                Console.WriteLine("Enter User Id:");
                                int id = Utility.getIntInput();
                                if (id == 0) { Console.Clear(); break; }
                                user.DeleteDiaryList(id);
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
                        case (int)AdminOptions.Find_Diary:
                        {
                            Console.Clear();
                            if (Cache.getCache().defaultEmpList.Count != 0)
                            {
                                Console.WriteLine("==== To Find a User Diary ====");
                                Console.WriteLine("\nTo exit at any point Enter 0\n");
                                Console.WriteLine("Which User Do you want to unauthorize?");
                                user.DisplayUserLists();
                                Console.WriteLine("Enter User Id:");
                                int id = Utility.getIntInput();
                                if (id == 0) { Console.Clear(); break; }
                                user.FindDiaryList(id);
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
                        //case (int)AdminOptions.Add_Admin:
                        //{
                        //    Console.Clear();
                        //    Console.WriteLine("==== To Create an Admin ====");
                        //    Console.WriteLine("\nTo exit at any point Enter exit\n");
                        //    Console.WriteLine("Enter Name:");
                        //    string name = Utility.Utility.getInput();
                        //    if (name == "exit") { Console.Clear(); break; }

                        //    Console.WriteLine("Enter UserName:");
                        //    string username = Utility.getUsernameInput();
                        //    if (username == "exit") { Console.Clear(); break; }

                        //    Console.WriteLine("Enter Password: ");
                        //    string passcode = Utility.Utility.getInput();
                        //    if (passcode == "exit") { Console.Clear(); break; }

                        //    user.CreateAdmin(username, name, passcode);
                        //    Console.WriteLine($"\nAdmin Created!\n");
                        //    Console.WriteLine("Press to Continue!");
                        //    Console.ReadKey();
                        //    break;
                        //}
                        //case (int)AdminOptions.Remove_Admin:
                        //{
                        //    Console.Clear();
                        //    if (Cache.defaultAdminList.Count != 1) {
                        //        Console.WriteLine("==== To Delete an Admin ====");
                        //        Console.WriteLine("\nTo exit at any point Enter 0\n");
                        //        Console.WriteLine("Which User Do you want to unauthorize?");
                        //        user.DisplayAdminLists();
                        //        Console.WriteLine("Enter Admin Id:");
                        //        int id = Utility.getIntInput();
                        //        if (id == 0) { Console.Clear(); break; }
                        //        user.DeleteAdmin(id);
                        //        Console.WriteLine("\nPress to Continue");
                        //        Console.ReadKey();
                        //    } else
                        //    {
                        //        Console.WriteLine("No Admins Available except you!\n Press Continue");
                        //        Console.ReadKey();
                        //    }
                        //    break;
                        //}
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
                            } else
                            {
                                Console.WriteLine("No Users Available!\n Press Continue");
                                Console.ReadKey();
                            }
                            break;
                        }
                        case (int)AdminOptions.Update_Profile:
                        {
                            Console.Clear();
                            Console.WriteLine("==== To Update your Profile ====\n");
                            user.display();
                            Console.WriteLine("Enter Name if you want to change:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Password if you want to change:");
                            string passcode = Console.ReadLine();
                            Console.WriteLine("Enter Phone Number (03xx-xxxxxxx) if you want to change:");
                            string phone = Utility.getPhoneInput();
                            Console.WriteLine("Enter Email if you want to change:");
                            string email = Utility.getEmailInput();

                            user.UpdateUser(name, passcode, phone, email);
                            Console.WriteLine("\nPress to Continue");
                            Console.ReadKey();
                            break;
                        }
                        case (int)AdminOptions.Display:
                            Console.Clear();
                            Console.WriteLine("===== Profile Displayed =====");
                            user.display();
                            Console.WriteLine("Press to Continue!");
                            Console.ReadKey();
                            break;
                        case (int)AdminOptions.Logout:
                            Console.Clear();
                            Console.WriteLine("========== Do you really want to logout? =========\n"+"If Yes press y, Else press n");
                            string response = Utility.getInput();
                            if (response == "y")
                            {
                                cache.Logout();
                                user = null;
                                
                            }
                                break;
		                default:
                            break;}
	                    
                    
        }
    }
   
    }
}


using System;

/// <summary>
/// Summary description for View
/// Console Interface of the Diaries
/// </summary>
namespace UserDiaryConsole
{
    public class View
    {
        Cache cache;
        dynamic user;
        
        public View(){
            Console.WriteLine("===========Welcome to the Diaries===========\n");
            cache = new Cache();

            MainInterface();
        }
    void MainInterface()
        {
            do
            {
                Console.WriteLine("\nDo you want to Login or Register an Accout?\n"+
                "- To Login Press 0\n" + "- To Register Press 1\n" + "- To Exit Press 2\n");
            int opt = getIntInput();
            if (opt == 0) {
                    LoginInterface();
            }
            else if (opt == 1)
            {
                    RegisterInterface();
            }
            else if (opt == 2)
            {
                    break;
            }
            else { Console.WriteLine("Wrong Input!"); }
            } while (user is null);
        }
    void RegisterInterface() {
        Console.WriteLine("=========== Register ===========\n");
            bool temp = true;
            do {
            Console.WriteLine("\nDo you want to Create Account as Admin or User?\n"+
                "- To Register as Admin Press 0\n" + "- To Register as User Press 1\n" + "- To Exit Press 2\n");
            int opt = getIntInput();
            if (opt == 0) {
                Console.WriteLine("===Admin Register===");
                Console.WriteLine("Enter Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Password: ");
                string passcode = Console.ReadLine();
                Console.WriteLine("Enter Phone Number: ");
                string phone = Console.ReadLine();
                Console.WriteLine("Enter Email Address: ");
                string email = Console.ReadLine();
                
                Register.AdminRegister(name, passcode, phone, email);
                break;
            }
            else if (opt == 1)
            {
                Console.WriteLine("===User Register===\n");
                Console.WriteLine("Enter Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Password: ");
                string passcode = Console.ReadLine();
                Console.WriteLine("Enter Phone Number: ");
                string phone = Console.ReadLine();
                Console.WriteLine("Enter Email Address: ");
                string email = Console.ReadLine();
                
                Register.EmployeeRegister(name, passcode, phone, email);
                break;
            }
            else if (opt == 2)
            {
                    break;
            }
            else { Console.WriteLine("Wrong Input!"); }
            } while (temp);
        }
    void LoginInterface()
        {
            Console.WriteLine("=========== Login ===========\n");
            RunUserLogin(user);
            //do {
            //Console.WriteLine("\nDo you want to login as Admin or User?\n"+
            //    "- To Login as Admin Press 0\n" + "- To Login as User Press 1\n" + "- To Exit Press 2\n");
            //int opt = Convert.ToInt32(Console.ReadLine());
            //if (opt == 0) {
            //    Console.WriteLine("===Admin Login===");
            //    RunAdminLogin(user);
            //}
            //else if (opt == 1)
            //{
            //    Console.WriteLine("===User Login===");
            //    RunUserLogin(user);
            //}
            //else if (opt == 2)
            //{
            //        break;
            //}
            //else { Console.WriteLine("Wrong Input!"); }
            //} while (user is null);
        }
    void RunUserLogin(dynamic user)
        {
            do
	            {
                Console.WriteLine("Enter your UserId: ");
                int id = getIntInput(); 
                Console.WriteLine("Enter your Password: ");
                string password = Console.ReadLine();

                user = cache.UserLog(id, password);

	            } while (user is null);
                if (user is EmployeeUser)
                {
                RunUserFunctions(user);

                } else if (user is AdminUser)
                {
                RunAdminFunctions(user);
                }
        }
    //void RunAdminLogin(AdminUser user)
    //    {
    //        do
	   //         {
    //            Console.WriteLine("Enter your UserId: ");
    //            int id;
    //            try
    //            {
    //                id = Convert.ToInt32(Console.ReadLine());
    //            }
    //            catch
    //            {
    //                Console.WriteLine("UserId is not Number!\n Try it again");
    //                id = Convert.ToInt32(Console.ReadLine());
    //            } 
    //            Console.WriteLine("Enter your Password: ");
    //            string password = Console.ReadLine();

    //            user = cache.AdminLog(id, password);

	   //         } while (user is null);
    //            RunAdminFunctions(user);
    //    }
    void RunUserFunctions(EmployeeUser user)
        {
        outer: while (user is not null) {
                    Console.WriteLine("\nWhat do you want to do?");
                    Console.WriteLine(
                        "- To Create a Diary Press 0\n"+
                        "- To Update a Diary Press 1\n" + 
                        "- To Find a Diary Press 2\n" +
                        "- To Delete a Diary Press 3\n" +
                        "- To Display Diaries Press 4\n" +
                        "- To Update your Profile Press 5\n" +
                        "- To Display your Profile Press 6\n" +
                        "- To Logout Press 7\n");
                    int input = getIntInput();
                switch (input)
	                {
                        case 0:
                        {
                            Console.WriteLine("==== To Create a Diary ====\n"+
                            "Enter Name of the Diary:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Content: ");
                            string content = Console.ReadLine();
                            user.CreateDiary(name, content);
                            break;
                        }
                        case 1:
                        {
                            Console.WriteLine("==== To Update a Diary ====\n"+
                            "Enter Diary Id:");
                            int id = getIntInput();
                            Console.WriteLine("Enter Name of the Diary:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Content: ");
                            string content = Console.ReadLine();
                            user.UpdateDiary(id, name, content);
                            break;
                        }
                        case 2:
                        {
                            Console.WriteLine("==== To Find a Diary ====\n"+
                            "Enter Diary Id:");
                            int id = getIntInput();
                            user.FindDiary(id);
                            break;
                        }
                        case 3:
                        { 
                            Console.WriteLine("==== To Delete a Diary ====\n"+
                            "Enter Diary Id:");
                            int id = getIntInput();
                            user.DeleteDiary(id);
                            break;
                        }
                        case 4:
                        { 
                            Console.WriteLine("==== Diaries Displayed ====\n");
                            user.DisplayDiaries();
                            break;
                        }
                        case 5:
                        { 
                            Console.WriteLine("==== To Update your Profile ====\n");
                            user.display();
                            Console.WriteLine("Enter Name if you want to change:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Password if you want to change:");
                            string passcode = Console.ReadLine();
                            Console.WriteLine("Enter Phone Number if you want to change:");
                            string phone = Console.ReadLine();
                            Console.WriteLine("Enter Email if you want to change:");
                            string email = Console.ReadLine();

                            user.UpdateUser(name, passcode, phone, email);
                            break;
                        }
                        case 6:
                            Console.WriteLine("===== Profile Displayed =====");
                            user.display();
                            break;
                        case 7:
                            Console.WriteLine("==== Do you really want to logout? ====\n"+"If Yes press y, Else press n");
                            string response = Console.ReadLine();
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
    void RunAdminFunctions(AdminUser user)
        {
        outer: while (user is not null) {
                    Console.WriteLine("\nWhat do you want to do as Admin?");
                    Console.WriteLine(
                        "- To Create a User Press 0\n"+
                        "- To Delete a User Press 1\n" + 
                        "- To Find a User Press 2\n" +
                        "- To Authorize a User Press 3\n" +
                        "- To UnAuthorize a User Press 4\n" +
                        "- To Find User Diary Press 5\n" +
                        "- To Delete an Admin Press 6\n" + 
                        "- To Display Users Press 7\n" +
                        "- To Display Admins Press 8\n" +
                        "- To Display User Diaries Press 9\n" +
                        "- To Update your Profile Press 10\n" +
                        "- To Display your Profile Press 11\n" +
                        "- To Logout Press 12\n");
                    int input = getIntInput();
                switch (input)
	                {
                        case 0:
                        {

                            Console.WriteLine("==== To Create a User ====\n"+
                            "Enter User Name:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter User Password: ");
                            string passcode = Console.ReadLine();
                            user.CreateUser(name, passcode);
                            break;
                        }
                        case 1:
                        {
                            Console.WriteLine("==== To Delete a User ====\n"+
                            "Enter User Id:");
                            int id = getIntInput();
                            user.DeleteUser(id);
                            break;
                        }
                        case 2:
                        {

                            Console.WriteLine("==== To Find a User ====\n"+
                            "Enter User Id:");
                            int id = getIntInput();
                            user.FindUser(id);
                            break;
                        }
                        case 3:
                        { 
                            Console.WriteLine("==== To Authorize a User ====\n"+
                            "Enter User Id:");
                            int id = getIntInput();
                            user.AuthorizeUser(id);
                            break;
                        }
                        case 4:
                        { 
                            Console.WriteLine("==== To Unauthorize a User ====\n"+
                            "Enter User Id:");
                            int id = getIntInput();
                            user.DeleteDiaryList(id);
                            break;
                        }
                        case 5:
                        { 
                            Console.WriteLine("==== To Find a User Diary ====\n"+
                            "Enter User Id:");
                            int id = getIntInput();
                            user.FindDiaryList(id);
                            break;
                        }
                        case 6:
                        {
                            Console.WriteLine("==== To Delete an Admin ====\n"+
                            "Enter Admin Id:");
                            int id = getIntInput();
                            user.DeleteAdmin(id);
                            break;
                        }
                        case 7:
                        {
                            Console.WriteLine("==== Users Displayed ====\n");
                            user.DisplayUserLists();
                            break;
                        }
                        case 8:
                        { 
                            Console.WriteLine("==== Admins Displayed ====\n");
                            user.DisplayAdminLists();
                            break;
                        }
                        case 9:
                        { 
                            Console.WriteLine("==== User Diaries Displayed ====");
                            user.DisplayDiaryLists();
                            break;
                        }
                        case 10:
                        { 
                            Console.WriteLine("==== To Update your Profile ====\n");
                            user.display();
                            Console.WriteLine("Enter Name if you want to change:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter Password if you want to change:");
                            string passcode = Console.ReadLine();
                            Console.WriteLine("Enter Phone Number if you want to change:");
                            string phone = Console.ReadLine();
                            Console.WriteLine("Enter Email if you want to change:");
                            string email = Console.ReadLine();

                            user.UpdateUser(name, passcode, phone, email);
                            break;
                        }
                        case 11:
                            Console.WriteLine("===== Profile Displayed =====");
                            user.display();
                            break;
                        case 12:
                            Console.WriteLine("==== Do you really want to logout? ====\n"+"If Yes press y, Else press n");
                            string response = Console.ReadLine();
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

    int getIntInput()
        {
            int id;
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
                return id;
            }
            catch
            {
                Console.WriteLine("Incorrect input!\n Try it again\n");
                id = getIntInput();
                return id;
            }
        }
}
}


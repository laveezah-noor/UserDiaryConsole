using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{
    public class UserLogin
    {
        static UserLogin instance = new UserLogin();
        public static dynamic currentUser;
        public UserLogin() { }
        
        public static void getLoggedOut(dynamic user)
        {
            user.Logout();
            instance = null;
            currentUser = null;
        }
   
        public static bool getLoggedIn(int userId, string password)
        {

            if (currentUser == null)
            {
                
                    dynamic user = Cache.UserList.Employee_UserList.findUser(userId);
                    if (user is not null)
                    {
                        if (user.Login(userId, password)) {
                            UserLogin.currentUser = user;
                            return true;
                        }
                        else
                        {
                        Console.WriteLine("\nIncorrect Credentials\n");
                        return false;
                        }
                    user = null;
                    }
                    user = Cache.UserList.Admin_UserList.findUser(userId);
                    if (user is not null)
                    {
                        if (user.Login(userId, password)) {
                           UserLogin.currentUser = user;
                            return true;
                        } else
                        {
                        Console.WriteLine("\nIncorrect Credentials\n");
                        return false;
                        }       
                    }
                    
            } else Console.WriteLine("Already Logged In");
            return false;
        }
        public static UserLogin getInstance()
        {
            if (instance != null)
            {

                instance = new UserLogin();

            };
            return instance;
        }
        public string print()
        {
            return "\nLogged In\n";
        }

    }
}

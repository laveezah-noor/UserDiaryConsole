using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{

    public class AdminLogin
    {
        static AdminLogin instance = new AdminLogin();
        public static AdminUser currentUser;
        private AdminLogin() { }
        public static void getLoggedOut()
        {
            currentUser.Logout();
            instance = null;
            currentUser = null;
        }
        public static bool getLoggedIn(int userId, string password)
        {

            if (AdminLogin.currentUser == null)
            {

                AdminUser user = View.UserList.Admin_UserList.findUser(userId);
                if (user is not null &&
                        user.Login(userId, password))
                {
                    AdminLogin.currentUser = user;
                    return true;
                }
                Console.WriteLine("Incorrect Credentials");

            };
            Console.WriteLine("Already Logged In");
            return false;
        }
        public static AdminLogin getInstance()
        {
            if (AdminLogin.instance != null)
            {

                AdminLogin.instance = new AdminLogin();

            };
            return instance;
        }
        public string print()
        {
            return "\nLogged In\n";
        }

    }

    public class UserLogin
    {
        static UserLogin instance = new UserLogin();
        public static EmployeeUser currentUser;
        private UserLogin() { }
        public static void getLoggedOut()
        {
            currentUser.Logout();
            instance = null;
            currentUser = null;
        }
        public static bool getLoggedIn(int userId, string password)
        {

            if (UserLogin.instance != null)
            {
                EmployeeUser user = View.UserList.Employee_UserList.findUser(userId);
                if (user is not null &&
                        user.Login(userId, password))
                {
                    UserLogin.currentUser = user;
                    return true;
                }
                Console.WriteLine("Incorrect Credentials");

            };
            return false;
        }
        public static UserLogin getInstance()
        {
            if (UserLogin.instance != null)
            {

                UserLogin.instance = new UserLogin();

            };
            return instance;
        }
        public string print()
        {
            return "\nLogged In\n";
        }

    }
}

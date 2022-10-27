using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{
    public class UserLogin<T>
    {
        static UserLogin<T> instance = new UserLogin<T>();
        public static T currentUser;
        private UserLogin() { }
        public static void getLoggedOut(dynamic user)
        {
            user.Logout();
            instance = null;
            currentUser = default(T);
        }
        public static bool getLoggedIn(int userId, string password)
        {

            if (instance != null)
            {
                if(instance is UserLogin<EmployeeUser>)
                {
                    //Console.WriteLine("Employee");
                    EmployeeUser user = Cache.UserList.Employee_UserList.findUser(userId);
                if (user is not null &&
                        user.Login(userId, password))
                {
                    UserLogin<EmployeeUser>.currentUser = user;
                    return true;
                }
                Console.WriteLine("Incorrect Credentials");
                }
                else if(instance is UserLogin<AdminUser>)
                {
                    //Console.WriteLine("Admin");
                    AdminUser user = Cache.UserList.Admin_UserList.findUser(userId);
                    if (user is not null &&
                        user.Login(userId, password))
                {
                    UserLogin<AdminUser>.currentUser = user;
                    return true;
                }
                Console.WriteLine("Incorrect Credentials");

                }
            } else Console.WriteLine("Already Logged In");
            return false;
        }
        public static UserLogin<T> getInstance()
        {
            if (instance != null)
            {

                instance = new UserLogin<T>();

            };
            return instance;
        }
        public string print()
        {
            return "\nLogged In\n";
        }

    }
}

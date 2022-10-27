using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{
    public class Cache
    {
        public static defaultUserList UserList = new defaultUserList();
        public static User_List<AdminUser> defaultAdminList = UserList.Admin_UserList;
        public static User_List<EmployeeUser> defaultEmpList = UserList.Employee_UserList;
        //public static AdminUser admin;
        //public static EmployeeUser emp;
        public static dynamic currentUser;

        public Cache()
        {
            try
            {
            Cache.UserList = GetDefaultUserList();
            Cache.defaultEmpList = Cache.UserList.Employee_UserList;
            Cache.defaultAdminList = Cache.UserList.Admin_UserList;
            
            }
            catch (Exception ex)
            {
                Xml<defaultUserList>.Serialize(Cache.UserList);

            }
        }
        defaultUserList GetDefaultUserList()
        {
            return Xml<defaultUserList>.Deserialize(UserList);
        }
        public
            void DisplayUserLists()
        {
            UserList.Admin_UserList.displayUsers();
            UserList.Employee_UserList.displayUsers();

        }
        public
          // void
          AdminUser
            AdminLog(int userId, string password)
        {
            UserLogin<AdminUser> user = UserLogin<AdminUser>.getInstance();
            //Console.WriteLine(currentUser);
            if (currentUser == null)
            {
                if (UserLogin<AdminUser>.getLoggedIn(userId, password))
                {
                    currentUser = UserLogin<AdminUser>.currentUser;
                    return currentUser;
                }
                return null;
            } else Console.WriteLine("Already Logged In As User");
            return null;

        }
        public EmployeeUser UserLog(int userId, string password)
        {
            UserLogin<EmployeeUser> user = UserLogin<EmployeeUser>.getInstance();
            if (currentUser == null)
            {
                if (UserLogin<EmployeeUser>.getLoggedIn(userId, password))
                {
                    currentUser = UserLogin<EmployeeUser>.currentUser;
                    return currentUser;
                }
                return null;
            }
            Console.WriteLine("Already Logged In As Admin");
            return null;
        }
        public void Logout()
        {
            if(currentUser != null && currentUser is EmployeeUser)
            {
                UserLogin<EmployeeUser> user = UserLogin<EmployeeUser>.getInstance();
            
                UserLogin<EmployeeUser>.getLoggedOut(currentUser);
                currentUser = null;
                //admin = null;
                //emp = null;
            }
            else if (currentUser != null && currentUser is AdminUser)
            {
                UserLogin<AdminUser> user = UserLogin<AdminUser>.getInstance();
                UserLogin<AdminUser>.getLoggedOut(currentUser);
                currentUser = null;
                //emp = null;
                //admin = null;   

            }

        }
        
    }

}

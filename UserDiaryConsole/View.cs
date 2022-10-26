using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{
    public class View
    {
        public static List<Diary_List> defaultDiaryList = new List<Diary_List>();
        public static defaultUserList UserList = new defaultUserList();
        public static User_List<AdminUser> defaultAdminList = UserList.Admin_UserList;
        public static User_List<EmployeeUser> defaultEmpList = UserList.Employee_UserList;
        public static AdminUser admin;
        public static EmployeeUser emp;
        
        public View()
        {
            try
            {
                //Console.WriteLine("Boot");
            View.UserList = GetDefaultUserList();
            View.defaultEmpList = View.UserList.Employee_UserList;
            View.defaultAdminList = View.UserList.Admin_UserList;
            }
            catch (Exception ex)
            {
                Xml<defaultUserList>.Serialize(View.UserList);

            }
        }
        defaultUserList GetDefaultUserList()
        {
            return Xml<defaultUserList>.Deserialize(UserList);
        }
        public static
            void DisplayUserLists()
        {
            UserList.Admin_UserList.displayUsers();
            UserList.Employee_UserList.displayUsers();

        }
        public static
          // void
          AdminUser
            AdminLog(int userId, string password)
        {
            AdminLogin user = AdminLogin.getInstance();
            Console.WriteLine(emp);
            if (emp == null)
            {
                if (AdminLogin.getLoggedIn(userId, password))
                {
                    admin = AdminLogin.currentUser;
                    return admin;
                }
                return null;
            }
            Console.WriteLine("Already Logged In As User");
            return null;

        }
        public static EmployeeUser UserLog(int userId, string password)
        {
            UserLogin user = UserLogin.getInstance();
            if (admin == null)
            {
                if (UserLogin.getLoggedIn(userId, password))
                {
                    emp = UserLogin.currentUser;
                    return emp;
                }
                return null;
            }
            Console.WriteLine("Already Logged In As Admin");
            return null;
        }
        public static void Logout()
        {
            UserLogin user = UserLogin.getInstance();
            if(emp != null)
            {
                admin = null;
                emp = null;
                UserLogin.getLoggedOut();
            }
            else if (admin != null)
            {
                emp = null;
                admin = null;   
             AdminLogin.getLoggedOut();

            }

        }
    }

}

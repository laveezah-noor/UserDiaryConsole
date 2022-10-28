using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{
    public class Cache
    {
        public static List<Diary_List> defaultDiaryList = new List<Diary_List>();
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
            Cache.defaultDiaryList = GetDefaultDiaryList();
            
            
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
        List<Diary_List> GetDefaultDiaryList()
        {
            for (int i = 0; i < Cache.defaultEmpList.users.Count; i++)
            {
                var item = Cache.defaultEmpList.users[i];
                if (item.userDiaries is not null)
                {
                    defaultDiaryList.Add(item.userDiaries);
                }
            }
            return defaultDiaryList;
        }
        public
            void DisplayUserLists()
        {
            UserList.Admin_UserList.displayUsers();
            UserList.Employee_UserList.displayUsers();

        }
        // Can only show the user Diary with id and count not the stuff in diaries have fix it.
        public void DisplayDiaryList()
        {
            Console.WriteLine($"\nDiary List Count: {defaultDiaryList.Count} out of {defaultEmpList.users.Count} \n");
            
            foreach(var item in Cache.defaultDiaryList)
            {

                Console.WriteLine($"UserId: {item.user}, UserDiariesCount: {item.diaryCount()}");
            }
        }
        //public
        //  // void
        //  AdminUser
        //    AdminLog(int userId, string password)
        //{
        //    UserLogin<AdminUser> user = UserLogin<AdminUser>.getInstance();
        //    //Console.WriteLine(currentUser);
        //    if (currentUser == null)
        //    {
        //        if (UserLogin<AdminUser>.getLoggedIn(userId, password))
        //        {
        //            currentUser = UserLogin<AdminUser>.currentUser;
        //            return currentUser;
        //        }
        //        return null;
        //    } else Console.WriteLine("Already Logged In As User");
        //    return null;

        //}
        //public EmployeeUser UserLog(int userId, string password)
        //{
        //    UserLogin<EmployeeUser> user = UserLogin<EmployeeUser>.getInstance();
        //    if (currentUser == null)
        //    {
        //        if (UserLogin<EmployeeUser>.getLoggedIn(userId, password))
        //        {
        //            currentUser = UserLogin<EmployeeUser>.currentUser;
        //            return currentUser;
        //        }
        //        return null;
        //    }
        //    Console.WriteLine("Already Logged In As Admin");
        //    return null;
        //}

        public dynamic UserLog(int userId, string password)
        {
            //UserLogin<EmployeeUser> user = UserLogin<EmployeeUser>.getInstance();
            if (currentUser == null)
            {
                if (UserLogin.getLoggedIn(userId, password))
                {
                    currentUser = UserLogin.currentUser;
                    return currentUser;
                }
                return null;
            }
            Console.WriteLine("Already Logged In");
            return null;
        }
        public void Logout()
        {
            if(currentUser != null && currentUser is EmployeeUser)
            {
                UserLogin user = UserLogin.getInstance();
            
                UserLogin.getLoggedOut(currentUser);
                currentUser = null;
                //admin = null;
                //emp = null;
            }
            else if (currentUser != null && currentUser is AdminUser)
            {
                UserLogin user = UserLogin.getInstance();
                UserLogin.getLoggedOut(currentUser);
                currentUser = null;
                //emp = null;
                //admin = null;   

            }

        }
        
    }

}

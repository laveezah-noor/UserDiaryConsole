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
        public static dynamic? currentUser;

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

        // Gets the UserList from the Xml
        defaultUserList GetDefaultUserList()
        {
            return Xml<defaultUserList>.Deserialize(UserList);
        }
        
        // Gets the DiaryList from the UserList
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

        // Displays the UserLists
        public void DisplayUserLists()
        {
            UserList.Admin_UserList.displayUsers();
            UserList.Employee_UserList.displayUsers();

        }

        // Displays User Diaries with id and count
        public void DisplayDiaryList()
        {
            Console.WriteLine($"\nDiary List Count: {defaultDiaryList.Count} out of {defaultEmpList.users.Count} \n");
            
            foreach(var item in Cache.defaultDiaryList)
            {

                Console.WriteLine($"UserId: {item.user}, UserDiariesCount: {item.diaryCount()}");
            }
        }

        // To Login
        public dynamic UserLog(int userId, string password)
        {
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

        // To Logout
        public void Logout()
        {
            if(currentUser != null && currentUser is EmployeeUser)
            {
                UserLogin user = UserLogin.getInstance();
            
                UserLogin.getLoggedOut(currentUser);
                currentUser = null;
            }
            else if (currentUser != null && currentUser is AdminUser)
            {
                UserLogin user = UserLogin.getInstance();
                UserLogin.getLoggedOut(currentUser);
                currentUser = null;
            }

        }
        
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{
    public class Cache
    {
        static Cache instance;
        public List<string> UsernameList;
        public List<Diary_List> defaultDiaryList;
        public defaultUserList UserList = new defaultUserList();

        public List<User> defaultAdminList;
        public List<User> defaultEmpList = new List<User>();
        
        public User currentUser;

        public static Cache getCache()
        {
            if (instance == null)
            {
                instance = new Cache();
            };
            return instance;
        }

        Cache()
        {
            UsernameList = new List<string>();
            defaultDiaryList = new List<Diary_List>();

            UserList = Xml<defaultUserList>.Deserialize(UserList);
            if (UserList == null)
            {
                UserList = new defaultUserList();
                User admin = new User("admin", "Admin", "admin","admin","active", "", "");

                Xml<defaultUserList>.Serialize(UserList);

            }
            defaultEmpList = GetEmpList();
            defaultAdminList = GetAdminList();
            defaultDiaryList = GetDefaultDiaryList();
            UsernameList = GetUsernameList();
            //Console.WriteLine(defaultDiaryList is not null);
        }
        List<User> GetEmpList()
        {
            for (int i = 0; i < UserList.UsersList.Count; i++)
            {
                var item = UserList.UsersList[i];
                if (item.Type == Types.user.ToString())
                {
                    defaultEmpList.Add(item);
                }
            }
            return defaultEmpList;
        }
        public List<User> GetAdminList()
        {
            defaultAdminList = new List<User>();
            for (int i = 0; i < UserList.UsersList.Count; i++)
            {
                var item = UserList.UsersList[i];
                if (item.Type == Types.admin.ToString())
                {
                    defaultAdminList.Add(item);
                }
            }
            return defaultAdminList;
        }

        // Gets the DiaryList from the UserList
        List<Diary_List> GetDefaultDiaryList()
        {
            for (int i = 0; i < UserList.UsersList.Count; i++)
            {
                var item = UserList.UsersList[i];
                if (item.userDiaries is not null)
                {
                    defaultDiaryList.Add(item.userDiaries);
                }
            }
            //for (int i = 0; i < Cache.defaultEmpList.users.Count; i++)
            //{
            //    var item = Cache.defaultEmpList.users[i];
            //    if (item.userDiaries is not null)
            //    {
            //        defaultDiaryList.Add(item.userDiaries);
            //    }
            //}
            return defaultDiaryList;
        }
        public List<string> GetUsernameList()
        {
            for (int i = 0; i < UserList.UsersList.Count; i++)
            {
                var item = UserList.UsersList[i];
                UsernameList.Add(item.UserName);

            }
            return UsernameList;
        }
        // Displays the UserLists
        public void DisplayUserLists()
        {
            //UserList.Admin_UserList.displayUsers();
            //UserList.Employee_UserList.displayUsers();

        }

        // Displays User Diaries with id and count
        public void DisplayDiaryList()
        {
            Console.WriteLine($"\nDiary List Count: {defaultDiaryList.Count} out of {defaultEmpList.Count} \n");

            foreach (var item in Cache.getCache().defaultDiaryList)
            {

                Console.WriteLine($"UserId: {item.user}, UserDiariesCount: {item.diaryCount()}");
            }
        }

        // To Login
        public dynamic UserLog(string username, string password)
        {

            if (currentUser == null)
            {
                User user = UserList.findUser(username);
                if (user is not null)
                {
                    if (user.Login(username, password))
                    {
                        currentUser = user;

                        return currentUser;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nIncorrect Credentials\n");
                        return null;
                    }
                }
                //dynamic user = Cache.UserList.Employee_UserList.findUser(username);
                //if (user is not null)
                //{
                //    if (user.Login(username, password))
                //    {
                //        currentUser = user;

                //        return currentUser;
                //    }
                //    else
                //    {
                //        Console.Clear();
                //        Console.WriteLine("\nIncorrect Credentials\n");
                //        return false;
                //    }
                //    user = currentUser;
                //}
                //user = Cache.UserList.Admin_UserList.findUser(username);
                //if (user is not null)
                //{
                //    if (user.Login(username, password))
                //    {
                //        currentUser = user;
                //        return currentUser;
                //    }
                //    else
                //    {
                //        Console.Clear();
                //        Console.WriteLine("\nIncorrect Credentials\n");
                //        return false;
                //    }
                //}
                Console.Clear();
                Console.WriteLine("\nIncorrect Credentials\n");


            }
            else Console.WriteLine("Already Logged In");
            return currentUser;
            //if (UserLogin.getLoggedIn(username, password))
            //{
            //    currentUser = UserLogin.currentUser;
            //    return currentUser;
            //}
            //return currentUser;
            //}
            //Console.WriteLine("Already Logged In");
            //    return null;
        }

        // To Logout
        public void Logout()
        {
            if (currentUser != null)
            {
                currentUser.Logout();
                currentUser = null;
            }

        }

        //To update the XML DiaryList
        public void UpdateDiaryList()
        {
            UpdateUserList();
        }
        //To update the XML UserList
        public void UpdateUserList()
        {
            Xml<defaultUserList>.Serialize(Cache.getCache().UserList);

        }

    }

}

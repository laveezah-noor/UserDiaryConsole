using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{
    public class Cache
    {
        static Cache? instance;
        public List<string> UsernameList;
        public List<Diary_List> defaultDiaryList;
        public defaultUserList UserList;

        public List<User> defaultAdminList;
        
        public User? currentUser;

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
            UserList =  new defaultUserList();
            UsernameList = new List<string>();
            defaultDiaryList = new List<Diary_List>();

            UserList = Xml<defaultUserList>.Deserialize(UserList);
            if (UserList == null)
            {
                UserList = new defaultUserList();
                Xml<defaultUserList>.Serialize(UserList);
                User admin = new User("admin", "Admin", "admin","admin","active", "", "");
                UserList.addUser(admin);

            }
            //defaultEmpList = GetEmpList();
            defaultAdminList = GetAdminList();
            defaultDiaryList = GetDefaultDiaryList();
            UsernameList = GetUsernameList();
            
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
        

        // Displays User Diaries with id and count
        public void DisplayDiaryList()
        {
            Console.WriteLine($"\nDiary List Count: {defaultDiaryList.Count} out of {UserList.Count} \n");

            foreach (var item in Cache.getCache().defaultDiaryList)
            {

                Console.WriteLine($"UserId: {item.user}, UserDiariesCount: {item.diaryCount()}");
            }
        }
        
        public void Register(string name, string username, string passcode, string email, string phone)
        {
            User emp = new User(username, name, passcode, Types.user.ToString(), Statuses.pending.ToString(), phone, email);
            UserList.addUser(emp);
            Console.Clear();
            emp.display();

            Console.WriteLine($"\nAccount Created!\n");

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
                        UpdateUserList();
                        return currentUser;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nIncorrect Credentials\n");
                        return null;
                    }
                }
                Console.Clear();
                Console.WriteLine("\nIncorrect Credentials\n");


            }
            else Console.WriteLine("Already Logged In");
            return currentUser;
        }

        // To Logout
        public void Logout()
        {
            if (currentUser != null)
            {
                currentUser.Logout();
                UpdateUserList();
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

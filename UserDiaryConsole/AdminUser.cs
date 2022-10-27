using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserDiaryConsole
{
    [XmlRoot("Admin")]
    public class AdminUser : User
    {
        AdminUser user;
        public static List<Diary_List> defaultDiaryList = new List<Diary_List>();
        public static User_List<AdminUser> defaultAdminList = Cache.defaultAdminList;
        public static User_List<EmployeeUser> defaultEmpList = Cache.defaultEmpList;
        public static defaultUserList defaultUserList = Cache.UserList;

        public static void Register(
            string Name,
            string Password,
            string phone,
            string email
            )
        {
            AdminUser user = new AdminUser();
            user.create(Name, Password, Types.admin.ToString(), Statuses.active.ToString());
            user.UpdateEmail(email);
            user.UpdatePhone(phone);
            Cache.defaultAdminList.addUser(user);
            //DisplayUserLists();
            user.UpdateUserList();
            user.display();
            //return this.user;
        }

        //To add DiaryList of a specific user inside the default diary list
        public void CreateDiaryList(Diary_List userDiary)
        {
            if (this.LogStatus)
            {
                defaultDiaryList.Add(userDiary);
            } else Console.WriteLine("Logged Out");
        }

        //To delete the DiaryList of a specific user inside the default diary list
        public void DeleteDiaryList(int user)
        {
            if (this.LogStatus)
            {

                int itemIndex = FindDiaryList(user);
                if (itemIndex != 0)
                {
                    defaultDiaryList.RemoveAt(itemIndex);
                }
            }
            Console.WriteLine("Logged Out");
        }

        public int FindDiaryList(int user)
        {
            if (this.LogStatus)
            {
                foreach (var item in defaultDiaryList)
                {
                    if (item.user == user)
                    {
                        Console.WriteLine("\nItem Found!\n");
                        item.displayDiaries();
                        return defaultDiaryList.IndexOf(item);
                    }
                }
                Console.WriteLine("Not Found!");
            }
            Console.WriteLine("Logged Out");
            return 0;
        }

        //To Display the Diaries in the App. Cannot access the data of user diaries HAVE TO REMOVE IT
        //Can only see the count of the diaries
        public void DisplayDiaryLists()
        {
            if (this.LogStatus)
            {
                Console.WriteLine("Diary List Count: " + defaultDiaryList.Count + "\n");
                foreach (var item in defaultDiaryList)
                {
                    Console.WriteLine("UserId: " + item.user);
                    item.displayDiaries();
                }

            }
            Console.WriteLine("Logged Out");
        }

        //To  Create the users from the admin portal
        public void CreateUser
            (
            string Name,
            string Password)
        {
            if (this.LogStatus)
            {
                EmployeeUser newUser = new EmployeeUser(true);
                newUser.create(Name, Password, Types.user.ToString(), Statuses.active.ToString());
                defaultEmpList.addUser(newUser);
                CreateDiaryList(newUser.userDiaries);
                UpdateUserList();
                newUser.display();
            } else Console.WriteLine("Not Logged in");
            //return null;
        }
        //To Delete users from the admin portal
        public void DeleteUser(int userId)
        {
            if (this.LogStatus)
            {
                defaultEmpList.deleteUser(userId);
                UpdateUserList();
            }
            Console.WriteLine("Logged Out");
        }

        //To Delete admin from the admin portal
        public void DeleteAdmin(int userId)
        {
            if (this.LogStatus)
            {
                defaultAdminList.deleteUser(userId);
                UpdateUserList();
            }
            Console.WriteLine("Logged Out");
        }

        //For Implementing search bars
        public void FindUser(int userId)
        {
            if (this.LogStatus)
            {
                EmployeeUser emp = defaultEmpList.findUser(userId);
                emp.display();
            }
            Console.WriteLine("Logged Out");
        }

        //Authorizes the user to use Diaries
        public void AuthorizeUser(int userId)
        {
            if (this.LogStatus)
            {
                EmployeeUser emp = defaultEmpList.findUser(userId);
                emp.display();
                emp.Authorize();
                CreateDiaryList(emp.userDiaries);
                UpdateDiaryList();

            }
        }
        public
            void DisplayUserLists()
        {
            //if (LogStatus)

            defaultEmpList.displayUsers();

        }
        public
            void DisplayAdminLists()
        {
            //if (LogStatus)

            defaultAdminList.displayUsers();

        }
    }

}

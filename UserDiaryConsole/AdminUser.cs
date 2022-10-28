using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserDiaryConsole
{
    [XmlRoot("Admin")]
    public class AdminUser : User
    {
        AdminUser user;
        public static List<Diary_List> defaultDiaryList = Cache.defaultDiaryList;

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
        //Can also add the functionality to unauthorize the user but keep the diaries
        public void DeleteDiaryList(int user)
        {
            if (this.LogStatus)
            {
                EmployeeUser emp = FindUser(user);
                int itemIndex = FindDiaryList(user);
                if (itemIndex != 0)
                {
                    defaultDiaryList.RemoveAt(itemIndex);
                    emp.UpdateStatus(Statuses.pending.ToString());
                    UpdateDiaryList();
                }
            }
        }

        // Masla h
        public int FindDiaryList(int user)
        {
            if (this.LogStatus)
            {
                foreach (var item in defaultDiaryList)
                {
                    if (item.user == user)
                    {
                        Console.WriteLine("\nItem Found!\n");
                        Console.WriteLine($"Diary Count: {item.diaryCount()}");
                        return defaultDiaryList.IndexOf(item);
                    }
                }
                Console.WriteLine("Not Found!");
                return 0;
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
                Console.WriteLine($"\nDiary List Count: {defaultDiaryList.Count} out of {defaultEmpList.users.Count} \n");
                foreach (var item in defaultDiaryList)
                {
                    Console.WriteLine($"UserId: {item.user}, UserDiariesCount: {item.diaryCount()}");
                    //item.displayDiaries();
                }

            } else Console.WriteLine("Logged Out");
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
        }

        //To Delete admin from the admin portal
        public void DeleteAdmin(int userId)
        {
            if (this.LogStatus)
            {
                defaultAdminList.deleteUser(userId);
                UpdateUserList();
            }
        }

        //For Implementing search bars
        public EmployeeUser FindUser(int userId)
        {
            if (this.LogStatus)
            {
                EmployeeUser emp = defaultEmpList.findUser(userId);
                emp.display();
                return emp;
            }
            return null;
        }

        //Authorizes the user to use Diaries
        public void AuthorizeUser(int userId)
        {
            if (this.LogStatus)
            {
                EmployeeUser emp = FindUser(userId);
                if (this.FindDiaryList(userId) is 0) { 
                emp.userDiaries = new Diary_List(emp.Id);
                CreateDiaryList(emp.userDiaries);
                }
                emp.UpdateStatus(Statuses.active.ToString());
                UpdateDiaryList();
                emp.display();

            }
        }

        //Have to fix it Admin cannot see the password of the users
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

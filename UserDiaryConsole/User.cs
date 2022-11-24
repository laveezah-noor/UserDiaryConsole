using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserDiaryConsole
{

    enum Types
    {
        admin,
        user
    }

    enum Statuses
    {
        active,
        pending
    }

    public class User
    {
        static int count = Cache.getCache().UserList.id;

        [XmlElement("ID")]
        public int Id;
        [XmlElement("Username")]
        public string UserName { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        public string Password { get; set; }
        [XmlElement("Phone")]
        public string phone { get; set; }
        [XmlElement("Email")]
        public string email { get; set; }
        public string Type;
        public string Status { get; set; }
        public bool LogStatus { get; set; }
        [XmlElement("Diaries")]
        public Diary_List userDiaries;

        public User() { }
        public User(
            string UserName, 
            string Name,
            string Password,
            string Type,
            string Status,
            string Phone,
            string Email
            )
        {
            count++;
            this.Id = count;
            Cache.getCache().UserList.id = count;
            this.UserName = UserName;
            this.Name = Name;
            this.Password = Password;
            this.Type = Type;
            this.phone = Phone;
            this.email = Email;
            this.Status = Status;
            this.LogStatus = false;
            
        }

        public bool Login (string username, string password)
        {
            if (this.UserName == username && this.Password == password)
            {
                Console.WriteLine($"{this.Name} Logged In ");
                this.LogStatus = true;
               return true;
            }
            return false;
        }

        public void Logout()
        {
            this.LogStatus = false;
            Console.WriteLine("Logged Out");
        }

        public void UpdateUser(string Name, string Password, string Phone, string Email)
        {
            if (Name != "" || Password != "" || Phone != "" || Email != "" && this.LogStatus)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Name != "")
                    {
                        this.Name = Name;
                        Name = "";
                        Console.WriteLine(Name);
                    }
                    else if (Password != "")
                    {
                        this.Password = Password;
                        Password = "";
                        Console.WriteLine(Password);
                    }
                    else if (Phone != "")
                    {
                        this.phone = Phone;
                        Phone = "";
                        Console.WriteLine(Phone);
                    }
                    else if (Email != "")
                    {
                        this.email = Email;
                        Email = "";
                        Console.WriteLine(Email);
                    }
                }
                Cache.getCache().UpdateUserList();
                Console.Clear();
                Console.WriteLine("\nProfile Updated!\n");
                this.display();
            }
            else Console.Clear(); Console.WriteLine("\nNothing to Update!\n");
        }
        
        public void UpdateStatus(string input)
        {
            if (input != null)
            {
                this.Status = input;
            }
        }
        

        public void display()
        {
            Console.WriteLine("\n" +
                        $"ID: {this.Id}\n" +
                        $"Username: {this.UserName}\n" +
                        $"Name: {this.Name}\n" +
                        $"Password: {this.Password}\n" +    
                        $"Type: {this.Type}\n" +
                        $"Status: {this.Status}\n" +
                        $"Phone: {this.phone}\n" +
                        $"Email: {this.email}\n" +
                        $"Authorize: {this.userDiaries is not null}\n");

        }
        //To Create a New Diary
        public void CreateDiary(string name, string content)
        {
            if (this.LogStatus)
            {
                if (this.userDiaries is not null && this.Status == Statuses.active.ToString())
                {
                    this.userDiaries.addDiary(name, content);
                    Cache.getCache().UpdateDiaryList();
                }
                else
                {
                    Console.WriteLine($"\nNo diary space for {this.Name} contact your admin\n");

                }

            }
            else Console.WriteLine("Logged out");
        }

        //To delete a diary
        public void DeleteDiary(int diaryID)
        {
            if (this.LogStatus)
            {
                if (this.userDiaries.deleteDiary(diaryID))
                {
                    Cache.getCache().UpdateDiaryList();
                    Console.Clear();
                    Console.WriteLine("\nDiary Deleted!");

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nDiary Not Found!");

                }

            }
            else
                Console.WriteLine("Logged out");
        }

        //To update a diary
        public void UpdateDiary(int diaryId, string Name, string Content)
        {
            if (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Content))
            {
                if (this.userDiaries.UpdateDiary(diaryId, Name, Content))
                {
                    Cache.getCache().UpdateDiaryList();
                    Diary diary = this.userDiaries.FindDiary(diaryId);
                    Console.Clear();
                    Console.WriteLine(diary.display());
                    Console.WriteLine("Diary Updated!");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nDiary Not Found!");
                }

            }
            else { 
                Console.Clear(); Console.WriteLine("\nNothing to Update!");
            
            }
        }

        //To find a diary
        public bool FindDiary(int diaryID)
        {
            if (this.userDiaries.FindDiary(diaryID) is not null)
            {
                return true;
            }
            else return false;
        }

        //To display the diaries
        public void DisplayDiaries()
        {
            if (this.LogStatus)
            {
                if (this.userDiaries is not null)
                {
                    this.userDiaries.displayDiaries();
                }
                else
                {
                    Console.WriteLine($"No Diary Space for {this.Name}");
                }

            }
            else Console.WriteLine("Logged out");
        }


        //Admin Functions


        //To add DiaryList of a specific user inside the default diary list
        public void CreateDiaryList(User user)
        {
            if (this.Type == Types.admin.ToString())
            {
                user.userDiaries = new Diary_List(user.Id);
                Cache.getCache().defaultDiaryList.Add(user.userDiaries);
                Cache.getCache().UpdateDiaryList();
            }
        }

        public void DeleteDiaryList(User emp, int itemIndex)
        {
            if (this.Type == Types.admin.ToString())
            {
                Cache.getCache().defaultDiaryList.RemoveAt(itemIndex);
                emp.userDiaries = null;
                Cache.getCache().UpdateDiaryList();

            }
        }

        //Authorizes the user to use Diaries
        public void AuthorizeUser(int userId)
        {
            if (this.Type == Types.admin.ToString())
            {
                if (FindUser(userId) is null) { Console.Clear(); Console.WriteLine("User is not available"); }
                else
                {
                    User emp = FindUser(userId);

                    if (this.FindDiaryList(userId) is -1 && emp.Status != "active")
                    {
                        emp.UpdateStatus(Statuses.active.ToString());
                        CreateDiaryList(emp);
                        Console.Clear();
                        Console.WriteLine("\nUser Authorized\n");
                        emp.display();
                    }
                    else { Console.Clear(); Console.WriteLine("Given User is already authorized!"); }

                }

            }
        }

        //To delete the DiaryList of a specific user inside the default diary list
        //Can also add the functionality to unauthorize the user but keep the diaries
        public void Unauthorize(int user)
        {
            if (this.Type == Types.admin.ToString())
            {
                User emp = FindUser(user);
                int itemIndex = FindDiaryList(user);
                if (itemIndex != -1)
                {
                    emp.UpdateStatus(Statuses.pending.ToString());
                    DeleteDiaryList(emp, itemIndex);
                    Console.Clear();

                    Console.WriteLine("\nUser Unauthorized\n");
                    emp.display();
                }
                else if (emp is null) { Console.Clear(); Console.WriteLine("User is not available"); }
                else
                {
                    Console.Clear();
                    Console.WriteLine("User is already unauthorized!");
                }
            }
        }

        // To Find the Diary List of the user
        public int FindDiaryList(int user)
        {
            if (this.Type == Types.admin.ToString())
            {
                foreach (var item in Cache.getCache().defaultDiaryList)
                {
                    if (item.user == user)
                    {
                        Console.Clear();
                        Console.WriteLine("\nDiary Found!\n");
                        Console.WriteLine($"Diary Count: {item.diaryCount()}");
                        return Cache.getCache().defaultDiaryList.IndexOf(item);
                    }
                }
                Console.Clear();
                Console.WriteLine("\nUser Diary Not Found!");
                return -1;
            }
            return -1;
        }

        //To Display the Diaries in the App.
        //Can only see the count of the diaries
        public void DisplayDiaryLists()
        {
            if (this.Type == Types.admin.ToString())
            {
                Console.WriteLine($"\nDiary List Count: {Cache.getCache().defaultDiaryList.Count} out of {Cache.getCache().UserList.UsersList.Count} \n");
                foreach (var item in Cache.getCache().defaultDiaryList)
                {
                    Console.WriteLine($"UserId: {item.user}, UserDiariesCount: {item.diaryCount()}");
                }

            }
        }

        //To  Create the users from the admin portal
        public void CreateUser
            (
            string Type,
            string UserName,
            string Name,
            string Password)
        {
            if (this.Type == Types.admin.ToString())
            {
                User newUser;
                if (Type == Types.user.ToString())
                {
                    newUser = new User(UserName, Name, Password, Types.user.ToString(), Statuses.active.ToString(), "", "");
                    CreateDiaryList(newUser);
                    Cache.getCache().UserList.addUser(newUser);
                    Console.Clear();
                    newUser.display();

                }
                else if (Type == Types.admin.ToString())
                {
                    newUser = new User(UserName, Name, Password, Types.admin.ToString(), Statuses.active.ToString(), "", "");
                    CreateDiaryList(newUser);
                    Cache.getCache().UserList.addUser(newUser);
                    Console.Clear();
                    newUser.display();
                }

            }
        }

        //public void CreateUser
        //    (
        //    string UserName,
        //    string Name,
        //    string Password)
        //{
        //    if (this.Type == Types.admin.ToString())
        //    {
        //        User newUser = new User(UserName, Name, Password, Types.user.ToString(), Statuses.active.ToString(), "", "");
                
        //        //Cache.UserList.addUser(newUser);
        //        CreateDiaryList(newUser.userDiaries);
        //       Cache.getCache().UpdateUserList();
        //        Console.Clear();
        //        newUser.display();
        //    }
        //}

        ////To  Create the users from the admin portal
        //public void CreateAdmin
        //    (
        //    string UserName,
        //    string Name,
        //    string Password)
        //{
        //    if (this.Type == Types.admin.ToString())
        //    {
        //        User newUser = new User(UserName, Name, Password, Types.admin.ToString(), Statuses.active.ToString(), "","");
        //        Console.Clear();
        //        newUser.display();
        //    }

        //}

        //To Delete users from the admin portal
        public void DeleteUser(int userId)
        {
            if (this.Type == Types.admin.ToString())
            {
                if (Cache.getCache().UserList.deleteUser(userId))
                {
                    Console.Clear();
                    Console.WriteLine("User Deleted!");
                }
            }
        }

        //To Delete admin from the admin portal
        public void DeleteAdmin(int userId)
        {
            if (this.Type == Types.admin.ToString())
            {
                if (this.Id != userId && userId != 1)
                {
                    if (Cache.getCache().UserList.deleteUser(userId))
                    {
                        Console.Clear();
                        Console.WriteLine("Admin Removed Successfully!");

                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nYou cannot remove yourself or main admin");
                }
            }
        }

        //For Implementing search bars, searches the user
        public User FindUser(int userId)
        {
            if (this.Type == Types.admin.ToString())
            {
                User emp = Cache.getCache().UserList.findUser(userId);
                if (emp is not null) emp.display();
                return emp;
            }
            return null;
        }
        public User FindUser(string userId)
        {
            if (this.Type == Types.admin.ToString())
            {
                User emp = Cache.getCache().UserList.findUser(userId);
                if (emp is not null) emp.display();
                return emp;
            }
            return null;
        }
        
        //To Display User List
        public void DisplayUserLists()
        {
            if (this.Type == Types.admin.ToString())
                Cache.getCache().UserList.displayUsers();
        }

        //To Display Admin List
        public void DisplayAdminLists()
        {
            if (this.Type == Types.admin.ToString())
            Cache.getCache().UserList.displayUsers(Cache.getCache().defaultAdminList);
        }
    }

}

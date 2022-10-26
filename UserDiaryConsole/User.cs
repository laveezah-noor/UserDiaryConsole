using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        pending,
        deleted
    }

    public abstract class User
    {
        static int count = View.defaultEmpList.users.Count + View.defaultAdminList.users.Count;
        
        public int Id;
        public string Name { get; set; }
        public string Password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string Type;
        public string Status { get; set; }
        public bool LogStatus { get; set; }

        public User()
        {
            count++;
            this.Id = count;
            //Console.WriteLine("Count: "+count);
        }
        public void create
            (
            string Name,
            string Password,
            string Type,
            string Status
            )
        {
            this.Name = Name;
            this.Password = Password;
            this.Status = Status;
            this.Type = Type;
            this.LogStatus = false;
        }
        public bool Login
            (int userId, string password
            )
        {
            if (this.Id == userId && this.Password == password)
            {

                Console.WriteLine($"{this.Name} Logged In ");
                this.LogStatus = true;
                UpdateUserList();
                return true;
            }
            Console.WriteLine("Incorrect");
            return false;
        }
        public void Logout()
        {
            this.LogStatus = false;
            UpdateUserList();
            Console.WriteLine("Logged Out");
        }
        public void UpdateUser(int userId, string Name, string Password, string Phone, string Email)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Name != "" || Password != "" || Phone != "" || Email != "" && this.LogStatus)
                {
                    // Console.WriteLine(i)

                    if (Name != "")
                    {
                        this.UpdateName(Name);
                        Name = "";
                        Console.WriteLine(Name);
                        this.display();
                    }
                    else if (Password != "")
                    {
                        this.UpdatePassword(Password);
                        Password = "";
                        Console.WriteLine(Password);
                        this.display();
                    }
                    else if (Phone != "")
                    {
                        this.UpdatePhone(Phone);
                        Phone = "";
                        Console.WriteLine(Phone);
                        this.display();
                    }
                    else if (Email != "")
                    {
                        this.UpdateEmail(Email);
                        Email = "";
                        Console.WriteLine(Email);
                        this.display();
                    }
                }
            }
        }
        public void UpdateName(string input)
        {
            if (input != null && this.LogStatus != false)
            {
                this.Name = input;
            }
        }
        public void UpdatePassword(string input)
        {
            if (input != null && this.LogStatus != false)
            {
                this.Password = input;
            }
        }
        public void UpdateStatus(string input)
        {
            if (input != null && this.LogStatus)
            {
                this.Status = input;
            }
        }
        public void UpdatePhone(string input)
        {
            this.phone = input;
        }
        public void UpdateEmail(string input)
        {
            this.email = input;
        }
        public void display()
        {
            Console.WriteLine($"ID: {this.Id}\n" +
                        $"Name: {this.Name}\n" +
                        $"Password: {this.Password}\n" +
                        $"Type: {this.Type}\n" +
                        $"Status: {this.Status}\n" +
                        $"Phone: {this.phone}\n" +
                        $"Email: {this.email}\n");

        }
        //To update the XML DiaryList
        public void UpdateDiaryList()
        {
            UpdateUserList();
        }
        //To update the XML UserList
        public void UpdateUserList()
        {
            Xml<defaultUserList>.Serialize(View.UserList);
        }
    }

}

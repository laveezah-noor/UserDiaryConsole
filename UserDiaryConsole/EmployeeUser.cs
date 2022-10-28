using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserDiaryConsole
{

    [XmlRoot("User")]
    public class EmployeeUser : User
    {
        EmployeeUser user;
        public Diary_List userDiaries;

        public EmployeeUser() : base()
        {
        }

        public EmployeeUser(bool authorized) : base()
        {
            if (authorized)
            {
                this.userDiaries = new Diary_List(this.Id);
            }
        }

        //To Register the a new account
        public static void Register(
             string Name,
             string Password,
             string phone,
             string email
             )
        {
            EmployeeUser user = new EmployeeUser();
            user.create(Name, Password, Types.user.ToString(), Statuses.pending.ToString());
            user.UpdateEmail(email);
            user.UpdatePhone(phone);
            AdminUser.defaultEmpList.addUser(user);
            user.UpdateUserList();
            user.display();
        }

        //To Create a New Diary
        public void CreateDiary(string name, string content)
        {
            if (this.LogStatus)
            {
                if (this.userDiaries is not null && this.Status == Statuses.active.ToString())
                {
                    this.userDiaries.addDiary(name, content);
                    UpdateDiaryList();
                }
                else
                {
                    Console.WriteLine($"\nNo diary space for {this.Name} contact your admin\n");
                    
                }

            } else  Console.WriteLine("Logged out");
        }

        //To delete a diary
        public void DeleteDiary(int diaryID)
        {
            if (this.LogStatus)
            {
                this.userDiaries.deleteDiary(diaryID);
                Console.WriteLine("Item Deleted!");
                UpdateDiaryList();

            } else 
                Console.WriteLine("Logged out");
        }

        //To update a diary
        public void UpdateDiary(int diaryId, string Name, string Content)
        {
            if (this.LogStatus)
            {
                this.userDiaries.UpdateDiary(diaryId, Name, Content);
                UpdateDiaryList();

            } else Console.WriteLine("Logged out");
        }

        //To find a diary
        public void FindDiary(int diaryID)
        {
            if (this.LogStatus)
            {
                this.userDiaries.FindDiary(diaryID);
            } else Console.WriteLine("Logged out");
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

            }else Console.WriteLine("Logged out");
        }

    }

}

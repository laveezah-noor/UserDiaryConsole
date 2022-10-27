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
                //Console.WriteLine(this.Id);
                this.userDiaries = new Diary_List(this.Id);
            }
        }
        public void Authorize()
        {
            this.userDiaries = new Diary_List(this.Id);
        }

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
            //return this.user;
        }
        public void CreateDiary(string name, string content)
        {
            if (this.LogStatus)
            {
                //console.writeline(name, content);
                if (this.userDiaries is not null)
                {
                    this.userDiaries.addDiary(name, content);
                    UpdateDiaryList();
                }
                else
                {
                    Console.WriteLine($"No diary space for {this.Name} contact your admin");
                    
                }

            } else  Console.WriteLine("Logged out");
        }
        public void DeleteDiary(int diaryID)
        {
            if (this.LogStatus)
            {
                this.userDiaries.deleteDiary(diaryID);
                UpdateDiaryList();

            }
            Console.WriteLine("Logged out");
        }
        public void UpdateDiary(int diaryId, string Name, string Content)
        {
            if (this.LogStatus)
            {
                this.userDiaries.UpdateDiary(diaryId, Name, Content);
                UpdateDiaryList();

            }
            Console.WriteLine("Logged out");
        }

        public void FindDiary(int diaryID)
        {
            if (this.LogStatus)
            {
                this.userDiaries.FindDiary(diaryID);
            }
            Console.WriteLine("Logged out");
        }
        public void DisplayDiaries()
        {
            if (this.LogStatus)
            {
                if (this.userDiaries is not null)
                {
                    this.display();
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

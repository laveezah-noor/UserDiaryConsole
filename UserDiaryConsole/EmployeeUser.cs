//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Serialization;

//namespace UserDiaryConsole
//{

//    [XmlRoot("User")]
//    public class EmployeeUser : User
//    {
//        //EmployeeUser user;
//        public Diary_List userDiaries;

//        public EmployeeUser() { }
//        //public EmployeeUser(bool authorized) : base(
            
//        //    )
//        //{
//        //    if (authorized)
//        //    {
//        //        this.userDiaries = new Diary_List(this.Id);
//        //    }
//        //}

//        //To Register the a new account
//        public EmployeeUser(
//             string UserName,
//             string Name,
//             string Password,
//             string phone,
//             string email
//             ) : base(
//                 UserName,
//            Name,
//            Password,
//            Types.user.ToString(),
//            Statuses.pending.ToString(),
//            phone,
//            email)
//        {
//            Cache.defaultEmpList.addUser(this);
//            this.UpdateUserList();
//            Console.Clear();
//            this.display();
//        }

//        //To Create a New Diary
//        public void CreateDiary(string name, string content)
//        {
//            if (this.LogStatus)
//            {
//                if (this.userDiaries is not null && this.Status == Statuses.active.ToString())
//                {
//                    this.userDiaries.addDiary(name, content);
//                    UpdateDiaryList();
//                }
//                else
//                {
//                    Console.WriteLine($"\nNo diary space for {this.Name} contact your admin\n");
                    
//                }

//            } else  Console.WriteLine("Logged out");
//        }

//        //To delete a diary
//        public void DeleteDiary(int diaryID)
//        {
//            if (this.LogStatus)
//            {
//                if (this.userDiaries.deleteDiary(diaryID))
//                {
//                UpdateDiaryList();
//                    Console.Clear();
//                    Console.WriteLine("\nDiary Deleted!");

//                }
//                else
//                {
//                    Console.Clear();
//                    Console.WriteLine("\nDiary Not Found!");

//                }

//            } else 
//                Console.WriteLine("Logged out");
//        }

//        //To update a diary
//        public void UpdateDiary(int diaryId, string Name, string Content)
//        {
//            if (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Content))
//            {
//                if (this.userDiaries.UpdateDiary(diaryId, Name, Content))
//                { 
//                    UpdateDiaryList();
//                    Console.Clear();
//                    Console.WriteLine(this.userDiaries.diaries[diaryId].display());
//                    Console.WriteLine("Diary Updated!"); }
//                else
//                {
//                    Console.Clear();
//                    Console.WriteLine("\nDiary Not Found!");
//                }

//            }
//            else Console.Clear(); Console.WriteLine("\nNothing to Update!");
//        }

//        //To find a diary
//        public bool FindDiary(int diaryID)
//        {
//            if (this.userDiaries.FindDiary(diaryID) is not null) { 
//                return true;
//            } else return false;
//        }

//        //To display the diaries
//        public void DisplayDiaries()
//        {
//            if (this.LogStatus)
//            {
//                if (this.userDiaries is not null)
//                {
//                    this.userDiaries.displayDiaries();
//                }
//                else
//                {
//                    Console.WriteLine($"No Diary Space for {this.Name}");
//                }

//            }else Console.WriteLine("Logged out");
//        }

//    }

//}
    
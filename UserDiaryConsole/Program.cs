using System.ComponentModel;
using System.ComponentModel.Design;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using UserDiaryConsole;

class Program
{   


    public static void Main(string[] args)
    {
    
        View view = new View();
        //AdminUser adminDemoUser1 = (AdminUser)Register.AdminRegister("Noor", "noor", "", "");
        //AdminUser admin = View.AdminLog(2, "noor");
        //admin.CreateUser("Demo 2", "demo2");
        //View.Logout();
        //EmployeeUser user = View.UserLog(1, "demo1");
        //  AdminUser admin2 = View.AdminLog(2, "admin2");
        //  admin.display();
        //user.display();
        // 
        // admin2.display();
        //EmployeeUser demoUser1 = (EmployeeUser)Register.EmployeeRegister("Demo 1", "demo1", "", "");
        //adminDemoUser1.CreateUser();
        //adminDemoUser1.Login(adminDemoUser1.Id, adminDemoUser1.Password);
        //demoUser1.Login(demoUser1.Id, "abc");
        EmployeeUser demoUser2 = View.UserLog(3,"demo2");
        //demoUser1.Logout();
        AdminUser admin = View.AdminLog(2, "noor");
        
        //Console.WriteLine("\n====Profiles Display====\n");
        //adminDemoUser1.display();
        //demoUser1.display();
        demoUser2.display();
        //Console.WriteLine(demoUser1);
        //Console.WriteLine(demoUser1.userDiaries);
        //Console.WriteLine(demoUser2.userDiaries);
        //user.CreateDiary("name", "");
        //View.Logout();
        demoUser2.CreateDiary("Demo 2 Diary", "");
        //  demoUser1.DisplayDiaries();
        //  demoUser2.DisplayDiaries();
        //  adminDemoUser1.AuthorizeUser(demoUser1);
        //  adminDemoUser1.DeleteUser(demoUser1.Id);
        //demoUser1.UpdateUser(demoUser1.Id,"Noor","","",
        //  "noor@gmail.com");
        //demoUser1.CreateDiary("Demo 1 Diary 1", "");
        //demoUser1.CreateDiary("Demo 1 Diary 2", "");
        //demoUser1.CreateDiary("Demo 1 Diary 3", "");
        //demoUser1.CreateDiary("Demo 1 Diary 4", "");
        //demoUser1.CreateDiary("Demo 1 Diary 5", "");
        //demoUser1.CreateDiary("Demo 1 Diary 6", "");
        demoUser2.DisplayDiaries();

        //Console.WriteLine("\n====Diaries Display====\n");
        //adminDemoUser1.DisplayDiaryLists();

        //demoUser1.FindDiary(4);
        //demoUser1.UpdateDiary(4, "", "Update Feature Implement in diary!");
        //////adminDemoUser1.DeleteDiaryList(demoUser1.Id);

        //Console.WriteLine("\n====Diaries Display====\n");
        //adminDemoUser1.DisplayDiaryLists();



        //adminDemoUser1.FindDiaryList(demoUser1.Id);
        //adminDemoUser1.FindDiaryList(adminDemoUser1.Id);

        //Diary diary = new Diary();
        //diary.create(Name: "Today's Diary", Content: "Today's the first day to start implementing the Diaries", user: myUser);
        //Console.WriteLine(diary.display());
    }
}

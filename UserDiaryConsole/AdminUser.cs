//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Serialization;

//namespace UserDiaryConsole
//{
//    [XmlRoot("Admin")]
//    public class AdminUser : User
//    {
//        //AdminUser user;
//        //[XmlIgnore]
//        //public List<Diary_List> defaultDiaryList = Cache.defaultDiaryList;
//        //[XmlIgnore]
//        //public User_List<AdminUser> defaultAdminList = Cache.defaultAdminList;
//        //[XmlIgnore] public User_List<EmployeeUser> defaultEmpList = Cache.defaultEmpList;
//        //[XmlIgnore] public defaultUserList defaultUserList = Cache.UserList;

//        public AdminUser() { }
//        //To Register the a new account
//        public AdminUser(
//            string UserName,
//            string Name,
//            string Password,
//            string phone,
//            string email
//            ):base(
//            UserName,
//            Name,
//            Password,
//            Types.admin.ToString(),
//            Statuses.active.ToString(),
//            phone,
//            email
//                )
//        {
//            Cache.defaultAdminList.addUser(this);
//            this.UpdateUserList();
//            this.display();
//        }

//    }

//}

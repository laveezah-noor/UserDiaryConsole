using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserDiaryConsole
{

    [XmlRoot("Users")]
    public class defaultUserList
    {
        [XmlElement("Employees")]
        public User_List<EmployeeUser> Employee_UserList { get; set; }
        [XmlElement("Admins")]
        public User_List<AdminUser> Admin_UserList { get; set; }
        public defaultUserList()
        {
            this.Employee_UserList = new User_List<EmployeeUser>();
            this.Admin_UserList = new User_List<AdminUser>();
        }
    }
}

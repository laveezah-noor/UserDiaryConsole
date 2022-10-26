using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{

    class Register
    {
        public static User AdminRegister(
            string Name,
            string Password,
            string phone,
            string email
            )
        {
            return new AdminUser().Register(Name, Password, phone, email);
        }
        public static User EmployeeRegister(
            string Name,
            string Password,
            string phone,
            string email
            )
        {
            return new EmployeeUser().Register(Name, Password, phone, email);
        }
    }

}

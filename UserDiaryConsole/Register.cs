using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDiaryConsole
{

    class Register
    {
        public static void AdminRegister(
            string Name,
            string Password,
            string phone,
            string email
            )
        {
            AdminUser.Register(Name, Password, phone, email);
        }
        public static void EmployeeRegister(
            string Name,
            string Password,
            string phone,
            string email
            )
        {
            EmployeeUser.Register(Name, Password, phone, email);
        }
    }

}

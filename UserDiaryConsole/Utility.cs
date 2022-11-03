using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserDiaryConsole
{
    internal class Utility
    {
        public static int toInt(string input)
        {
            return Convert.ToInt32(input);
        }

        public static int getIntInput()
        {
            int id;
            try
            {
                id = Convert.ToInt32(getInput());
                return id;
            }
            catch
            {
                Console.WriteLine("Incorrect input!\n Try it again\n");
                id = getIntInput();
                return id;
            }
        }
        public static string getInput(string label)
        {
            string input = Console.ReadLine();
            if (input is not null && input != "" && input != " ")
            {
                return input;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect or Null input!\nTry it again\n");
                Console.WriteLine(label);
                input = getInput(label);
                return input;
            }
        }
        public static string getInput()
        {
            string input = Console.ReadLine();
            if (input is not null && input != "" && input != " ")
            {
                return input;
            }
            else
            {
                Console.WriteLine("Incorrect or Null input!\nTry it again\n");

                input = getInput();
                return input;
            }
        }
        public static string getUsernameInput()
        {
            String username = getInput();
            if (Cache.getCache().UsernameList.Contains(username))
            {
                Console.Clear();
                Console.WriteLine("Username already present\nEnter an unique username\nEnter Username:");
                username = getUsernameInput();
                return username;
            }
            else
            {
                return username;
            }
        }
        public static string getPhoneInput()
        {
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && input != "exit")
            {
                var r = new Regex(@"^\(?(03[-.●]?[0-9]{2})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
                if (r.IsMatch(input))
                {
                    return input;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nIncorrect Phone Format\nTry Again\nEnter Phone Number (03xx-xxxxxxx):\n");
                    input = getPhoneInput();
                }
            }
            return input;

        }

        public static bool ValidateEmail( string input)
        {
            if (input != "exit" && !string.IsNullOrEmpty(input))
            {
                string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

                if (Regex.IsMatch(input, expression))
                {
                    if (Regex.Replace(input, expression, string.Empty).Length == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool ValidatePhone(string input)
        {
            if (!string.IsNullOrEmpty(input) && input != "exit")
            {
                var r = new Regex(@"^\(?(03[-.●]?[0-9]{2})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
                if (r.IsMatch(input))
                {
                    return true;
                }
            }
            return false;
        }

        public static string getEmailInput()
        {
            string input = Console.ReadLine();
            if (input != "exit" && !string.IsNullOrEmpty(input))
            {
                string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

                if (Regex.IsMatch(input, expression))
                {
                    if (Regex.Replace(input, expression, string.Empty).Length == 0)
                    {
                        return input;
                    }
                }

                else
                {

                    Console.Clear();
                    //Console.WriteLine(trimmedEmail);
                    Console.WriteLine("\nIncorrect Email Format\nTry Again\nEnter Email Address:\n");
                    input = getEmailInput();
                }
            }
            return input;

        }

    }
}

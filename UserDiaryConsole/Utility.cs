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
        public static bool isNumeric(dynamic input)
        {
            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int n))
                return true;
            return false;
        }

        public static string ValidateInput(string label)
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

        public static string getInput(string label)
        {
            Console.WriteLine(label);
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect or Null input!\nTry it again\\nPress to Try again!");
                Console.ReadKey();
                input = getInput(label);
                return input;
            }
        }

        public static string getInput()
        {
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
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

        public static bool ValidateUsername(string input)
        {
            if (Cache.getCache().UsernameList.Contains(input))
            {
                Console.WriteLine("\nUsername already present\nEnter an unique username\nPress to Try again!");
                Console.ReadKey();
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ValidateEmail(string input)
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


    }
}

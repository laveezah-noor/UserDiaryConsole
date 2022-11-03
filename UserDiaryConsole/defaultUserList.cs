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
        [XmlAttribute("ID")]
        public int id;

        [XmlElement("Users")]
        public List<User> UsersList { get; set; }

        public defaultUserList()
        {
            this.UsersList = new List<User>();
        }

        public void addUser(User user)
        {
            this.UsersList.Add(user);
            Cache.getCache().UsernameList = Cache.getCache().GetUsernameList();

            Cache.getCache().defaultAdminList = Cache.getCache().GetAdminList();
        }

        public void updateUser() { }

        public bool deleteUser(int userId)
        {
            if (findUser(userId) != null)
            {
                this.UsersList.Remove(findUser(userId));

                Cache.getCache().UsernameList = Cache.getCache().GetUsernameList();
                Cache.getCache().defaultAdminList = Cache.getCache().GetAdminList();
                return true;
            }
            else { Console.Clear(); Console.WriteLine("\nNo user available of this Id!\n"); return false; }
        }

        public User findUser(int userId)
        {
            foreach (User user in this.UsersList)
            {
                if (user.Id == userId)
                {
                    return user;
                }
            }
            return null;
        }
        public User findUser(string userId)
        {
            foreach (User user in this.UsersList)
            {
                if (user.UserName == userId)
                {
                    return user;
                }
            }
            return null;
        }

        public void displayUsers(List<User> list)
        {
            Console.WriteLine(list.Count);
            foreach (User user in list)
            {
                Console.WriteLine($"ID: {user.Id}\n" +
                    $"Name: {user.Name}\n" +
                    $"UserName: {user.UserName}\n" +
                    $"Type: {user.Type}\n" +
                    $"Status: {user.Status}\n" +
                    $"Phone: {user.phone}\n" +
                    $"Email: {user.email}\n");
            }
        }

        public void displayUsers()
        {
            foreach (User user in this.UsersList)
            {
                Console.WriteLine($"ID: {user.Id}\n" +
                    $"Name: {user.Name}\n" +
                    $"UserName: {user.UserName}\n" +
                    $"Type: {user.Type}\n" +
                    $"Status: {user.Status}\n" +
                    $"Phone: {user.phone}\n" +
                    $"Email: {user.email}\n");
            }
        }
    }
}

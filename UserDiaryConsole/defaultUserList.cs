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

        [XmlElement("User")]
        public List<User> UsersList { get; set; }

        [XmlIgnore]
        public int Count
        {
            get;
            set;
        }

        public defaultUserList()
        {
            this.UsersList = new List<User>();
            Count = this.UsersList.Count;
        }

        public void addUser(User user)
        {
            this.UsersList.Add(user);
            Count = this.UsersList.Count;
            Cache.getCache().UpdateUserList();
            Cache.getCache().UsernameList = Cache.getCache().GetUsernameList();
            Cache.getCache().defaultAdminList = Cache.getCache().GetAdminList();
        }

        public void updateUser() { }

        public bool deleteUser(int userId)
        {
            if (findUser(userId) != null)
            {
                this.UsersList.Remove(findUser(userId));
                Count = this.UsersList.Count;
                Cache.getCache().UpdateUserList();
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
        public User findUser(string userName)
        {
            foreach (User user in this.UsersList)
            {
                if (user.UserName == userName)
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

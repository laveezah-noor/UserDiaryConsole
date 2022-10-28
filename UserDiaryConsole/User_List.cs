using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserDiaryConsole
{
    public class User_List<T>
    where T : User
    {
        
        public List<T> users;
        public void addUser(T user)
        {
            this.users.Add(user);
        }
        public User_List() {
        this.users = new List<T>();
        }

        public void updateUser()
        {

        }
        public void deleteUser(int userId)
        {
            this.users.Remove(findUser(userId));

        }
        public T findUser(int userId)
        {
            foreach (T user in this.users)
            {
                if (user.Id == userId)
                {
                    return user;
                }
            }
            return null;
        }
        public void displayUsers()
        {
            foreach (T user in this.users)
            {
                Console.WriteLine($"ID: {user.Id}\n" +
                    $"Name: {user.Name}\n" +
                    $"Type: {user.Type}\n" +
                    $"Status: {user.Status}\n" +
                    $"Phone: {user.phone}\n" +
                    $"Email: {user.email}\n");
            }
        }
    }
}

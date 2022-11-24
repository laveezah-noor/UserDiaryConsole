//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Serialization;

//namespace UserDiaryConsole
//{
//    public class User_List<T>
//    where T : User
//    {
        
//        public List<T> users;
        
//        public User_List() {
//        this.users = new List<T>();
//        }

//        public void addUser(T user)
//        {
//            this.users.Add(user);
//            Cache.getCache().UsernameList = Cache.getCache().GetUsernameList();
//        }
        
//        public void updateUser() {}
        
//        public bool deleteUser(int userId)
//        {
//            if (findUser(userId) != null ) { 
//                this.users.Remove(findUser(userId));

//                Cache.getCache().UsernameList = Cache.getCache().GetUsernameList(); 
//                return true; }
//            else { Console.Clear(); Console.WriteLine("\nNo user available of this Id!\n"); return false; }
//        }

//        public T findUser(int userId)
//        {
//            foreach (T user in this.users)
//            {
//                if (user.Id == userId)
//                {
//                    return user;
//                }
//            }
//            return null;
//        }
//        public T findUser(string userId)
//        {
//            foreach (T user in this.users)
//            {
//                if (user.UserName == userId)
//                {
//                    return user;
//                }
//            }
//            return null;
//        }

//        public void displayUsers()
//        {
//            foreach (T user in this.users)
//            {
//                Console.WriteLine($"ID: {user.Id}\n" +
//                    $"Name: {user.Name}\n" +
//                    $"UserName: {user.UserName}\n" +
//                    $"Type: {user.Type}\n" +
//                    $"Status: {user.Status}\n" +
//                    $"Phone: {user.phone}\n" +
//                    $"Email: {user.email}\n");
//            }
//        }
//    }
//}

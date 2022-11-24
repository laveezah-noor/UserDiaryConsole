using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserDiaryConsole
{
    public class Diary_List
    {
        [XmlElement("Diary")]
        public List<Diary> diaries = new List<Diary>();

        [XmlAttribute("UserID")]
        public int user;

        public Diary_List() { }
        public Diary_List(int ID)
        {
            this.user = ID;
        }

        // To add a new Diary in the list
        public void addDiary(string name, string content)
        {
            Diary diary = new(diaryId(),name);
            diary.create(content);
            this.diaries.Add(diary);
            Console.WriteLine("Diary Created");
        }
        int diaryId()
        {
            if (diaryCount() == 0 || diaryCount() == this.diaries.Last().Id)
            {
                return diaryCount()+1;
            }
            else return this.diaries.Last().Id + 1;

        }

        // To update the Diary in the list
        public bool UpdateDiary(int diaryId, string Name, string Content)
        {
            Diary diary = FindDiary(diaryId);
            if (diary != null)
            {
                if (Name != "" && Content != "")
                {
                    diary.updateName(Name);
                    diary.updateContent(Content);
                }
                else
                {
                    if (Name != "")
                    {
                        diary.updateName(Name);
                    }
                    else if (Content != "")
                    {
                        diary.updateContent(Content);
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Diary Not Present");
                return false;
            }
        }

        // To delete the diary from the list
        public bool deleteDiary(int id)
        {
            Diary diary = FindDiary(id);
            if (diary != null)
            {
                this.diaries.Remove(diary);
                return true;
            }
            else
            {
                Console.WriteLine("Diary Not Present");
                return false;
            }
        }

        // To find the diary from the list
        public Diary FindDiary(int diaryId)
        {
            foreach (var item in diaries)
            {
                if (item.Id == diaryId)
                {
                    Console.WriteLine("\nItem Found!\n");
                    Console.WriteLine(item.display(this.user));
                    return item;
                }
            }
            Console.WriteLine("Not Found!");
            return null;
        }

        // To display diaries of the list
        public void displayDiaries()
        {
            if (this.diaries.Count != 0) {
                for (int i = 0; i < this.diaries.Count; i++)
                {
                    Console.WriteLine(this.diaries[i].display(user));
                }
            
            } else Console.WriteLine("No Diaries Created Yet!");
        }

        // To display diary count
        public int diaryCount()
        {
            return this.diaries.Count;
        }
    }
}

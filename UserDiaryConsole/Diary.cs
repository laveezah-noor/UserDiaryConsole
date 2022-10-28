﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserDiaryConsole
{
    public class Diary
    {
        [XmlAttribute("DiaryID")]
        public int Id;
        public string Name;
        public string Content { get; set; }
        public DateTime CreatedAt;
        public DateTime LastUpdate { get; set; }
        public Diary() { }
        public Diary(int count, string Name)
        {
            this.Id = count;
            this.Name = Name;
            this.Content = "";
            this.CreatedAt = DateTime.Now;
            this.LastUpdate = DateTime.Now;
        }
        public void create(string Content)
        {
            this.Content = Content;
        }
        public void updateName(string text)
        {
            this.Name = text;
            this.LastUpdate = DateTime.Now;
        }
        public void updateContent(string text)
        {
            this.Content = text;
            this.LastUpdate = DateTime.Now;
        }

        public string display(int user)
        {
            return $"ID: {this.Id} ,Title: {this.Name}, Content: {this.Content}, UserID: {user}";
        }

        public string display()
        {
            return $"ID: {this.Id} ,Title: {this.Name}, Content: {this.Content}";
        }
    }
}

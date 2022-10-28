using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserDiaryConsole
{
    public class Xml<T> where T : class
    {
        public static void Serialize(T Item)
        {
            //Type type = Item.GetType().GetProperty("Item").PropertyType;
            Type type = Item.GetType();
            //Console.WriteLine($"Item Name: {type.GetProperty("Item")}");
            //Console.WriteLine(Item);
            XmlSerializer serializer = new XmlSerializer(Item.GetType());
            using (TextWriter writer = new StreamWriter(@$"{type}.xml"))
            {
                serializer.Serialize(writer, Item);
                writer.Close();
            }
            //     serializer.Serialize(Console.Out, Item);
            Console.WriteLine("\nXml Updated\n");
        }

        public static T Deserialize(T Item)
        {
            XmlSerializer deserializer = new XmlSerializer(Item.GetType());
            TextReader reader = new StreamReader(@$"{Item.GetType()}.xml");
            object obj = deserializer.Deserialize(reader);
            T XmlData = (T)obj;
            //Console.WriteLine(XmlData);
            reader.Close();
            return XmlData;
        }
    }
}

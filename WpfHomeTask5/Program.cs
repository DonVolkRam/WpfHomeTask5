using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfHomeTask5
{
    class Program
    {
        List<Employee> list;
        string fileName;
        /// <summary>
        /// Метод сохранения колекции вопросов в базе
        /// </summary>
        public void Save()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Employee>));
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlFormat.Serialize(fStream, list);
            fStream.Close();
        }

        /// <summary>
        /// метод загрузки коллекции из базы
        /// </summary>
        public void Load()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Employee>));
            Stream fStream = new FileStream(fileName, FileMode.Open,
            FileAccess.Read);
            list = (List<Employee>)xmlFormat.Deserialize(fStream);
            fStream.Close();
        }
    }
}

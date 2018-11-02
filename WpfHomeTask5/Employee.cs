using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfHomeTask5
{
    /// <summary>
    /// Класс описатель работника
    /// </summary>
    [Serializable]
    public class Employee
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Департамент
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Полная информация о сотруднике
        /// </summary>
        public string All { get => this.ToString(); }
        /// <summary>
        /// Порядковый номер созданного объекта
        /// </summary>
        static int Count;
        /// <summary>
        /// Генератор чисел
        /// </summary>
        public static Random Rnd;
        /// <summary>
        /// Инициализация статических переменных
        /// </summary>
        static Employee() { Rnd = new Random(); Count = 0; }
        /// <summary>
        /// класс для сериализации
        /// </summary>
        Employee() { }
        /// <summary>
        /// Конструктор создающий случайного рабочего с возрастом от 18 и не превышающим 60
        /// и с заданным именем департамента
        /// </summary>
        /// <param name="age"></param>
        /// <param name="department">департамент</param>
        public Employee(string department)
        {
            FirstName = $"Имя{Count}";
            LastName = $"Фамилия{Count}";
            Age = Rnd.Next(18, 60);
            Department = department;
            Count++;
        }
        /// <summary>
        /// Переопределение вывода в строку главных полей класса
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{FirstName} {LastName} {Age} лет"; 
        }
        /// <summary>
        /// Имя файла с базой имен
        /// </summary>
        string fileName;
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfHomeTask5
{
    /// <summary>
    /// Класс описатель департамента
    /// </summary>
    class Department: INotifyPropertyChanged
    {
        /// <summary>
        /// Переменная для генерации
        /// </summary>
        static Random Rnd;
        /// <summary>
        /// Порядковый номер созданного объекта
        /// </summary>
        static int Count;
        /// <summary>
        ///наименование департамента 
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }
        private string name;
        /// <summary>
        /// Список сотрудников в данном департаменте
        /// </summary>
        public ObservableCollection<Employee> Workers = new ObservableCollection<Employee>();
        /// <summary>
        /// Инициализация статических параметров класса
        /// </summary>
        static Department() { Rnd = new Random(); Count = 0; }
        /// <summary>
        /// Коструктор генерирующий случайное имя департамента 
        /// и случайное (от 1 до 10) сотрудников в департаменте
        /// в возрасте от 18 до 60 лет 
        /// </summary>
        public Department()
        {
            Name = $"Департамент{Count}";
            for (int i = 0; i < Rnd.Next(1,11); i++)
                Workers.Add(new Employee(Name));            
            Count++;
        }

        public Department(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new
                PropertyChangedEventArgs(propName));
        }
    }
}

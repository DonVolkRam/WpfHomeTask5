using System;
using System.Collections;
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
    [Serializable]
    public class Department : INotifyPropertyChanged /*: IEnumerable*/
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
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        private string name;
        /// <summary>
        /// Список сотрудников в данном департаменте
        /// </summary>
        public ObservableCollection<Employee> Workers
        {
            get { return workers; }
            set
            {
                workers = value;
                NotifyPropertyChanged("Workers");
            }
        }
        private ObservableCollection<Employee> workers = new ObservableCollection<Employee>();
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
            for (int i = 0; i < Rnd.Next(1, 11); i++)
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
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        //public IEnumerator GetEnumerator()
        //{
        //    foreach (Employee e in Workers)
        //    {
        //        yield return (Employee)e;
        //    }
        //}
        //private bool IsNullOrEmpty()
        //{
        //    bool flag = true;

        //    if (Workers != null)
        //    {
        //        if (Workers.Count > 0)
        //        {
        //            flag = false;
        //        }
        //    }
        //    return flag;
        //}
        //public Employee this[int index]
        //{
        //    get => !IsNullOrEmpty() ? Workers[index] : null;
        //}
        //public void Add(Employee E)
        //{
        //    Workers.Add(E);
        //}
        //public void Add(Department item)
        //{
        //    ICollection<Department>.Add(item);
        //}
    }
}

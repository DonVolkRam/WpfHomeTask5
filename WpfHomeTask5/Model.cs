using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace WpfHomeTask5
{
    [Serializable]
    class Model
    {
        #region Данные
        /// <summary>
        /// Путь к файлу с базой департаментов
        /// </summary>
        private readonly string FilePath;
        /// <summary>
        /// Формируемый список департаментов
        /// </summary>
        //        private ObservableCollection<Department> depList;
        //        public ObservableCollection<Department> DepList => this.depList;
        public ObservableCollection<Department> DepList { get; set; }
        /// <summary>
        /// Номер выбранного департамента
        /// </summary>       
        public int DepIndex { get ; set; }       
        /// <summary>
        /// Номер выбранного сотрудника
        /// </summary>        
        public int EmpIndex { get ; set ; }
       
        #endregion

        public Model(string filePath = "..\\..\\data.xml")
        {
            DepList = new ObservableCollection<Department>();
            DepIndex = 0;
            EmpIndex = 0;
            this.FilePath = filePath;           
        }
        /// <summary>
        /// Загрузка коллекции департаментов из базы
        /// </summary>        
        public void Load()
        {
            try
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Department>));
                Stream fStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                DepList = (ObservableCollection<Department>)xmlFormat.Deserialize(fStream);
                fStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}\nНет БД");
            }
        }
        /// <summary>
        /// Сохранение колекции департаментов в базе
        /// </summary>        
        public void Save()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Department>));
            Stream fStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            xmlFormat.Serialize(fStream, DepList);
            fStream.Close();
        }
        /// <summary>
        /// Получение списка департаментов
        /// </summary>
        public ObservableCollection<Department> GetDepList
        {
            get
            {
                if (DepIndex >= 0) { return DepList; }
                else { return null; }
            }
        }
        /// <summary>
        /// Получение списка сотрудника из (DepIndex) выбранного департамента
        /// </summary>
        public ObservableCollection<Employee> GetEmpList
        {
            get
            {
                if (DepIndex >= 0 && EmpIndex >= 0) { return DepList[DepIndex].Workers; }
                else { return null; }
            }
        }
    }
}

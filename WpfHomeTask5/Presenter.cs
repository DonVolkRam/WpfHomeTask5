using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfHomeTask5
{
    class Presenter
    {
        private Model model;
        private IView view;

        public Presenter(IView View)
        {
            this.view = View;
            model = new Model();
            model.DepList.CollectionChanged += (s, e) => CollectionChange();
        }

        public Presenter()
        {
        }

        public void CollectionChange()
        {
            view.DepList = model.DepList;            
        }
        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        public void Load()
        {
            model.Load();

            if (model.DepList.Count > 0)
            {
//                view.DepList = model.DepList;
                model.DepIndex = 0;
                if (model.DepList[model.DepIndex].Workers.Count > 0)
                {
                    model.EmpIndex = 0;
                    var temp = model.DepList[model.DepIndex].Workers[model.EmpIndex];

                    view.FirstName = temp.FirstName;
                    view.FamilyName = temp.FamilyName;
                    view.Age = temp.Age.ToString();
                    view.Department = temp.Department;
                }
            }
        }
        /// <summary>
        ///  Сохранение всех департаментов в файл
        /// </summary>
        public void Save()
        {
//            model.DepList = view.DepList;
            model.Save();
        }
        /// <summary>
        /// Генерация департамента
        /// </summary>
        public void Generate()
        {
            Department dep = new Department();
            model.DepList.Add(dep);
            Console.WriteLine(dep.Name);
        }
        /// <summary>
        /// кнопка добавления сотрудника, сохраняет введеные данные из текстовых полей в коллекцию
        /// если департамент новый, то создает его и добавляет сотрудника туда
        /// или же ищет совпадения в имеющихся департаментах и добавляет туда
        /// </summary>
        public void Add()
        {
            if (view.Department == model.DepList[view.DepIndex].Name)
                model.DepList[view.DepIndex].Workers.Add(
                    new Employee(
                        view.FirstName,
                        view.FamilyName,
                        view.FatherName,
                        Convert.ToInt32(view.Age),
                        view.Department));
            else
            {
                foreach (var a in model.DepList)
                {
                    if (view.Department == a.Name)
                    {
                        a.Workers.Add(
                            new Employee(
                                view.FirstName,
                                view.FamilyName,
                                view.FatherName,
                                Convert.ToInt32(view.Age),
                                view.Department));
                        return;
                    }
                }
                model.DepList.Add(new Department(view.Department));
                model.DepList[model.DepIndex - 1].Workers.Add(
                    new Employee(
                                view.FirstName,
                                view.FamilyName,
                                view.FatherName,
                                Convert.ToInt32(view.Age),
                                view.Department));
            }
        }
        /// <summary>
        /// Изменение данных о сотруднике
        /// Сохраняет введеные значения в поля в виде нового сотрудника
        /// </summary>
        public void Change()
        {
            if (model.DepList[view.DepIndex].Workers.Count > 0)
            {
                model.DepList[view.DepIndex].Workers[view.EmpIndex].FirstName = view.FirstName;
                model.DepList[view.DepIndex].Workers[view.EmpIndex].FamilyName = view.FamilyName;
                model.DepList[view.DepIndex].Workers[view.EmpIndex].Age = Convert.ToInt32(view.Age);
                model.DepList[view.DepIndex].Workers[view.EmpIndex].Department = view.Department;
            }
        }
        /// <summary>
        /// Вывод сотрудников по выбранному департаменту
        /// </summary>
        public void SelectDep()
        {
            //if (view.DepList.Count == 0)
            //    return;
            if (view.DepIndex == -1)
                view.DepIndex = 0;
            view.EmpList = model.DepList[view.DepIndex].Workers;
        }
        /// <summary>
        /// выбор сотрудника и вывод в текстовые поля его данных
        /// </summary>
        public void SelectEmp()
        {
            if (view.DepList[view.DepIndex].Workers.Count == 0)
                return;
            if (view.EmpIndex == -1)
                view.EmpIndex = 0;
            int depI = view.DepIndex;
            int empI = view.EmpIndex;
            var temp = model.DepList[depI].Workers[empI];
            if (model.DepList[depI].Workers.Count == 0)
            {
                view.FirstName = view.FamilyName = view.Department = "Н/Д";
                view.Age = "0";
            }
            else
            {
                view.FirstName = temp.FirstName;
                view.FamilyName = temp.FamilyName;
                view.Age = temp.Age.ToString();
                view.Department = temp.Department;
            }
        }

        public void CMI_DepRemove()
        {
            if (model.DepList[view.DepIndex].Workers.Count > 0)
                MessageBox.Show("Нальзя удалить департамент в котором есть сотрудники.\n" +
                    "Сначала переместите сотрудников в другой департамент");
            else
                model.DepList.RemoveAt(view.DepIndex);
        }
        public void CMI_EmpRemove()
        {
            model.DepList[view.DepIndex].Workers.RemoveAt(view.EmpIndex);
        }

        public void TransferEmployee(int index)
        {
            Employee Worker = model.DepList[view.DepIndex].Workers[view.EmpIndex];
            Worker.Department = model.DepList[index].Name;
            model.DepList[index].Workers.Add(Worker);
            model.DepList[view.DepIndex].Workers.RemoveAt(view.EmpIndex);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfHomeTask5
{
    class Presenter
    {
        private Model model;
        private IView view;

        public void Load()
        {
            model.Load();

            if (model.DepList.Count > 0)
            {
                view.DepList = model.DepList;
                model.DepIndex = 0;           
                if (model.DepList[model.DepIndex].Workers.Count > 0)
                {
                    model.EmpIndex = 0;
                    var temp = model.DepList[model.DepIndex].Workers[model.EmpIndex];

                    view.FirstName = temp.FirstName;
                    view.LastName = temp.LastName;
                    view.Age = temp.Age.ToString();
                    view.Department = temp.Department;
                }
            }
        }

        public void Save()
        {
            model.Save();
        }

        public void SelectDep()
        {
            if (model.DepIndex == -1)
                model.DepIndex = 0;
            view.EmpList = model.DepList[model.DepIndex].Workers;
        }
        public void SelectEmp()
        {
            if (model.EmpIndex == -1)
                model.EmpIndex = 0;
            int depI = model.DepIndex;
            int empI = model.EmpIndex;
            var temp = model.DepList[depI].Workers[empI];
            if (model.DepList[depI].Workers.Count == 0)
            {
                view.FirstName = view.LastName = view.Department = "Н/Д";
                view.Age = "0";
            }
            else
            {
                view.FirstName = temp.FirstName;
                view.LastName = temp.LastName;
                view.Age = temp.Age.ToString();
                view.Department = temp.Department;
            }
        }

    }
}

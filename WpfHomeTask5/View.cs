using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfHomeTask5
{
    public interface IView
    {
        ObservableCollection<Department> DepList { get; set; }
        ObservableCollection<Employee> EmpList { get; set; }

        string FirstName { get; set; }
        string LastName { get; set; }
        string Age { get; set; }
        string Department { get; set; }
    }
}

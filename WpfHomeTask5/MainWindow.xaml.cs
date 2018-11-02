using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfHomeTask5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Department> DepList = new ObservableCollection<Department>();
        public MainWindow()
        {
            InitializeComponent();
            DepList.Add(new Department());
            DepartmentList.ItemsSource = DepList;
            DepartmentList.DisplayMemberPath = "Name";
            EmployeeList.ItemsSource = DepList[DepartmentList.Items.CurrentPosition].Workers;
            EmployeeList.DisplayMemberPath = "All";
        }
        /// <summary>
        /// Кнопка генерации департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DepList.Add(new Department());
        }
        /// <summary>
        /// Вывод сотрудников по выбранному департаменту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EmployeeList.ItemsSource = DepList[DepartmentList.SelectedIndex].Workers;
        }
    }
}

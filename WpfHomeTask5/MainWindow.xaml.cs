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
            lvDepartment.ItemsSource = DepList;           
            lvEmployee.ItemsSource = DepList[lvDepartment.Items.CurrentPosition].Workers;
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


        private void btChange_Click(object sender, RoutedEventArgs e)
        {
            DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].FirstName = tbName.Text;
            DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].LastName = tbLastName.Text;           
            DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].Age = Convert.ToInt32(tbAge.Text);
            DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].Department = tbDep.Text;
        }
        /// <summary>
        /// Вывод сотрудников по выбранному департаменту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvEmployee.ItemsSource = DepList[lvDepartment.SelectedIndex].Workers;
        }

        private void lvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbName.Text = DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].FirstName;
            tbLastName.Text = DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].LastName;
            tbAge.Text = DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].Age.ToString();
            tbDep.Text = DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].Department;
        }
    }
}

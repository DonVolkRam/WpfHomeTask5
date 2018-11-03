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
            try
            {
                DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].Age = Convert.ToInt32(tbAge.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"{ex.Message} Введите корректный возраст");
            }
            DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].FirstName = tbName.Text;
            DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].LastName = tbLastName.Text;
            DepList[lvDepartment.SelectedIndex].Workers[lvEmployee.SelectedIndex].Department = tbDep.Text;
            lvDepartment.Items.Refresh();
            lvEmployee.Items.Refresh();
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
            var emp = lvEmployee.Items.CurrentPosition;
            //var emp = lvEmployee.SelectedIndex;
            if (DepList[lvDepartment.SelectedIndex].Workers.Count == 0)
            {
                tbName.Text = tbLastName.Text = tbDep.Text = "Н/Д";
                tbAge.Text = "0";
            }
            else
            {
                tbName.Text = DepList[lvDepartment.SelectedIndex].Workers[emp].FirstName;
                tbLastName.Text = DepList[lvDepartment.SelectedIndex].Workers[emp].LastName;
                tbAge.Text = DepList[lvDepartment.SelectedIndex].Workers[emp].Age.ToString();
                tbDep.Text = DepList[lvDepartment.SelectedIndex].Workers[emp].Department;
            }           
        }

        private void cmi_change_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DepList.Count != cmi_change.Items.Count)
            {
                cmi_change.Items.Clear();
                foreach (var a in DepList)
                {
                    MenuItem mi_add = new MenuItem();
                    mi_add.Header = a.Name;
                    cmi_change.Items.Add(mi_add);
                }
            }
        }

        private void cmi_remove1_Click(object sender, RoutedEventArgs e)
        {
            if (DepList[lvDepartment.SelectedIndex].Workers.Count > 0)
                MessageBox.Show("Нальзя удалить департамент в котором есть сотрудники.\n" +
                    "Сначала переместите сотрудников в другой департамент");
            else
                DepList.RemoveAt(lvDepartment.SelectedIndex);
        }

        private void cmi_Employee_remove_Click(object sender, RoutedEventArgs e)
        {
            DepList[lvDepartment.SelectedIndex].Workers.RemoveAt(lvEmployee.SelectedIndex);
            lvEmployee.Items.Refresh();
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            DepList[lvDepartment.SelectedIndex].Workers.Add(
                new Employee(tbName.Text, tbLastName.Text, Convert.ToInt32(tbAge.Text), tbDep.Text));
        }

        private void tbAge_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void tbAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            int chk;
            try
            {
                chk= Convert.ToInt32(tbAge.Text);
                if (chk<0 || chk>150)
                    throw new Exception();
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"{ex.Message}\nВведите корректный возраст");
                tbAge.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nНедопустимое значение возраста");
                tbAge.Text = "0";
            }           
        }
    }
}

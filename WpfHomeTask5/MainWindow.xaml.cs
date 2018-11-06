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
        ObservableCollection<Department> DepList { get; set; }
//        ObservableCollection<Department> depList = new ObservableCollection<Department>();

        public MainWindow()
        {
            InitializeComponent();           
            DataContext = this;
            DepList = new ObservableCollection<Department>
            {
                new Department()
            };
            lvDepartment.ItemsSource = DepList;
            //            lvEmployee.ItemsSource = DepList[lvDepartment.Items.CurrentPosition].Workers;
            //lvDepartment.SelectedIndex = 0;
            //lvEmployee.ItemsSource = DepList[lvDepartment.SelectedIndex].Workers;
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
        /// Изменение данных о сотруднике
        /// Сохраняет введеные значения в поля в виде нового сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btChange_Click(object sender, RoutedEventArgs e)
        {
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
            //если выделение отсутствует то встать на первую позицию
            if (lvDepartment.SelectedIndex == -1)
                lvDepartment.SelectedIndex = 0;
            lvEmployee.ItemsSource = DepList[lvDepartment.SelectedIndex].Workers;
            lvEmployee.SelectedIndex = 0;
        }
        /// <summary>
        /// выбор сотрудника и вывод в текстовые поля его данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var emp = lvEmployee.Items.CurrentPosition;
            //если выделение отсутствует то встать на первую позицию
            if (lvEmployee.SelectedIndex == -1)
                lvEmployee.SelectedIndex = 0;
            var emp = lvEmployee.SelectedIndex;

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
        /// <summary>
        /// при наведении на мышку создается столько подменю сколько сейчас есть департаментов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// контекстная кнопка удаления департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmi_remove1_Click(object sender, RoutedEventArgs e)
        {
            if (DepList[lvDepartment.SelectedIndex].Workers.Count > 0)
                MessageBox.Show("Нальзя удалить департамент в котором есть сотрудники.\n" +
                    "Сначала переместите сотрудников в другой департамент");
            else
                DepList.RemoveAt(lvDepartment.SelectedIndex);
        }
        /// <summary>
        /// еонтекстная кнопка удаления сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmi_Employee_remove_Click(object sender, RoutedEventArgs e)
        {
            DepList[lvDepartment.SelectedIndex].Workers.RemoveAt(lvEmployee.SelectedIndex);
            lvEmployee.Items.Refresh();
        }
        /// <summary>
        /// кнопка добавления сотрудника, сохраняет введеные данные из текстовых полей в коллекцию
        /// если департамент новый, то создает его и добавляет сотрудника туда
        /// или же ищет совпадения в имеющихся департаментах и добавляет туда
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbDep.Text == DepList[lvDepartment.SelectedIndex].Name)
                DepList[lvDepartment.SelectedIndex].Workers.Add(
                    new Employee(tbName.Text, tbLastName.Text, Convert.ToInt32(tbAge.Text), tbDep.Text));           
            else
            {
                foreach (var a in DepList)
                {
                    if (tbDep.Text == a.Name)
                    {
                        a.Workers.Add(new Employee(tbName.Text, tbLastName.Text, Convert.ToInt32(tbAge.Text), tbDep.Text));
                        return;
                    }
                }
                DepList.Add(new Department(tbDep.Text));
                DepList[DepList.Count-1].Workers.Add(
                    new Employee(tbName.Text, tbLastName.Text, Convert.ToInt32(tbAge.Text), tbDep.Text));
            }
        }
        /// <summary>
        /// проверка корректности ввода значений возраста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            int chk;
            try
            {
                chk = Convert.ToInt32(tbAge.Text);
                if (chk < 0 || chk > 150)
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

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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            //ApplicationViewModel.GenerateEmployee();
            ApplicationViewModel.ReadToConsole();
            Presenter P = new Presenter();
            //Binding binding = new Binding();
            //binding.ElementName = "MainWindow";
            //binding.Path = new PropertyPath("DepList");
            //lvDepartment.SetBinding(ListView.ItemsSourceProperty, binding);
            //lvDepartment.ItemsSource = DepList;
            //DepList.CollectionChanged
            //EmpList = DepList[lvDepartment.SelectedIndex].Workers;
            btnSave.Click += (s, e) => P.Save();
            btnLoad.Click += (s, e) => P.Load();
            btnGenerate.Click += (s, e) => P.Generate();
            btnAdd.Click += (s, e) => P.Add();
            btnChange.Click += (s, e) => P.Change();
            lvDepartment.SelectionChanged += (s, e) => P.SelectDep();
            lvEmployee.SelectionChanged += (s, e) => P.SelectEmp();
            cmi_DepRemove.Click += (s, e) => P.CMI_DepRemove();
            cmi_EmpRemove.Click += (s, e) => P.CMI_EmpRemove();
            //cmi_change.Click += (s, e) => P.CMI_EmpChange();
            //DepList.Add(new Department());
            
            //if (lvDepartment.SelectedIndex > 0)
            //    lvEmployee.ItemsSource = DepList[lvDepartment.SelectedIndex].Workers;
        }
        /// <summary>
        /// при наведении на мышку создается столько подменю сколько сейчас есть департаментов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmi_change_GotFocus(object sender, RoutedEventArgs e)
        {
            //    if (DepList.Count != cmi_Change.Items.Count)
            //    {
            //        cmi_Change.Items.Clear();
            //        foreach (var a in DepList)
            //        {
            //            MenuItem mi_add = new MenuItem();
            //            mi_add.Header = a.Name;
            //            mi_add.Click += (sender1, e1) => this.P.TransferEmployee(cmi_Change.Items.IndexOf(mi_add));
            //            cmi_Change.Items.Add(mi_add);
            //        }
            //    }
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

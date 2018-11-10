using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        public DataRow resultRow { get; set; }
        public MainWindow()
        {
            InitializeComponent();
           //ApplicationViewModel.GenerateDepartment();

            DataContext = new ApplicationViewModel();
//            ApplicationViewModel.GenerateEmployee();
            ApplicationViewModel.ReadToConsole();
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
            //int chk;
            //try
            //{
            //    chk = Convert.ToInt32(tbAge.Text);
            //    if (chk < 0 || chk > 150)
            //        throw new Exception();
            //}
            //catch (FormatException ex)
            //{
            //    MessageBox.Show($"{ex.Message}\nВведите корректный возраст");
            //    tbAge.Text = "0";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"{ex.Message}\nНедопустимое значение возраста");
            //    tbAge.Text = "0";
            //}
        }        
    }
}

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
            DataContext = new ApplicationViewModel();
            //ApplicationViewModel.GenerateEmployee();
            //ApplicationViewModel.GenerateDepartment();
            //ApplicationViewModel.ReadToConsole();
        }     
    }
}

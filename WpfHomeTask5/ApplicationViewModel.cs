using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfHomeTask5
{
    class ApplicationViewModel: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        static string ConnectionString = @"  Data Source=(localdb)\MSSQLLocalDB;
                    Initial Catalog=Lesson7;
                    Integrated Security=True;
                    Pooling=False";
        static int count;
        static SqlConnection connection = new SqlConnection(ConnectionString);
        public static DataTable dataTable { get; set; }
        static SqlDataAdapter adapter = new SqlDataAdapter();

        static SqlCommand command = new SqlCommand(
            "SELECT Id, FirstName, FamilyName, FatherName, BirthDate, Department, DepartmentId FROM Employee",
            connection);

        static ApplicationViewModel()
        {
            ConnectionString =
                @"  Data Source=(localdb)\MSSQLLocalDB;
                    Initial Catalog=Lesson7;
                    Integrated Security=True;
                    Pooling=False";
            count = 0;
        }
        public ApplicationViewModel()
        {
            adapter.SelectCommand = command;

            dataTable = new DataTable();
            adapter.Fill(dataTable);           
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      //Phone phone = new Phone();
                      //Phones.Insert(0, phone);
                      //SelectedPhone = phone;
                  }));
            }
        }





        static public void GenerateEmployee()
        {
            // Connection - Устанавливает подключение к источнику данных
            // Command - Позволяет выполнять операции с данными из БД
            // DataReader - Позволяет хранить и работать с данными независимо от БД
            // DataSet, DataTable - содержит данные, полученные из БД
            // DataAdapter - посредник между DataSet и источником данных
          
            try
            {
                var random = new Random();
                for (int i = 0; i < 10; i++)
                {
                    int temp = random.Next(0, 10);
                    count = i+1;
                    var worker = new Employee
                    {
                        FirstName = $"Имя_{random.Next(0, 100)}",
                        FamilyName = $"Фамилия_{random.Next(0, 100)}",
                        FatherName = $"Отчество_{random.Next(0, 100)}",
                        BirthDay = random.Next(1, 30),
                        BirthMounth = random.Next(1, 13),
                        BirthYear = random.Next(1950, 2000),
                        Department = $"Департамент_{temp}",
                        DepartmentId = temp,
                    };

                    var sql = String.Format("INSERT INTO Employee (Id, FirstName, FamilyName, FatherName, " +
                                                                "BirthDate, Department, DepartmentId)" +
                                            "VALUES (N'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                            count,
                                            worker.FirstName,
                                            worker.FamilyName,
                                            worker.FatherName,
                                            worker.BirthDate,
                                            worker.Department,
                                            worker.DepartmentId
                                            );

                    Console.WriteLine(sql);

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();

                        #region sql ex

                        //sql = $@"UPDATE People SET Phone ='6' WHERE FIO = 'ФИО_1' ";

                        //command = new SqlCommand(sql, connection);
                        //command.ExecuteNonQuery();

                        //sql = $@"DELETE FROM People WHERE id>20";

                        //var command = new SqlCommand(sql, connection);
                        //command.ExecuteNonQuery();

                        #endregion
                    }
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("exit");
            }                      
        }
        public static void ReadToConsole()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var sql = @" SELECT * FROM Employee";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"],2} | " +
                                          $"{reader["FirstName"],10} | " +
                                          $"{reader["FamilyName"],15} | " +
                                          $"{reader["FatherName"],15} | " +
                                          $"{reader["BirthDate"],12} | " +
                                          $"{reader["Department"],20} | " +
                                          $"{reader["DepartmentId"],2} | ");                                          
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

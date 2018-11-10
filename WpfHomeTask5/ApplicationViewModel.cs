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
    class ApplicationViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие изменения состояний
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Строка подключения
        /// </summary>
        static string ConnectionString = @"  Data Source=(localdb)\MSSQLLocalDB;
                    Initial Catalog=Lesson7;
                    Integrated Security=True;
                    Pooling=False";
        static int count;
        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        static SqlConnection connection = new SqlConnection(ConnectionString);       

        public DataTable dataTableEmployee { get; set; }
        public DataTable dataTableDepartment { get; set; }
        SqlDataAdapter adapterEmployee = new SqlDataAdapter();
        SqlDataAdapter adapterDepartment = new SqlDataAdapter();
        public object SelectedEmployee { get; set; }
        public object SelectedDepartment { get; set; }
        static SqlCommand commandSelectEmployee = new SqlCommand(
            "SELECT Id, FirstName, FamilyName, FatherName, BirthDate, Department, DepartmentId FROM Employee",
            connection);
        static SqlCommand commandDeleteEmployee = new SqlCommand(
            "DELETE FROM Employee WHERE ID = @ID",
            connection);
        static SqlCommand commandSelectDepartment = new SqlCommand(
            "SELECT Id, Name FROM Department",
            connection);
        static SqlCommand commandDeleteDepartment = new SqlCommand(
            "DELETE FROM Department WHERE ID = @ID",
             connection);

        static ApplicationViewModel()
        {
            count = 0;
        }
        public ApplicationViewModel()
        {
            adapterEmployee.SelectCommand = commandSelectEmployee;
            dataTableEmployee = new DataTable();
            adapterEmployee.Fill(dataTableEmployee);

            adapterDepartment.SelectCommand = commandSelectDepartment;
            dataTableDepartment = new DataTable();
            adapterDepartment.Fill(dataTableDepartment);

            SqlParameter parameter = commandDeleteEmployee.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
            parameter.SourceVersion = DataRowVersion.Original;

            adapterEmployee.DeleteCommand = commandDeleteEmployee;
            adapterDepartment.DeleteCommand = commandDeleteDepartment;

        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
     
        private RelayCommand addCommand;
        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      //DataRow newRow = dataTable.NewRow();
                      //newRow.Rows.Add(editWindow.resultRow);
                      //    adapter.Update(dataTable);

                  }));
            }
        }

        private RelayCommand removeCommandEmployee;
        /// <summary>
        /// Удаление сотрудника из базы
        /// </summary>
        public RelayCommand RemoveCommandEmployee
        {
            get
            {
                return removeCommandEmployee ??
                  (removeCommandEmployee = new RelayCommand(obj =>
                  {
                      DataRowView rowView = (DataRowView)SelectedEmployee;
                      rowView.Row.Delete();
                      adapterEmployee.Update(dataTableEmployee);
                  }, (obj) => dataTableEmployee.Rows.Count > 0));
            }
        }
        /// <summary>
        /// Создание 10 департаментов
        /// </summary>
        static public void GenerateDepartment()
        {
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    var Dep = new Department
                    {
                        Name = $"Департамент_{i}",
                    };

                    var sql = String.Format("INSERT INTO Department (Id, Name) VALUES ('{0}', '{1}')",
                                            i, Dep.Name);

                    Console.WriteLine(sql);

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();
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

        /// <summary>
        /// Создание 10 случайных сотрудников
        /// </summary>
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
                    count = i + 1;
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
                                            "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
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
        /// <summary>
        /// Вывод сотрудников в консоль
        /// </summary>
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

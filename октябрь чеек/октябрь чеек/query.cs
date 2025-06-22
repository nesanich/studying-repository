using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace октябрь_чеек
{
    internal class query
    {
        DataTable Bufer;
        SQLiteCommand comand;
        //SQLiteConnection conect;
        //SQLiteDataAdapter adapter;
        string connectionString = "Data Source=BDQ.db; Version=3;";

        public query(string conn)
        {
            //conect = new SQLiteConnection(conn);
            Bufer =  new DataTable();
        }

        public class ComboBoxItem
        {
            public string DisplayValue { get; set; } // Значение для отображения
            public string Value { get; set; } // Значение, которое будет использоваться
            public override string ToString() => DisplayValue; // Переопределяем ToString для отображения
        }
        public void poluchdata(string conect, ComboBox combo, string zap, bool r)
        {

            using (SQLiteConnection myCon = new SQLiteConnection(connectionString))
            {
                try
                {
                    SQLiteCommand com = new SQLiteCommand(zap, myCon);
                    myCon.Open();
                    SQLiteDataReader reader = com.ExecuteReader();
                    if(r==true)
                    {
                        while (reader.Read())
                        {
                            var item = new ComboBoxItem
                            {
                                DisplayValue = reader[1].ToString(),
                                Value = reader[1].ToString()
                            };
                            combo.Items.Add(item);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            var item = new ComboBoxItem
                            {
                                DisplayValue = reader[0].ToString(),
                                Value = reader[0].ToString()
                            };
                            combo.Items.Add(item);
                        }
                    }
                   
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { myCon.Close(); }
            }
        }
        
        public int retvalue(ComboBox combo) { return (combo.SelectedIndex +1); }
        public int GetIdByValue(string tableName, string column, string value)
        {
            int id = -1; // Значение по умолчанию, если ID не найден

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT ID FROM {tableName} WHERE {column} = @Value"; 

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Value", value);

                    try
                    {
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            id = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return id;
        }

        public DataTable Update(string query)
        {
            DataTable dt = new DataTable();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    adapter.Fill(dt);
                    connection.Close();
                    return dt;
                }
            }
        }
        public string ConvertDateFormat(string date)
        {
            // Проверяем, что строка имеет правильный формат
            if (DateTime.TryParseExact(date, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                // Изменяем формат на yyyyMMdd
                string formattedDate = parsedDate.ToString("yyyyMMdd");
                // Преобразуем строку в число и возвращаем
                return formattedDate;
            }
            else
            {
                throw new FormatException("Некорректный формат даты. Ожидался формат dd/MM/yyyy.");
            }
        }
        public DataTable GetRowsInRange(string tableName, int number1, int number2, string query)
        {
            DataTable resultTable = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Добавляем параметры для защиты от SQL-инъекций
                    command.Parameters.AddWithValue("@Number1", number1);
                    command.Parameters.AddWithValue("@Number2", number2);

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                    try
                    {
                        connection.Open();
                        adapter.Fill(resultTable);
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок (можно улучшить)
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
            }

            return resultTable;
        }
        public void Add_contract(string employee_id, string date_contract, string tarif_id, string client_id)
         {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                comand = new SQLiteCommand($"INSERT INTO contract(employee_id, date_contract, tarif_id, client_id, d_score) VALUES(@employee_id, @date_contract, @tarif_id, @client_id, @d_score)", connection);
                comand.Parameters.AddWithValue("@employee_id", employee_id);
                comand.Parameters.AddWithValue("@date_contract", date_contract);
                comand.Parameters.AddWithValue("@tarif_id", tarif_id);
                comand.Parameters.AddWithValue("@client_id", client_id);
                comand.Parameters.AddWithValue("@d_score", ConvertDateFormat(date_contract));
                try
                {
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Данные успешно добавлены!");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка при добавлении данных", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                
                connection.Close();
            }
        }
        public void Add_AD(string period, string date, string client_id, string employee_id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                comand = new SQLiteCommand($"INSERT INTO ad_contract(period, date, client_id, employee_id, d_score) VALUES(@period, @date, @client_id, @employee_id, @d_score)", connection);
                comand.Parameters.AddWithValue("@period", period);
                comand.Parameters.AddWithValue("@date", date);
                comand.Parameters.AddWithValue("@client_id", client_id);
                comand.Parameters.AddWithValue("@employee_id", employee_id);
                comand.Parameters.AddWithValue("@d_score", ConvertDateFormat(date));
                try
                {
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Данные успешно добавлены!");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка при добавлении данных", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                connection.Close();
            }
        }
        public void Add_tarif(string tarif_name, string speed, string type)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                comand = new SQLiteCommand($"INSERT INTO tarif(tarif_name, speed, type) VALUES(@tarif_name, @speed, @type)", connection);
                comand.Parameters.AddWithValue("@tarif_name", tarif_name);
                comand.Parameters.AddWithValue("@speed", speed);
                comand.Parameters.AddWithValue("@type", type);
                try
                {
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Данные успешно добавлены!");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка при добавлении данных", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                connection.Close();
            }
        }
        public void Add_client(string FIO, string pasport)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                comand = new SQLiteCommand($"INSERT INTO client(FIO, pasport) VALUES(@FIO, @pasport)", connection);
                comand.Parameters.AddWithValue("@FIO", FIO);
                comand.Parameters.AddWithValue("@pasport", pasport);
                try
                {
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Данные успешно добавлены!");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка при добавлении данных", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                connection.Close();
            }
        }
        public void Add_employee(string FIO, string pasport)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                comand = new SQLiteCommand($"INSERT INTO employee(FIO, pasport) VALUES(@FIO, @pasport)", connection);
                comand.Parameters.AddWithValue("@FIO", FIO);
                comand.Parameters.AddWithValue("@pasport", pasport);
                try
                {
                    comand.ExecuteNonQuery();
                    MessageBox.Show("Данные успешно добавлены!");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка при добавлении данных", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                connection.Close();
            }
        }


        public void Delete(string ID, string delsql)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();


                using (SQLiteCommand command = new SQLiteCommand(delsql, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные успешно удалены из таблицы.");
                        }
                        else
                        {
                            MessageBox.Show("Элемент с указанным ID не найден.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении данных: {ex.Message}");
                    }
                }
            }
        }

    }
}    



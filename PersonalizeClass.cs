using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using System.Data;
using System.Drawing.Text;

namespace DepersonalizationLR
{
    internal class PersonalizeClass
    {
        public void Personalize() // Деобезличивание
        {
            // Подключение по строке из конфига
            using (NpgsqlConnection connection = new NpgsqlConnection(
                ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString)) 
            {
                connection.Open(); // Открытие соединения
                string selectQuery = "SELECT f.*, a.*, g.* " +
                         "FROM  fullnametable AS f " +
                         "INNER JOIN addresstable AS a ON f.id_fullnametable = a.id_addressdata " +
                         "INNER JOIN gendertable AS g ON f.id_fullnametable = g.id_gendertable";
                try
                {
                    using (NpgsqlCommand selectCommand = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(selectCommand))
                        {
                            DataTable resultDataTable = new DataTable();
                            adapter.Fill(resultDataTable);

                            // Теперь resultDataTable содержит данные из трех таблиц на основе идентификаторов
                            // Вставка эти данных в таблицу personaldata
                            foreach (DataRow row in resultDataTable.Rows)
                            {

                                using (NpgsqlCommand insertCommand = new NpgsqlCommand(
                                    "INSERT INTO personaldata (lastname, firstname, surname, phonenumber, registrationaddress, birthdate, sex) " +
                                    "VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7)", connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@value1", row["lastname"]);
                                    insertCommand.Parameters.AddWithValue("@value2", row["firstname"]);
                                    insertCommand.Parameters.AddWithValue("@value3", row["surname"]);
                                    insertCommand.Parameters.AddWithValue("@value4", row["phonenumber"]);
                                    insertCommand.Parameters.AddWithValue("@value5", row["registrationaddress"]);
                                    insertCommand.Parameters.AddWithValue("@value6", row["birthdate"]);
                                    insertCommand.Parameters.AddWithValue("@value7", row["sex"]);

                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    TableCleaning();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void TableCleaning() 
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(
                ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString)) 
            {
                connection.Open();
                string deleteQuery = "DELETE FROM combineddata;" +
                    "\nDELETE FROM fullnametable;" +
                    "\nDELETE FROM gendertable;" +
                    "\nDELETE FROM addresstable;";
                string restartSequenceQuery = "ALTER SEQUENCE fullnametable_id_fullnametable_seq RESTART WITH 1;\n" +
                    "ALTER SEQUENCE addresstable_id_addressdata_seq RESTART WITH 1\n;" +
                    "ALTER SEQUENCE gendertable_id_gendertable_seq RESTART WITH 1\n;" +
                    "ALTER SEQUENCE combineddata_id_combineddata_seq RESTART WITH 1\n;";
                try
                {
                    using (NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.ExecuteNonQuery();
                    }
                    using (NpgsqlCommand restartSequenceCommand = new NpgsqlCommand(restartSequenceQuery, connection))
                    {
                        restartSequenceCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
                finally 
                { 
                    connection.Close(); 
                }
            }
        }
    }
}

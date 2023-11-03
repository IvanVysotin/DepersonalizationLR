using Npgsql;
using NpgsqlTypes;
using System.Configuration;

namespace DepersonalizationLR
{
    internal class DepersonalizeClass
    {
        
        // Запрос на вставку в таблицу с ФИО
        private string _insertFullnameQuery = "INSERT INTO public.fullnametable (lastname, firstname, surname)" +
            "SELECT  lastname, firstname, surname FROM public.personaldata;";

        // Запрос на вставку в таблицу с адресом и телефоном
        private string _insertAddressQuery = "INSERT INTO public.addresstable (phonenumber, registrationaddress)" +
            "SELECT phonenumber, registrationaddress FROM public.personaldata;";

        // Запрос на вставку в таблицу с полом и датой рождения
        private string _insertGenderQuery = "INSERT INTO public.gendertable (birthdate, sex)" +
            "SELECT birthdate, sex FROM public.personaldata;";

        // Запрос для удаления данных из оригинальной таблицы
        private string _deleteQuery = "DELETE FROM public.personaldata;";

        //private string resetSequenceQuery = "ALTER SEQUENCE fullnametable_id_fullnametable_seq RESTART WITH 1";
        private string insertCombineQuery = "SELECT";
        public void Depersonalize() // Обезличивание
        {
            // Подключение по строке из конфига
            using (NpgsqlConnection connection = new NpgsqlConnection(
                ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString)) 
            {
                connection.Open(); // Открытие соединения
                try
                {
                    using (NpgsqlCommand insertFullname = new NpgsqlCommand(_insertFullnameQuery, connection))
                    {
                        insertFullname.ExecuteNonQuery();
                    };
                    using (NpgsqlCommand insertAddress = new NpgsqlCommand(_insertAddressQuery, connection))
                    {
                        insertAddress.ExecuteNonQuery();
                    };
                    using (NpgsqlCommand insertGender = new NpgsqlCommand(_insertGenderQuery, connection))
                    {
                        insertGender.ExecuteNonQuery();
                    };
                    using (NpgsqlCommand deleteData = new NpgsqlCommand(_deleteQuery, connection))
                    {
                        deleteData.ExecuteNonQuery();
                    };
                    Combine();
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

        private void Combine() // Соединение обезличенных данных
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(
                ConfigurationManager.ConnectionStrings["PostgreSQLConnection"].ConnectionString)) 
            {
                connection.Open();
                // Для Адреса
                List<int> addressID = new List<int>(); 
                using (NpgsqlCommand getAddressIDCommand = new NpgsqlCommand("SELECT id_addressdata FROM public.addresstable", connection))
                {
                    using (NpgsqlDataReader reader = getAddressIDCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            addressID.Add(reader.GetInt32(0));
                        }
                    }
                }

                // Для ФИО
                List<int> fullnameID = new List<int>();
                using (NpgsqlCommand getFullnameIDCommand = new NpgsqlCommand("SELECT id_fullnametable FROM public.fullnametable", connection))
                {
                    using (NpgsqlDataReader reader = getFullnameIDCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fullnameID.Add(reader.GetInt32(0));
                        }
                    }
                }

                // Для Гендера
                List<int> genderID = new List<int>();
                using (NpgsqlCommand getGenderIDCommand = new NpgsqlCommand("SELECT id_gendertable FROM public.gendertable", connection))
                {
                    using (NpgsqlDataReader reader = getGenderIDCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genderID.Add(reader.GetInt32(0));
                        }
                    }
                }

                string insertQuery = "INSERT INTO combineddata (id_fullnametable, id_addressdata, id_gendertable) " +
                         "VALUES (@idFullname, @idAddress, @idGender)";

                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.Add(new NpgsqlParameter("@idFullname", NpgsqlDbType.Integer));
                    insertCommand.Parameters.Add(new NpgsqlParameter("@idAddress", NpgsqlDbType.Integer));
                    insertCommand.Parameters.Add(new NpgsqlParameter("@idGender", NpgsqlDbType.Integer));

                    for (int i = 0; i < Math.Min(addressID.Count, Math.Min(fullnameID.Count, genderID.Count)); i++)
                    {
                        insertCommand.Parameters["@idFullname"].Value = fullnameID[i];
                        insertCommand.Parameters["@idAddress"].Value = addressID[i];
                        insertCommand.Parameters["@idGender"].Value = genderID[i];
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}

using MySql.Data.MySqlClient;
using System;

namespace SportShop
{
    class DB_2 : IDisposable
    {
        private MySqlConnection connection;

        // Конструктор для инициализации соединения с базой данных
        public DB_2()
        {
            connection = new MySqlConnection("server=localhost;port=8889;user=root;password=root;database=numberphone");
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        // Метод для сохранения номера телефона в таблицу numberphones
        public bool SavePhoneNumber(string phoneNumber)
        {
            // Подготовка SQL-запроса
            string query = "INSERT INTO numberphones (phone) VALUES (@phoneNumber)";
            using (var command = new MySqlCommand(query, GetConnection()))
            {
                command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                // Выполнение запроса и возвращение результата
                return command.ExecuteNonQuery() == 1;
            }
        }

        public void Dispose()
        {
            CloseConnection();
            connection.Dispose();
        }
    }
}

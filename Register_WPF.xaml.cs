using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System;
using MySqlX.XDevAPI;

namespace SportShop
{
    /// <summary>
    /// Логика взаимодействия для Register_WPF.xaml
    /// </summary>
    public partial class Register_WPF : Window
    {
        private DB_1 db;
        public Register_WPF()
        {
            InitializeComponent();
            db = new DB_1();
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox nameBox = sender as TextBox;

            if (nameBox != null && nameBox.Text == "Введите имя")
            {
                nameBox.Text = string.Empty;
            }
            nameBox.CaretIndex = nameBox.Text.Length;
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox nameBox = sender as TextBox;

            if (nameBox != null && string.IsNullOrWhiteSpace(nameBox.Text))
            {
                nameBox.Text = "Введите имя";
            }
        }

        private void LoginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox loginBox = sender as TextBox;

            if (loginBox != null && loginBox.Text == "Введите логин")
            {
                loginBox.Text = string.Empty;
            }
            loginBox.CaretIndex = loginBox.Text.Length;
        }

        private void LoginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox loginBox = sender as TextBox;

            if (loginBox != null && string.IsNullOrWhiteSpace(loginBox.Text))
            {
                loginBox.Text = "Введите логин";
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox passwordBox = sender as TextBox;

            if (passwordBox != null && passwordBox.Text == "Введите пароль")
            {
                passwordBox.Text = string.Empty;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox passwordBox = sender as TextBox;

            if (passwordBox != null && string.IsNullOrEmpty(passwordBox.Text))
            {
                passwordBox.Text = "Введите пароль";
            }
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {

            string name = NameBox.Text.Trim();
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Text.Trim();
            string validationMessage = string.Empty;

            // Валидация данных
            if (string.IsNullOrEmpty(name) || name == "Введите имя")
            {
                validationMessage += "Имя не должно быть пустым.\n";
            }

            if (string.IsNullOrEmpty(login) || login == "Введите логин")
            {
                validationMessage += "Логин не должен быть пустым.\n";
            }

            if (string.IsNullOrEmpty(password) || password == "Введите пароль")
            {
                validationMessage += "Пароль не должен быть пустым.\n";
            }

            // Проверка длины имени
            if (name.Length < 4)
            {
                validationMessage += "Имя должно содержать не менее 4-х символов.\n";
            }

            // Проверка длины логина
            if (login.Length < 4)
            {
                validationMessage += "Логин должен содержать не менее 4-х символов.\n";
            }

            // Проверка длины пароля
            if (password.Length < 6)
            {
                validationMessage += "Пароль должен содержать не менее 6 символов.\n";
            }
            // Дополнительные проверки пароля можно добавить здесь

            // Проверяем окончательное сообщение валидации
            if (string.IsNullOrEmpty(validationMessage))
            {
                RegisterUser(name, login, password);
            }
            else
            {
                MessageBox.Show(validationMessage.Trim(), "Ошибки валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegisterUser(string name, string login, string password)
        {
            try
            {
                db.OpenConnection();

                
                MySqlCommand checkCommand = new MySqlCommand("SELECT COUNT(*) FROM users WHERE login = @login", db.GetConnection());
                checkCommand.Parameters.AddWithValue("@login", login);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Этот логин уже занят. Пожалуйста, выберите другой логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                MySqlCommand command = new MySqlCommand("INSERT INTO users (name, login, password) VALUES (@name, @login, @password)", db.GetConnection());
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@password", password); // Используем открытый пароль

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Вы зарегистрировались", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Не получилось зарегистрироваться", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        // Метод для проверки простоты пароля
        private bool IsSimpleSequence(string password)
        {
            // Выражение для проверки последовательности
            string pattern = @"^(?:(\d|[a-zA-Z])\1{1,5}|abcde|abcdef|123456)$";
            return Regex.IsMatch(password, pattern);
        }





        private void PasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Number_Writer_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
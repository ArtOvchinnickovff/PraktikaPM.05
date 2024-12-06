using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SportShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DB_1 db;
        public MainWindow()
        {
            InitializeComponent();
            db = new DB_1();
        }

        
        private void Login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "Введите логин")
            {
                Login.Text = string.Empty; 
            }
        }

        
        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Login.Text))
            {
                Login.Text = "Введите логин"; 
            }
        }

        // Обработчик события GotFocus для PasswordBox
        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            
            if (Password.Text == "Введите пароль")
            {
                Password.Text = string.Empty; // Очищаем текстовое поле
            }
        }

        // Обработчик события LostFocus для PasswordBox
        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Password.Text))
            {
                Password.Text = "Введите пароль"; 
            }
        }




        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            Register_WPF register_WPF = new Register_WPF();
            register_WPF.Show();
            this.Close();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем и обрезаем текст из текстбоксов
            string login = Login.Text.Trim();
            string password = Password.Text.Trim();
            string validationMessage = string.Empty;

            // Проверка на пустые значения и текст подсказки
            if (string.IsNullOrEmpty(login) || login == "Введите логин")
            {
                validationMessage += "Логин не должен быть пустым.\n";
            }

            if (string.IsNullOrEmpty(password) || password == "Введите пароль")
            {
                validationMessage += "Пароль не должен быть пустым.\n";
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

            // Проверяем окончательное сообщение валидации
            if (string.IsNullOrEmpty(validationMessage))
            {

                {
                    db.OpenConnection(); // Открываем соединение

                    // Создаем SQL-команду для проверки логина и пароля
                    MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM users WHERE login = @login AND password = @password", db.GetConnection());
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0) // Если такая запись найдена
                    {
                        MessageBox.Show("Вы успешно вошли в систему", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Переход к следующему окну (например, в магазин)
                        Shop shop = new Shop();
                        shop.Show();
                        this.Close(); // Закрываем текущее окно авторизации
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    db.CloseConnection(); // Закрываем соединение
                }
            }
            else
            {
                MessageBox.Show(validationMessage.Trim(), "Ошибки валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

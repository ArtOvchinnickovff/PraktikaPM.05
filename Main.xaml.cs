using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SportShop
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Register_WPF register_WPF = new Register_WPF();
            register_WPF.Show();
        }

        private void Gost_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Shop shop = new Shop();
            shop.Show();
        }
    }
}

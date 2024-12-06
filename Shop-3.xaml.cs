﻿using System;
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
    /// Логика взаимодействия для Shop_3.xaml
    /// </summary>
    public partial class Shop_3 : Window
    {
        public Shop_3()
        {
            InitializeComponent();
        }

        private void One_Button_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = new Shop();
            shop.Show();
            this.Close();
        }

        private void Two_Buton_Click(object sender, RoutedEventArgs e)
        {
            Shop_2 shop_2 = new Shop_2();
            shop_2.Show();
            this.Close();
        }

        private void Three_Button_Click(object sender, RoutedEventArgs e)
        {
            Shop_3 shop_3 = new Shop_3();
            shop_3.Show();
            this.Close();
        }

        private void Sale_1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ваш заказ создан, ожидайте звонка.");
        }
    }
}
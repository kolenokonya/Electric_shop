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

namespace Electric_shop
{
    /// <summary>
    /// Логика взаимодействия для Personal.xaml
    /// </summary>
    public partial class Personal : Window
    {
        public Personal()
        {
            InitializeComponent();
        }

        private void AddKlient_Click(object sender, RoutedEventArgs e)
        {
            new Klient().Show();
            this.Close();
        }

        private void AddChek_Click(object sender, RoutedEventArgs e)
        {
            new Chek().Show();
            this.Close();
        }

        private void AddOtch_Click(object sender, RoutedEventArgs e)
        {
            new Otchet().Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}

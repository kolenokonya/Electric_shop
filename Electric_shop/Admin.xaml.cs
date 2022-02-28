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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void AddPersonal_Click(object sender, RoutedEventArgs e)
        {
            new AddPers().Show();
            this.Close();
        }

        private void AddTovar_Click(object sender, RoutedEventArgs e)
        {
            new Tovar().Show();
            this.Close();
        }

        private void AddKateg_Click(object sender, RoutedEventArgs e)
        {
            new Kategorii().Show();
            this.Close();
        }

        private void AddSklad_Click(object sender, RoutedEventArgs e)
        {
            new Sklad().Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();   
        }
    }
}

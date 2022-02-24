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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Electric_shop.DataBaseTableAdapters;


namespace Electric_shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "admin" && Password.Password == "admin")
            {
                new Admin().Show();
                this.Close();
                return;
            }

            PersonalTableAdapter pers = new PersonalTableAdapter();

            List<string> Log = new List<string>();
            for (int i = 0; i < pers.GetData().Count(); i++) Log.Add(pers.GetData().Rows[i][4].ToString() + pers.GetData().Rows[i][5].ToString());

            for (int i = 0; i < Log.Count(); i++)
            {
                if (Log[i] == Login.Text + Password.Password)
                {
                    new Personal().Show();
                    this.Close();
                }
            }
        }
    }
}

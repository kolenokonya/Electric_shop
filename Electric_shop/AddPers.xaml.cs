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
using Electric_shop.DataBaseTableAdapters;

namespace Electric_shop
{
    /// <summary>
    /// Логика взаимодействия для AddPers.xaml
    /// </summary>
    public partial class AddPers : Window
    {
        PersonalTableAdapter pers = new PersonalTableAdapter();
        DataBase.PersonalDataTable user = new DataBase.PersonalDataTable();

        public AddPers()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            new Admin().Show();
            this.Close();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(user.Where())


                if (BDay.SelectedDate <= DateTime.Now)
                {
                    pers.InsertQuery(Familiya.Text,Imya.Text,Otchestvo.Text,Login.Text,Password.Text,SeriaPass.Text,NomerPass.Text,BDay.Text,Adres.Text,Telefon.Text,2);

                    new Admin().Show();
                    this.Close();
                }

            }
            catch { MessageBox.Show("Что-то введено не так"); }
        }
    }
}

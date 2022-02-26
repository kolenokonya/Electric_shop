using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Data.SqlClient;
using Electric_shop.DataBaseTableAdapters;

namespace Electric_shop
{
    /// <summary>
    /// Логика взаимодействия для AddPers.xaml
    /// </summary>
    public partial class Chek : Window
    {
        ChekTableAdapter pers = new ChekTableAdapter();
        DataBase.ChekDataTable user = new DataBase.ChekDataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["Electric_shop.Properties.Settings.Electric_shopConnectionString"].ConnectionString;

        public Chek()
        {
            InitializeComponent();
            Check();
            Person();
            Klienton();
            Tovar();
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
                if (Date.SelectedDate <= DateTime.Now)
                {
                    pers.InsertQuery(Date.Text, Convert.ToInt32(Combo.SelectedValue),Convert.ToInt32(Pers.SelectedValue), Convert.ToInt32(Klient.SelectedValue));

                    new Admin().Show();
                    this.Close();
                }


            }
            catch { MessageBox.Show("Что-то введено не так"); }
        }



        private void Check()
        {
            Chek_ViewTableAdapter adapter = new Chek_ViewTableAdapter();
            DataBase.Chek_ViewDataTable table = new DataBase.Chek_ViewDataTable();
            adapter.Fill(table);
            LB.ItemsSource = table;
            LB.DisplayMemberPath = "Данные чека";
            LB.SelectedValuePath = "Код чека";
        }

        private void Tovar()
        {
            Tovar_ViewTableAdapter adapter = new Tovar_ViewTableAdapter();
            DataBase.Tovar_ViewDataTable table = new DataBase.Tovar_ViewDataTable();
            adapter.Fill(table);
            Combo.ItemsSource = table;
            Combo.DisplayMemberPath = "Наименование товара";
            Combo.SelectedValuePath = "Код товара";
        }

        private void Person()
        {
            Personal_ViewTableAdapter adapter = new Personal_ViewTableAdapter();
            DataBase.Personal_ViewDataTable table = new DataBase.Personal_ViewDataTable();
            adapter.Fill(table);
            Pers.ItemsSource = table;
            Pers.DisplayMemberPath = "ФИО пользователя";
            Pers.SelectedValuePath = "Код пользователя";
        }

        private void Klienton()
        {
            Klient_ViewTableAdapter adapter = new Klient_ViewTableAdapter();
            DataBase.Klient_ViewDataTable table = new DataBase.Klient_ViewDataTable();
            adapter.Fill(table);
            Klient.ItemsSource = table;
            Klient.DisplayMemberPath = "ФИО клиента";
            Klient.SelectedValuePath = "Код клиента";
        }

        public void Clear()
        {
            Date.Text = "";
            Klient.Text = "";
            Combo.Text = "";
            Pers.Text = "";
        }



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChekTableAdapter pers = new ChekTableAdapter();
                pers.DeleteQuery(Convert.ToInt32(LB.SelectedValue));
                LB.ItemsSource = new List<String>();

                Person();
            }
            catch { MessageBox.Show("Сначала необоходимо выбрать удаляемый объект"); }
        }

        private void Polzovateli_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LB.SelectedItem != null)
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {

                    connect.Open();
                    SqlCommand command = new SqlCommand($"SELECT*FROM chek Where ID_chek={LB.SelectedValue}", connect);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {


                        Date.Text = reader["Data_prodazhi"].ToString();
                        Klient.SelectedValue = Convert.ToInt32(reader["Klient_ID"]);
                        Combo.SelectedValue = Convert.ToInt32(reader["Tovar_ID"]);
                        Pers.SelectedValue = Convert.ToInt32(reader["Pers_ID"]);


                    }
                    reader.Close();
                }
            }
        }
    }
}

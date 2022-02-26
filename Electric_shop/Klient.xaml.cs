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
    public partial class Klient : Window
    {
        KlientTableAdapter pers = new KlientTableAdapter();
        DataBase.KlientDataTable user = new DataBase.KlientDataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["Electric_shop.Properties.Settings.Electric_shopConnectionString"].ConnectionString;

        public Klient()
        {
            InitializeComponent();
            Personal();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            new Personal().Show();
            this.Close();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                    pers.InsertQuery(Familiya.Text, Imya.Text, Otchestvo.Text, Telefon.Text);

                new Personal().Show();
                this.Close();

            }
            catch { MessageBox.Show("Что-то введено не так"); }
        }



        private void Personal()
        {
            Klient_ViewTableAdapter adapter = new Klient_ViewTableAdapter();
            DataBase.Klient_ViewDataTable table = new DataBase.Klient_ViewDataTable();
            adapter.Fill(table);
            LB.ItemsSource = table;
            LB.DisplayMemberPath = "ФИО клиента";
            LB.SelectedValuePath = "Код клиента";
        }

        public void Clear()
        {
            Familiya.Text = "";
            Imya.Text = "";
            Otchestvo.Text = "";
            Telefon.Text = "";
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Familiya.Text != "" && Imya.Text != ""  && Telefon.Text != "" && LB.SelectedValue != null)
                {
                    new KlientTableAdapter().UpdateQuery(Familiya.Text, Imya.Text, Otchestvo.Text,  Telefon.Text, Convert.ToInt32(LB.SelectedValue));
                    Personal();
                    Clear();
                }
                else MessageBox.Show("Ошибка ввода данных1");
            }
            catch
            {
                MessageBox.Show("Ошибка ввода данных");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                KlientTableAdapter pers = new KlientTableAdapter();
                pers.DeleteQuery(Convert.ToInt32(LB.SelectedValue));
                LB.ItemsSource = new List<String>();

                Personal();
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
                    SqlCommand command = new SqlCommand($"SELECT*FROM Klient Where ID_Klient={LB.SelectedValue}", connect);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Familiya.Text = reader["Familiya"].ToString();
                        Imya.Text = reader["Imya"].ToString();
                        Otchestvo.Text = reader["Otchestvo"].ToString();
                        Telefon.Text = reader["Nomer_telefona"].ToString();
                    }
                    reader.Close();
                }
            }
        }
    }
}
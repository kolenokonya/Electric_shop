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
    public partial class Sklad : Window
    {
        SkladTableAdapter pers = new SkladTableAdapter();
        DataBase.SkladDataTable user = new DataBase.SkladDataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["Electric_shop.Properties.Settings.Electric_shopConnectionString"].ConnectionString;

        public Sklad()
        {
            InitializeComponent();
            Personal();
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

                pers.InsertQuery(Skladon.Text,Adres.Text);

                new Admin().Show();
                this.Close();

            }
            catch { MessageBox.Show("Что-то введено не так"); }
        }



        private void Personal()
        {
            Sklad_ViewTableAdapter adapter = new Sklad_ViewTableAdapter();
            DataBase.Sklad_ViewDataTable table = new DataBase.Sklad_ViewDataTable();
            adapter.Fill(table);
            LB.ItemsSource = table;
            LB.DisplayMemberPath = "Наименование склада";
            LB.SelectedValuePath = "Код склада";
        }


        public void Clear()
        {
            Skladon.Text = "";
            Adres.Text = "";
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Skladon.Text != "" && LB.SelectedValue != null && Adres.Text!="")
                {
                    new SkladTableAdapter().UpdateQuery(Skladon.Text,Adres.Text, Convert.ToInt32(LB.SelectedValue));
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
                SkladTableAdapter pers = new SkladTableAdapter();
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
                    SqlCommand command = new SqlCommand($"SELECT*FROM Sklad Where ID_Sklad={LB.SelectedValue}", connect);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {


                        Skladon.Text = reader["Naimenovamie_sklada"].ToString();
                        Adres.Text = reader["Adres_sklada"].ToString();



                    }
                    reader.Close();
                }
            }
        }
    }
}

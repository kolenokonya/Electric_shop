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
    public partial class Kategorii : Window
    {
        Kategoria_tovaraTableAdapter pers = new Kategoria_tovaraTableAdapter();
        DataBase.Kategoria_tovaraDataTable user = new DataBase.Kategoria_tovaraDataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["Electric_shop.Properties.Settings.Electric_shopConnectionString"].ConnectionString;

        public Kategorii()
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

                pers.InsertQuery(Kateg.Text);

                new Admin().Show();
                this.Close();

            }
            catch { MessageBox.Show("Что-то введено не так"); }
        }



        private void Personal()
        {
            Kategoria_ViewTableAdapter adapter = new Kategoria_ViewTableAdapter();
            DataBase.Kategoria_ViewDataTable table = new DataBase.Kategoria_ViewDataTable();
            adapter.Fill(table);
            LB.ItemsSource = table;
            LB.DisplayMemberPath = "Наименование товара";
            LB.SelectedValuePath = "Код товара";
        }


        public void Clear()
        {
            Kateg.Text = "";
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Kateg.Text != "" && LB.SelectedValue != null)
                {
                    new Kategoria_tovaraTableAdapter().UpdateQuery(Kateg.Text, Convert.ToInt32(LB.SelectedValue));
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
                Kategoria_tovaraTableAdapter pers = new Kategoria_tovaraTableAdapter();
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
                    SqlCommand command = new SqlCommand($"SELECT*FROM Kategoria_tovara Where ID_Kategoria={LB.SelectedValue}", connect);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {


                        Kateg.Text = reader["Naimenovamie_kategorii_tovara"].ToString();



                    }
                    reader.Close();
                }
            }
        }
    }
}

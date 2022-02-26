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
    public partial class Tovar : Window
    {
        TovarTableAdapter pers = new TovarTableAdapter();
        DataBase.TovarDataTable user = new DataBase.TovarDataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["Electric_shop.Properties.Settings.Electric_shopConnectionString"].ConnectionString;

        public Tovar()
        {
            InitializeComponent();
            Personal();
            Kategoria();
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

                    pers.InsertQuery(Naimenovanie.Text, Cena.Text,Convert.ToInt32(Combo.SelectedValue));

                    new Personal().Show();
                    this.Close();

            }
            catch { MessageBox.Show("Что-то введено не так"); }
        }



        private void Personal()
        {
            Tovar_ViewTableAdapter adapter = new Tovar_ViewTableAdapter();
            DataBase.Tovar_ViewDataTable table = new DataBase.Tovar_ViewDataTable();
            adapter.Fill(table);
            LB.ItemsSource = table;
            LB.DisplayMemberPath = "Наименование товара";
            LB.SelectedValuePath = "Код товара";
        }

        private void Kategoria()
        {
            Kategoria_ViewTableAdapter adapter = new Kategoria_ViewTableAdapter();
            DataBase.Kategoria_ViewDataTable table = new DataBase.Kategoria_ViewDataTable();
            adapter.Fill(table);
            Combo.ItemsSource = table;
            Combo.DisplayMemberPath = "Наименование товара";
            Combo.SelectedValuePath = "Код товара";
        }

        public void Clear()
        {
            Naimenovanie.Text = "";
            Cena.Text = "";
            Combo.Text="";
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (Naimenovanie.Text != "" && Cena.Text != "" &&  LB.SelectedValue != null&& Combo.SelectedValue != null)
                {
                    new TovarTableAdapter().UpdateQuery(Naimenovanie.Text, Convert.ToInt32(Cena.Text).ToString(), Convert.ToInt32(Combo.SelectedValue), Convert.ToInt32(LB.SelectedValue));
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
                TovarTableAdapter pers = new TovarTableAdapter();
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
                    SqlCommand command = new SqlCommand($"SELECT*FROM Tovar Where ID_tovar={LB.SelectedValue}", connect);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {   


                        Naimenovanie.Text = reader["Naimenovanie_tovara"].ToString();
                        Cena.Text = reader["Cena"].ToString();
                        Combo.SelectedValue = Convert.ToInt32(reader["Kategoria_ID"]);


                    }
                    reader.Close();
                }
            }
        }
    }
}

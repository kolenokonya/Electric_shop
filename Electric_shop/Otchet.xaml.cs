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
    public partial class Otchet : Window
    {
        Otchet_o_PostupleniiTableAdapter pers = new Otchet_o_PostupleniiTableAdapter();
        string connectionString = ConfigurationManager.ConnectionStrings["Electric_shop.Properties.Settings.Electric_shopConnectionString"].ConnectionString;

        public Otchet()
        {
            InitializeComponent();
            Check();
            Person();
            Skladon();
            Tovaron();
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
                if (Date.SelectedDate <= DateTime.Now)
                {
                    pers.InsertQuery(Convert.ToInt32(Kolich.Text),Date.Text, Convert.ToInt32(Tovar.SelectedValue), Convert.ToInt32(Pers.SelectedValue), Convert.ToInt32(Sklad.SelectedValue));

                    new Personal().Show();
                    this.Close();
                }


            }
            catch { MessageBox.Show("Что-то введено не так"); }
        }



        private void Check()
        {
            Otchet_ViewTableAdapter adapter = new Otchet_ViewTableAdapter();
            DataBase.Otchet_ViewDataTable table = new DataBase.Otchet_ViewDataTable();
            adapter.Fill(table);
            LB.ItemsSource = table;
            LB.DisplayMemberPath = "Данные отчета";
            LB.SelectedValuePath = "Код отчета";
        }

        private void Tovaron()
        {
            Tovar_ViewTableAdapter adapter = new Tovar_ViewTableAdapter();
            DataBase.Tovar_ViewDataTable table = new DataBase.Tovar_ViewDataTable();
            adapter.Fill(table);
            Tovar.ItemsSource = table;
            Tovar.DisplayMemberPath = "Наименование товара";
            Tovar.SelectedValuePath = "Код товара";
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

        private void Skladon()
        {
            Sklad_ViewTableAdapter adapter = new Sklad_ViewTableAdapter();
            DataBase.Sklad_ViewDataTable table = new DataBase.Sklad_ViewDataTable();
            adapter.Fill(table);
            Sklad.ItemsSource = table;
            Sklad.DisplayMemberPath = "Наименование склада";
            Sklad.SelectedValuePath = "Код склада";
        }

        public void Clear()
        {
            Date.Text = "";
            Kolich.Text = "";
            Sklad.Text = "";
            Pers.Text = "";
            Tovar.Text = "";
        }



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Otchet_o_PostupleniiTableAdapter pers = new Otchet_o_PostupleniiTableAdapter();
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
                    SqlCommand command = new SqlCommand($"SELECT*FROM Otchet_o_Postuplenii Where ID_otchet={LB.SelectedValue}", connect);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        Date.Text = reader["Data_postavki"].ToString();
                        Kolich.Text = reader["Kolichestvo_tovara"].ToString();
                        Sklad.SelectedValue = Convert.ToInt32(reader["Sklad_ID"]);
                        Pers.SelectedValue = Convert.ToInt32(reader["Pers_id"]);
                        Tovar.SelectedValue = Convert.ToInt32(reader["Tovar_ID"]);


                    }
                    reader.Close();
                }
            }
        }
    }
}

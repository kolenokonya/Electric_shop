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
using static Electric_shop.DataBase;

namespace Electric_shop
{
    /// <summary>
    /// Логика взаимодействия для AddPers.xaml
    /// </summary>
    public partial class AddPers : Window
    {
        PersonalTableAdapter pers = new PersonalTableAdapter();
        DataBase.PersonalDataTable user = new DataBase.PersonalDataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["Electric_shop.Properties.Settings.Electric_shopConnectionString"].ConnectionString;



        public AddPers()
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
            pers.Fill(user);

            PersonalRow currentUser = user.Where(P => P.Login_Pers == Login.Text).FirstOrDefault();
            try
            {
                if(currentUser==null)
                {
                    if (BDay.SelectedDate <= DateTime.Now)
                    {
                        pers.InsertQuery(Familiya.Text,Imya.Text,Otchestvo.Text,Login.Text,Password.Text,SeriaPass.Text,NomerPass.Text,BDay.Text,Adres.Text,Telefon.Text,2);

                        new Admin().Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Такой логин уже существует");
                }


            }
            catch { MessageBox.Show("Что-то введено не так"); }
        }



        private void Personal()
        {
            Personal_ViewTableAdapter adapter = new Personal_ViewTableAdapter();
            DataBase.Personal_ViewDataTable table = new DataBase.Personal_ViewDataTable();
            adapter.Fill(table);
            LB.ItemsSource = table;
            LB.DisplayMemberPath = "ФИО пользователя";
            LB.SelectedValuePath = "Код пользователя";
        }

        public void Clear()
        {
            Familiya.Text = "";
            Imya.Text = "";
            Otchestvo.Text = "";
            Login.Text = "";
            Password.Text = "";
            SeriaPass.Text = "";
            NomerPass.Text = "";
            Adres.Text = "";
            Telefon.Text = "";
            BDay.Text = "";
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            pers.Fill(user);

            PersonalRow currentUser = user.Where(P => P.Login_Pers == Login.Text).FirstOrDefault();
            try
            {
                if (currentUser == null)
                {
                    if (Familiya.Text != "" && Imya.Text != "" && Login.Text != "" && Convert.ToDateTime(BDay.Text) < DateTime.Now && Password.Text != "" && SeriaPass.Text != "" && NomerPass.Text != "" && Convert.ToString(Convert.ToDateTime(BDay.Text)) != "" && Adres.Text != "" && Telefon.Text != "" && LB.SelectedValue != null)
                    {
                        new PersonalTableAdapter().UpdateQuery(Familiya.Text, Imya.Text, Otchestvo.Text, Login.Text, Password.Text, SeriaPass.Text, NomerPass.Text, BDay.Text, Adres.Text, Telefon.Text, 2, Convert.ToInt32(LB.SelectedValue));
                        Personal();
                        Clear();
                    }
                    else MessageBox.Show("Ошибка ввода данных1");
                }
                else
                {
                    MessageBox.Show("Такой логин уже существует");
                }
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
                PersonalTableAdapter pers = new PersonalTableAdapter();
                pers.DeleteQuery(Convert.ToInt32(LB.SelectedValue));
                LB.ItemsSource = new List<String>();

                Personal();
            }
            catch { MessageBox.Show("Сначала необоходимо выбрать удаляемый объект"); }
        }

        private void Polzovateli_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(LB.SelectedItem!=null)
            {
                using (SqlConnection connect = new SqlConnection(connectionString)) 
                {
                    
                    connect.Open();
                    SqlCommand command = new SqlCommand($"SELECT*FROM Personal Where ID_Pers={LB.SelectedValue}",connect);
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        Familiya.Text = reader["Familia_Pers"].ToString();
                        Imya.Text = reader["Imya_Pers"].ToString();
                        Otchestvo.Text = reader["Otchestvo_Pers"].ToString();
                        Login.Text = reader["Login_Pers"].ToString();
                        Password.Text = reader["Pass_Pers"].ToString();
                        SeriaPass.Text = reader["Ser_Pass"].ToString();
                        NomerPass.Text = reader["Nom_Pass"].ToString();
                        Adres.Text = reader["Adres"].ToString();
                        Telefon.Text = reader["Telefon_Pers"].ToString();
                        BDay.Text = reader["Data_Roj"].ToString();
                        

                    }
                    reader.Close();
                }
            }
        }
    }
}

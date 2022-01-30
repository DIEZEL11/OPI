
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string connectionString = @"Data Source=DIZZEL;Initial Catalog=OPI;Integrated Security=True;";
        private string connectionString = @"Data Source = opi.db";
        public MainWindow()
        {
            InitializeComponent();
            FillClient();
            FillTrener();
        }
        public void FillClient()
        {


            try
            {


                string sql = "select client.id,client.fio,client.DATE,(Trener.FIO) as 'FIO Trener' from client left join Trener on Trener.id = client.IdTrener";

                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    using (SqliteCommand cmdSel = new SqliteCommand(sql, connection))
                    {


                        DataTable dt = new DataTable();
                        dt.Load(cmdSel.ExecuteReader());

                        Client_Grid.DataContext = dt;
                    }

                    connection.Close();
                }
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
        }
        public void FillTrener()
        {
            try
            {


                string sql = "SELECT * FROM Trener";

                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    using (SqliteCommand cmdSel = new SqliteCommand(sql, connection))
                    {


                        DataTable dt = new DataTable();
                        dt.Load(cmdSel.ExecuteReader());

                        Trener_Grid.DataContext = dt;
                    }

                    connection.Close();
                }
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
        }
        public class client
        {
            public string Fio { get; set; }
            public int index { get; set; }
            public client(string d, int h)
            {
                Fio = d;
                index = h;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SqliteConnection sqlConnection1 = new SqliteConnection(connectionString);
            AddTrener s = new AddTrener();
            try
            {
                if (s.ShowDialog() == true)
                {



                    SqliteCommand cmd = new SqliteCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Trener (FIO,Date) VALUES ('" + s.Fio.Text + "',' " + s.Date + "')"; ;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection1.Close();

                }
                FillTrener();
            }

            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqliteConnection sqlConnection1 = new SqliteConnection(connectionString);

            List<client> c = new List<client> { };
            SqliteCommand com = new SqliteCommand("select * from trener", sqlConnection1);
            try
            {
                AddClient s = new AddClient();
                sqlConnection1.Open();
                SqliteDataReader reader;
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        int result2 = 0;
                        string result = reader.GetString(1);
                        if (!reader.IsDBNull(0))
                            result2 = reader.GetInt16(0);

                        c.Add(new client(result, result2));


                    }
                    catch (Exception f)
                    {
                        MessageBox.Show(f.ToString());
                    }

                }
                s.Client.ItemsSource = c;


                sqlConnection1.Close();
                if (s.ShowDialog() == true)
                {


                    SqliteCommand cmd = new SqliteCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO client (FIO,Date,idTrener ) VALUES ('" + s.Fio.Text + "',' " + s.Date + "'," + s.Client.SelectedValue + ")"; ;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection1.Close();

                }
                FillClient();
            }

            catch (Exception b)
            {
                MessageBox.Show(b.ToString());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)Client_Grid.SelectedItem;
            //int index = Client_Grid.CurrentCell.Column.DisplayIndex;
            if (dataRow != null)
            {
                string cellValue = dataRow.Row.ItemArray[0].ToString();
                SqliteConnection sqlConnection1 = new SqliteConnection(connectionString);


                SqliteCommand cmd = new SqliteCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "DELETE FROM client WHERE id=" + cellValue + "";
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                FillClient();
            }
            else
                MessageBox.Show("Выберети строку");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)Trener_Grid.SelectedItem;
            //int index = Client_Grid.CurrentCell.Column.DisplayIndex;
            if (dataRow != null)
            {
                string cellValue = dataRow.Row.ItemArray[0].ToString();
                SqliteConnection sqlConnection1 = new SqliteConnection(connectionString);


                SqliteCommand cmd = new SqliteCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM trener WHERE id=" + cellValue + "";
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                FillTrener();

            }
            else
                MessageBox.Show("Выберети строку");
        }
        private void textBox1_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {


                string sql = "select client.id,client.fio,client.DATE,(Trener.FIO) as 'FIO Trener' from client left join Trener on Trener.id = client.IdTrener where client.Fio like '%" + clintsearch.Text + "%' or Trener.Fio like '%" + clintsearch.Text + "%'";

                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    using (SqliteCommand cmdSel = new SqliteCommand(sql, connection))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(cmdSel.ExecuteReader());
                        Client_Grid.DataContext = dt;
                    }
                    connection.Close();
                }
            }
            catch (Exception b) { MessageBox.Show(b.ToString()); }
        }
        private void textBox2_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {


                string sql = "SELECT * FROM Trener where Fio like '%" + Trenersearch.Text + "%'";

                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    using (SqliteCommand cmdSel = new SqliteCommand(sql, connection))
                    {
                        DataTable dt = new DataTable();
                        dt.Load(cmdSel.ExecuteReader());

                        Trener_Grid.DataContext = dt;
                    }
                    connection.Close();
                }
            }
            catch (Exception b) { MessageBox.Show(b.ToString()); }
        }

        private void Client_Grid_CurrentCellChanged(object sender, EventArgs e)
        {
            DataRowView dataRow = (DataRowView)Trener_Grid.SelectedItem;
            //int index = Client_Grid.CurrentCell.Column.DisplayIndex;
            string cellValue = dataRow.Row.ItemArray[0].ToString();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)Client_Grid.SelectedItem;
            //int index = Client_Grid.CurrentCell.Column.DisplayIndex;
            if (dataRow != null)
            {
                string cellValue = dataRow.Row.ItemArray[0].ToString();
                SqliteConnection sqlConnection1 = new SqliteConnection(connectionString);

                List<client> c = new List<client> { };
                SqliteCommand com = new SqliteCommand("select * from trener", sqlConnection1);
                try
                {
                    AddClient s = new AddClient();
                    sqlConnection1.Open();
                    SqliteDataReader reader;
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            int result2 = 0;
                            string result = reader.GetString(1);
                            if (!reader.IsDBNull(0))
                                result2 = reader.GetInt16(0);

                            c.Add(new client(result, result2));


                        }
                        catch (Exception f)
                        {
                            MessageBox.Show(f.ToString());
                        }

                    }
                    s.Client.ItemsSource = c;


                    sqlConnection1.Close();
                    if (s.ShowDialog() == true)
                    {


                        SqliteCommand cmd = new SqliteCommand();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = "UPDATE client set FIO='" + s.Fio.Text + "', date='" + s.Date + "', idTrener=" + s.Client.SelectedValue + " where id="+ cellValue + "";
                        cmd.Connection = sqlConnection1;

                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();

                    }
                    FillClient();

                }

                catch (Exception b)
                {
                    MessageBox.Show(b.ToString());
                }
            }
            else
            MessageBox.Show("Выберети строку");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)Trener_Grid.SelectedItem;
            //int index = Client_Grid.CurrentCell.Column.DisplayIndex;
            if (dataRow != null)
            {
                string cellValue = dataRow.Row.ItemArray[0].ToString();
                SqliteConnection sqlConnection1 = new SqliteConnection(connectionString);
                AddTrener s = new AddTrener();
                try
                {
                    if (s.ShowDialog() == true)
                    {



                        SqliteCommand cmd = new SqliteCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE Trener set FIO='" + s.Fio.Text + "', date='" + s.Date + "'where id=" + cellValue + ""; 
                        cmd.Connection = sqlConnection1;

                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();

                    }
                    FillTrener();
                }

                catch (Exception a)
                {
                    MessageBox.Show(a.ToString());
                }
            }
            else
                MessageBox.Show("Выберети строку");

        }
    }
}


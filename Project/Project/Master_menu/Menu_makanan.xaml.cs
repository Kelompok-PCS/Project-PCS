using Oracle.DataAccess.Client;
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
using System.Data;
using System.Windows.Threading;

namespace Project.Master_menu
{
    /// <summary>
    /// Interaction logic for InsertMenu.xaml
    /// </summary>
    public partial class Menu_makanan : Window
    {
        OracleConnection conn;
        DispatcherTimer timer;
        public Menu_makanan()
        {
            InitializeComponent();
            this.conn = App.Connection;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            loadMenu();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            InsertMenu insert = new InsertMenu();
            insert.ShowDialog();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("button");
        }

        DataTable tableMenu;
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            loadMenu();
        }

        private void loadMenu()
        {
            try
            {
                tableMenu = new DataTable();
                conn.Open();
                string query =
                    "SELECT nama_menu \"Nama Menu\",harga_menu \"Harga Menu\" FROM menu ";
                OracleCommand cmd = new OracleCommand(query,conn);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(tableMenu);
                DataColumn column = new DataColumn();
                column.ColumnName = "Action";
                tableMenu.Columns.Add(column);
                foreach (DataRow dataRow in tableMenu.Rows)
                {
                    string money = "Rp ";
                    int harga = Convert.ToInt32(dataRow[1].ToString());
                    string depan = (harga / 1000).ToString();
                    string belakang = ".000,00";
                    money += depan + belakang;
                    dataRow[1] = money;
                    Button btn = new Button();
                }
                gridMenu.ItemsSource = tableMenu.DefaultView;

                foreach (DataRow dataRow in tableMenu.Rows)
                {
                    Button btn = new Button();
                    btn.Content = dataRow[0].ToString();
                    
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("ada kesalahan saat load menu");
                conn.Close();
            }
        }

        
    }
}

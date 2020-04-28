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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Oracle.DataAccess.Client;

namespace Project.Master_paket
{
    /// <summary>
    /// Interaction logic for Paket.xaml
    /// </summary>
    public partial class Paket : UserControl
    {
        OracleConnection connection;
        Canvas canvas;
        public Paket(Canvas canvas)
        {
            InitializeComponent();
            connection = App.Connection;
            this.canvas = canvas;
        }

        DataTable tableMenu;

        private void loadMenu(string status, DataGrid grid)
        {
            try
            {
                tableMenu = new DataTable();
                connection.Open();
                string query =
                    $"SELECT NAMA_PAKET \"Nama Paket\",HARGA_PAKET \"Harga Paket\" FROM PAKET WHERE status = '{status}'";
                tbFilter.Text = query;
                OracleCommand cmd = new OracleCommand(query, connection);
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
                grid.ItemsSource = tableMenu.DefaultView;

                foreach (DataRow dataRow in tableMenu.Rows)
                {
                    Button btn = new Button();
                    btn.Content = dataRow[0].ToString();

                }

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("ada kesalahan saat load paket");
                connection.Close();
            }
        }
        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void gridMenu_Loaded(object sender, RoutedEventArgs e)
        {
            loadMenu("1", gridMenu);
        }

        private void gridPurgatory_Loaded(object sender, RoutedEventArgs e)
        {
            loadMenu("0", gridPurgatory);
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Insert_Paket insert_paket = new Insert_Paket(canvas);
            canvas.Children.Add(insert_paket);
        }
    }
}

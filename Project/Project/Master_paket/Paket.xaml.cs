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
            gridMenu.IsReadOnly = true;
        }

        DataTable tableMenu;
        DataTable tableMenuActive;
        DataTable tableMenuPurge;
        List<string> kodeMenuActive;
        List<string> kodeMenuPurge;

        private void loadMenu(string status, DataGrid grid, DataTable tableMenu, List<string> kodeMenu)
        {
            try
            {
                connection.Open();
                string query =
                    $"SELECT NAMA_PAKET \"Nama Paket\",HARGA_PAKET \"Harga Paket\" FROM PAKET WHERE status = '{status}'";
                tbFilter.Text = query;
                OracleCommand cmd = new OracleCommand(query, connection);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(tableMenu);
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

                query =
                    $"SELECT ID_PAKET FROM paket WHERE status = '{status}'";
                tbFilter.Text = query;
                cmd = new OracleCommand(query, connection);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kodeMenu.Add(reader.GetString(0));
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
            tableMenuActive = new DataTable();
            kodeMenuActive = new List<string>();
            loadMenu("1", gridMenu, tableMenuActive, kodeMenuActive);
        }

        private void gridPurgatory_Loaded(object sender, RoutedEventArgs e)
        {
            tableMenuPurge = new DataTable();
            kodeMenuPurge = new List<string>();
            loadMenu("0", gridPurgatory, tableMenuPurge, kodeMenuPurge);
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Insert_Paket insert_paket = new Insert_Paket(canvas," ");
            canvas.Children.Add(insert_paket);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbKode.Content.ToString() != "Kosong")
            {
                if (rdUpdate.IsChecked == true)
                {
                    canvas.Children.Clear();
                    //Insert_menu_UC update_menu = new Insert_menu_UC(canvas, lbKode.Content.ToString());
                    //canvas.Children.Add(update_menu);
                }
                else
                {
                    connection.Open();
                    string query =
                            $"UPDATE menu SET status = 0 WHERE id_menu = '{lbKode.Content}'";
                    OracleCommand cmd = new OracleCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    tableMenuActive = new DataTable();
                    kodeMenuActive = new List<string>();
                    loadMenu("1", gridMenu, tableMenuActive, kodeMenuActive);

                    tableMenuPurge = new DataTable();
                    kodeMenuPurge = new List<string>();
                    loadMenu("0", gridPurgatory, tableMenuPurge, kodeMenuPurge);
                }
            }
            else
            {
                MessageBox.Show("tidak ada menu yang dipilih");
            }
        }

        private void rdUpdate_Checked(object sender, RoutedEventArgs e)
        {
            btnEdit.Content = "Update";
        }

        private void rdDelete_Checked(object sender, RoutedEventArgs e)
        {
            btnEdit.Content = "Delete";
        }

        private void gridMenu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridMenu.SelectedIndex != -1)
            {
                lbKode.Content = kodeMenuActive[gridMenu.SelectedIndex];
                DataRow dr = tableMenuActive.Rows[gridMenu.SelectedIndex];
                tbNama.Text = dr[0].ToString();
            }
        }
    }
}

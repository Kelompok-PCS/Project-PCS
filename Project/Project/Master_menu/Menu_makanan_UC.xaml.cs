using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Project.Master_menu
{
    /// <summary>
    /// Interaction logic for Menu_makanan_UC.xaml
    /// </summary>
    public partial class Menu_makanan_UC : UserControl
    {
        OracleConnection conn;
        Canvas canvas;
        public Menu_makanan_UC(Canvas canvas)
        {
            InitializeComponent();
            conn = App.Connection;
            this.canvas = canvas;
            gridMenu.IsReadOnly = true;
            gridPurgatory.IsReadOnly = true;
        }

        DataTable tableMenuActive;
        DataTable tableMenuPurge;
        List<string> kodeMenuActive;
        List<string> kodeMenuPurge;
        private void loadMenu(string status,DataGrid grid,DataTable tableMenu,List<string> kodeMenu)
        {
            try
            {
                
                conn.Open();
                string query =
                    $"SELECT nama_menu \"Nama Menu\",TO_CHAR(harga_menu) \"Harga Menu\",DESKRIPSI \"Deskripsi Menu\" FROM menu WHERE status = '{status}'";
                OracleCommand cmd = new OracleCommand(query, conn);
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
                    $"SELECT ID_MENU FROM menu WHERE status = '{status}'";
                tbFilter.Text = query;
                cmd = new OracleCommand(query,conn);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kodeMenu.Add(reader.GetString(0));
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

        private void gridMenu_Loaded(object sender, RoutedEventArgs e)
        {
            tableMenuActive = new DataTable();
            kodeMenuActive = new List<string>();
            loadMenu("1",gridMenu,tableMenuActive,kodeMenuActive);
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Insert_menu_UC insert_Menu = new Insert_menu_UC(canvas," ");
            canvas.Children.Add(insert_Menu);
        }

        private void gridPurgatory_Loaded(object sender, RoutedEventArgs e)
        {
            tableMenuPurge = new DataTable();
            kodeMenuPurge = new List<string>();
            loadMenu("0",gridPurgatory,tableMenuPurge,kodeMenuPurge);
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if(lbKode.Content.ToString() != "Id Menu")
            {
                if(rdUpdate.IsChecked == true)
                {
                    canvas.Children.Clear();
                    Insert_menu_UC update_menu = new Insert_menu_UC(canvas, lbKode.Content.ToString());
                    canvas.Children.Add(update_menu);
                }
                else
                {
                    conn.Open();
                    string query =
                            $"UPDATE menu SET status = 0 WHERE id_menu = '{lbKode.Content}'";
                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    tableMenuActive = new DataTable();
                    kodeMenuActive = new List<string>();
                    loadMenu("1", gridMenu, tableMenuActive, kodeMenuActive);

                    tableMenuPurge = new DataTable();
                    kodeMenuPurge = new List<string>();
                    loadMenu("0", gridPurgatory, tableMenuPurge, kodeMenuPurge);
                    lbKode.Content = "Id Menu";
                    tbNama.Text = "Kosong";
                }
            }
            else
            {
                MessageBox.Show("tidak ada menu yang dipilih");
            }
        }

        private void rdDelete_Checked(object sender, RoutedEventArgs e)
        {
            btnEdit.Content = "Delete";
        }

        private void rdUpdate_Checked(object sender, RoutedEventArgs e)
        {
            btnEdit.Content = "Edit";
        }

        private void btnPulihkan_Click(object sender, RoutedEventArgs e)
        {
            if (lbKodePurge.Content.ToString() != "Id Menu")
            {
                conn.Open();
                string query =
                        $"UPDATE menu SET status = 1 WHERE id_menu = '{lbKodePurge.Content}'";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();

                tableMenuActive = new DataTable();
                kodeMenuActive = new List<string>();
                loadMenu("1", gridMenu, tableMenuActive, kodeMenuActive);

                tableMenuPurge = new DataTable();
                kodeMenuPurge = new List<string>();
                loadMenu("0", gridPurgatory, tableMenuPurge, kodeMenuPurge);
                lbKodePurge.Content = "Id Menu";
                tbNamaPulih.Text = "Kosong";
            }
            else
            {
                MessageBox.Show("tidak ada menu yang dipilih");
            }
        }

        private void gridPurgatory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (gridPurgatory.SelectedIndex != -1)
            {
                lbKodePurge.Content = kodeMenuPurge[gridPurgatory.SelectedIndex];
                DataRow dr = tableMenuPurge.Rows[gridPurgatory.SelectedIndex];
                tbNamaPulih.Text = dr[0].ToString();
            }
        }
    }
}

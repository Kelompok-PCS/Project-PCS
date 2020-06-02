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
using Oracle.DataAccess.Client;
using System.Data;
namespace Project
{
    /// <summary>
    /// Interaction logic for Master_Kategori_UC.xaml
    /// </summary>
    public partial class Master_Kategori_UC : UserControl
    {
        OracleConnection con;
        Canvas can;
        public Master_Kategori_UC(Canvas can)
        {
            InitializeComponent();
            this.can = can;
            con = App.Connection;
            dtgKategori.IsReadOnly = true;
            purgKategori.IsReadOnly = true;
        }

        DataTable tableMenuActive;
        DataTable tableMenuPurge;
        List<string> kodeMenuActive;
        List<string> kodeMenuPurge;
        private void callKategori(string status, DataGrid grid, DataTable tableMenu, List<string> kodeMenu)
        {
            try
            { 
                con.Open();
                string query =
                    $"SELECT nama_kategori \"Nama Kategori\", jenis_kategori \"Jenis Kategori\" FROM KATEGORI WHERE STATUS_KATEGORI = '{status}'";
                OracleCommand cmd = new OracleCommand(query, con);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(tableMenu);
                grid.ItemsSource = tableMenu.DefaultView;

                query =
                    $"SELECT ID_KATEGORI FROM KATEGORI WHERE status_kategori = '{status}'";
                tbFilter.Text = query;
                cmd = new OracleCommand(query, con);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kodeMenu.Add(reader.GetString(0));
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("ada kesalahan saat load kategori");
                con.Close();
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSubmit_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            can.Children.Clear();
            Insert_kategori panel = new Insert_kategori(can,lbKode.Content.ToString());
            can.Children.Add(panel);
        }

        private void dtgKategori_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dtgKategori.SelectedIndex != -1)
            {
                lbKode.Content = kodeMenuActive[dtgKategori.SelectedIndex];
                DataRow dr = tableMenuActive.Rows[dtgKategori.SelectedIndex];
                tbNama.Text = dr[0].ToString();
            }
        }

        private void dtgKategori_Loaded(object sender, RoutedEventArgs e)
        {
            tableMenuActive = new DataTable();
            kodeMenuActive = new List<string>();
            callKategori("1", dtgKategori, tableMenuActive, kodeMenuActive);
        }

        private void purgKategori_Loaded(object sender, RoutedEventArgs e)
        {
            tableMenuPurge = new DataTable();
            kodeMenuPurge = new List<string>();
            callKategori("0", purgKategori, tableMenuPurge, kodeMenuPurge);
        }

        private void purgKategori_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (purgKategori.SelectedIndex != -1)
            {
                lbKodePurge.Content = kodeMenuPurge[purgKategori.SelectedIndex];
                DataRow dr = tableMenuPurge.Rows[purgKategori.SelectedIndex];
                tbNamaPulih.Text = dr[0].ToString();
            }
        }

        private void rdUpdate_Checked(object sender, RoutedEventArgs e)
        {
            btnEdit.Content = "Edit";
        }

        private void rdDelete_Checked(object sender, RoutedEventArgs e)
        {
            btnEdit.Content = "Delete";
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbKode.Content.ToString() != "Id Kategori")
            {
                if (rdUpdate.IsChecked == true)
                {
                    can.Children.Clear();
                    Insert_kategori update_kat = new Insert_kategori(can, lbKode.Content.ToString());
                    can.Children.Add(update_kat);
                }
                else
                {
                    con.Open();
                    string query =
                            $"UPDATE kategori SET status_kategori = 0 WHERE id_kategori = '{lbKode.Content}'";
                    OracleCommand cmd = new OracleCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    tableMenuActive = new DataTable();
                    kodeMenuActive = new List<string>();
                    callKategori("1", dtgKategori, tableMenuActive, kodeMenuActive);

                    tableMenuPurge = new DataTable();
                    kodeMenuPurge = new List<string>();
                    callKategori("0", purgKategori, tableMenuPurge, kodeMenuPurge);
                    lbKode.Content = "Id Kategori";
                    tbNama.Text = "Kosong";
                }
            }
            else
            {
                MessageBox.Show("tidak ada kategori yang dipilih");
            }
        }

        private void btnPulihkan_Click(object sender, RoutedEventArgs e)
        {
            if (lbKodePurge.Content.ToString() != "Id Kategori")
            {
                con.Open();
                string query =
                        $"UPDATE kategori SET status_kategori = 1 WHERE id_kategori = '{lbKodePurge.Content}'";
                OracleCommand cmd = new OracleCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                tableMenuActive = new DataTable();
                kodeMenuActive = new List<string>();
                callKategori("1", dtgKategori, tableMenuActive, kodeMenuActive);

                tableMenuPurge = new DataTable();
                kodeMenuPurge = new List<string>();
                callKategori("0", purgKategori, tableMenuPurge, kodeMenuPurge);
                lbKodePurge.Content = "Id Kategori";
                tbNamaPulih.Text = "Kosong";
            }
            else
            {
                MessageBox.Show("tidak ada kategori yang dipilih");
            }
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }
    }
}

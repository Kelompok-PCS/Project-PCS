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

namespace Project.Pegawai
{
    /// <summary>
    /// Interaction logic for Pemesanan.xaml
    /// </summary>
    public partial class Pemesanan : UserControl
    {
        OracleConnection conn;
        public Pemesanan()
        {
            InitializeComponent();
            this.conn = App.Connection;
        }
        List<kategori> kategoris;
        List<string> kodeMenuTrans;
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            tbId.IsEnabled = false;
            kategoris = new List<kategori>();
            tableTrans = new DataTable();
            kodeMenuTrans = new List<string>();
            tableTrans.Columns.Add("Nama Menu");
            tableTrans.Columns.Add("Harga Menu");
            tableTrans.Columns.Add("Deskripsi Menu");
            tableTrans.Columns.Add("Jumlah");
            tableTrans.Columns.Add("Subtotal");
            gridMenu.IsReadOnly = true;
            gridTrans.IsReadOnly = true;
            try
            {
                conn.Open();
                string query1 =
                    "SELECT * FROM kategori ";
                OracleCommand cmd = new OracleCommand(query1,conn);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kategoris.Add(new kategori(reader.GetString(0), reader.GetString(1)));
                }
                cmbKategori.ItemsSource = kategoris;
                cmbKategori.DisplayMemberPath = "nama";
                cmbKategori.SelectedValuePath = "kode";
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
            cmbKategori.SelectedIndex = 0;
            string query =
                "SELECT nama_menu,harga_menu,deskripsi " +
                "FROM menu " +
                $"WHERE id_kategori = '{cmbKategori.SelectedValue}' AND status = '1'";
            loadMenu(query);
        }

        private class kategori
        {
            public string kode { get; set; }
            public string nama { get; set; }

            public kategori(string kode, string nama)
            {
                this.kode = kode;
                this.nama = nama;
            }
        }

        private void rdPunya_Checked(object sender, RoutedEventArgs e)
        {
            btnSearch.IsEnabled = true;
        }

        private void rdTdk_Checked(object sender, RoutedEventArgs e)
        {
            tbId.Text = "M000000000";
            btnSearch.IsEnabled = false;
        }
        DataTable tableMenu, tableTrans;

        private void cmbKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string query =
                "SELECT nama_menu \"Nama Menu\",harga_menu \"Harga Menu\",deskripsi " +
                "FROM menu " +
                $"WHERE id_kategori = '{cmbKategori.SelectedValue}' AND status = '1'";
            loadMenu(query);
        }
        List<string> kodeMenu;
        private void loadMenu(string query)
        {
            
            conn.Open();
            OracleCommand cmd = new OracleCommand(query,conn);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            tableMenu = new DataTable();
            adapter.Fill(tableMenu);
            gridMenu.ItemsSource = tableMenu.DefaultView;

            kodeMenu = new List<string>();
            query =
                "SELECT id_menu " +
                "FROM menu ";
            cmd = new OracleCommand(query,conn);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                kodeMenu.Add(reader.GetString(0));
            }
            conn.Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string query =
               "SELECT nama_menu,harga_menu,deskripsi " +
               "FROM menu " +
               $"WHERE nama_menu LIKE INITCAP('%{tbMenu.Text}%') AND status = '1'";
            loadMenu(query);
        }
        int grandtotal = 0;
        int jumlahPesanan = 0;
        private void btnTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int jumlah = Convert.ToInt32(tbjumlah.Text);
                if (gridMenu.SelectedIndex != -1)
                {
                    if (jumlah <= 0)
                    {
                        MessageBox.Show("jumlah harus lebih besar dari 0");
                    }
                    else
                    {
                        DataRow newTrans = tableTrans.NewRow();
                        newTrans[0] = tableMenu.Rows[gridMenu.SelectedIndex][0].ToString();
                        newTrans[1] = tableMenu.Rows[gridMenu.SelectedIndex][1].ToString();
                        newTrans[2] = tableMenu.Rows[gridMenu.SelectedIndex][2].ToString();
                        newTrans[3] = tbjumlah.Text;
                        newTrans[4] = Convert.ToInt32(newTrans[1].ToString()) * jumlah;
                        jumlahPesanan += jumlah;
                        grandtotal += Convert.ToInt32(newTrans[4].ToString());
                        lbPesanan.Content = jumlahPesanan;
                        lbTotal.Content = grandtotal;
                        tableTrans.Rows.Add(newTrans);
                        kodeMenuTrans.Add(kodeMenu[gridMenu.SelectedIndex]);
                        gridTrans.ItemsSource = tableTrans.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("salah input jumlah");
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            OracleTransaction trans = conn.BeginTransaction();
            try
            {
                string kode = "HJ";
                DateTime tgl = DateTime.Now;
                string nomor_nota = tgl.Year.ToString() + tgl.Month.ToString().PadLeft(2, '0') + tgl.Day.ToString().PadLeft(2, '0');
                string tanggl_trans = tgl.Day.ToString().PadLeft(2, '0') + tgl.Month.ToString().PadLeft(2, '0') + tgl.Year.ToString();
                kode += nomor_nota;
                string query =
                    "SELECT LPAD(NVL(MAX(SUBSTR(id_hjual,-3,3)),0)+1,3,0) " +
                    "FROM hjual " +
                    $"WHERE id_hjual LIKE '{kode}%' ";
                OracleCommand cmd = new OracleCommand(query, conn);
                kode += cmd.ExecuteScalar();
                MessageBox.Show(query);
                MessageBox.Show(kode);
                string jenisPemesanan = "";
                if(rdDine.IsChecked == true)
                {
                    jenisPemesanan = "Dine In";
                }
                else if(rdTake.IsChecked == true)
                {
                    jenisPemesanan = "Take Away";
                }
                else
                {
                    jenisPemesanan = "Delivery";
                }

                //TODO ganti pegawai dengan pegawai asli
                query =
                    "INSERT INTO hjual VALUES ( " +
                    $"'{kode}',TO_DATE('{tanggl_trans}','dd-mm-yyyy'),'{grandtotal}','{jenisPemesanan}','{"PEG001"}','{tbId.Text}') ";
                cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();

                int index = 0;
                string id_hjual = kode;
                foreach (DataRow row in tableTrans.Rows)
                {
                    kode += "_";
                    query =
                        "SELECT LPAD(NVL(MAX(SUBSTR(id_djual,-2,2)),0)+1,2,0) " +
                        "FROM djual " +
                        $"WHERE id_djual LIKE '{kode}%'";
                    cmd = new OracleCommand(query,conn);
                    kode += cmd.ExecuteScalar();
                    MessageBox.Show(query);
                    MessageBox.Show("id_djual: "+kode);
                    string id_menu = kodeMenuTrans[index];
                    index++;
                    string harga = row[1].ToString();
                    string jumlah = row[3].ToString();
                    string subtotal = row[4].ToString();

                    query =
                        $"INSERT INTO djual VALUES ( " +
                        $"'{kode}','{id_menu}','{harga}','{jumlah}','{subtotal}','{id_hjual}') ";
                    cmd = new OracleCommand(query,conn);
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                conn.Close();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                conn.Close();
                MessageBox.Show(ex.Message);
            }

            tableTrans = new DataTable();
            gridTrans.ItemsSource = tableTrans.DefaultView;
            kodeMenuTrans.Clear();
            grandtotal = 0;
            jumlahPesanan = 0;
            lbPesanan.Content = jumlahPesanan;
            lbTotal.Content = grandtotal;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string query =
                "SELECT id_member,fullname " +
                "FROM members " +
                $"WHERE no_hp = '{tbTelp.Text}'";
            conn.Open();
            OracleCommand cmd = new OracleCommand(query,conn);
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tbId.Text = reader.GetString(0);
                tbNama.Text = reader.GetString(1);
            }

            conn.Close();
        }
    }
}

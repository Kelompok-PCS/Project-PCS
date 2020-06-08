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
    public partial class Pemesanan_copy : UserControl
    {
        OracleConnection conn;
        public Pemesanan_copy()
        {
            InitializeComponent();
            this.conn = App.Connection;
            if (Form_pegawai.lbtn.Count() > 0)
            {
                string detail = " ";
                foreach (Button item in Form_pegawai.lbtn)
                {
                    detail += item.Content + ",";
                }
                detail_meja_pesanan.Text = detail.Substring(0, detail.Length - 1);
                jumlah_meja.Text = Form_pegawai.lbtn.Count().ToString();
            }
            else
            {
                detail_meja_pesanan.Text = "-";
                jumlah_meja.Text = "0";
            }
            reset_trans();
            loadMenu();
        }
        List<kategori> kategoris;
        List<string> kodeMenuTrans;
        private void getDetail_Meja()
        {
            //if (Form_pegawai.lbtn.Count()>0)
            //{
            //    string detail = " ";
            //    foreach (Button item in Form_pegawai.lbtn)
            //    {
            //        detail += item.Content + ",";
            //    }
            //    MessageBox.Show(detail);
            //    detail_meja_pesanan.Text = detail.Substring(0, detail.Length - 1);
            //    jumlah_meja.Text = Form_pegawai.lbtn.Count().ToString();
            //}
            //else
            //{
            //    detail_meja_pesanan.Text = "-";
            //    jumlah_meja.Text = "0";
            //}
        }
        private void reset_trans()
        {
            tableTrans = new DataTable();
            tableTrans.Columns.Add("Nama Menu");
            tableTrans.Columns.Add("Harga Menu");
            tableTrans.Columns.Add("Deskripsi Menu");
            tableTrans.Columns.Add("Jumlah");
            tableTrans.Columns.Add("Subtotal");
            gridTrans.ItemsSource = tableTrans.DefaultView;
        }
        private void update_meja()
        {
            if(rdDine.IsChecked == true)
            {
                string[] detail = detail_meja_pesanan.Text.Split(',');
                foreach (string item in detail)
                {
                    string query = $"UPDATE meja set status=2 where id_meja={item}";
                    OracleCommand cmd = new OracleCommand(query, App.Connection);
                    cmd.ExecuteNonQuery();
                }
                Form_pegawai.lbtn = new List<Button>();
                detail_meja_pesanan.Text = "-";
                jumlah_meja.Text = "0";
            }
            
        }
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
            gridTrans.IsReadOnly = true;
            

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

        }
        List<string> kodeMenu = new List<string>();
        private void loadMenu()
        {
            conn.Open();
            foreach (App.menu item in App.lMenu)
            {
                DataRow dr = tableTrans.NewRow();
                if (item.nama.Substring(0, 2) == "ME")
                {

                    string query = $"SELECT * from menu where id_menu='{item.nama}'";
                    using (OracleDataAdapter adap = new OracleDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adap.Fill(dt);
                        string id = dt.Rows[0].ItemArray[0].ToString();
                        string nama = dt.Rows[0].ItemArray[1].ToString();
                        string harga = dt.Rows[0].ItemArray[2].ToString();
                        string deskrip = dt.Rows[0].ItemArray[4].ToString();
                        int grandtotal = item.jumlah * Convert.ToInt32(harga);
                        dr[0] = nama;
                        dr[1] = harga;
                        dr[2] = deskrip;
                        dr[3] = item.jumlah;
                        dr[4] = grandtotal;
                        tableTrans.Rows.Add(dr);
                    }
                }
                else
                {

                    string query = $"SELECT * from paket where id_paket='{item.nama}'";
                    using (OracleDataAdapter adap = new OracleDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adap.Fill(dt);
                        string id = dt.Rows[0].ItemArray[0].ToString();
                        string nama = dt.Rows[0].ItemArray[1].ToString();
                        string harga = dt.Rows[0].ItemArray[2].ToString();
                        string deskrip = "";
                        int grandtotal = item.jumlah * Convert.ToInt32(harga);
                        dr[0] = nama;
                        dr[1] = harga;
                        dr[2] = deskrip;
                        dr[3] = item.jumlah;
                        dr[4] = grandtotal;
                        tableTrans.Rows.Add(dr);
                    }
                }
            }
            gridTrans.ItemsSource = tableTrans.DefaultView;
            conn.Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            
        }
        int grandtotal = 0;
        int jumlahPesanan = 0;
        private void btnTrans_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (gridTrans.SelectedIndex !=-1)
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
                    kode += cmd.ExecuteScalar().ToString();
                    string jenisPemesanan = "";

                    if (rdDine.IsChecked == true)
                    {
                        jenisPemesanan = "Dine In";
                    }
                    else if (rdTake.IsChecked == true)
                    {
                        jenisPemesanan = "Take Away";
                    }
                    else if (rdDelivery.IsChecked == true)
                    {
                        jenisPemesanan = "Delivery";
                    }


                    var keterangan = $"Jumlah Meja :{jumlah_meja.Text}||Detail Meja :{detail_meja_pesanan.Text}";

                    //TODO ganti pegawai dengan pegawai asli
                    query =
                        "INSERT INTO hjual VALUES ( " +
                        $"'{kode}',TO_DATE('{tanggl_trans}','dd-mm-yyyy'),'{grandtotal}','{jenisPemesanan}','{"PEG001"}','{tbId.Text}','{keterangan}') ";
                    cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    int index = 0;
                    string id_hjual = kode;
                    foreach (DataRow row in tableTrans.Rows)
                    {
                        string td = kode + "_";
                        query =
                            "SELECT LPAD(NVL(MAX(SUBSTR(id_djual,-2,2)),0)+1,2,0) " +
                            "FROM djual " +
                            $"WHERE id_djual LIKE '{td}%'";
                        cmd = new OracleCommand(query, conn);
                        td += cmd.ExecuteScalar().ToString();
                        string id_menu = kodeMenuTrans[index];
                        index++;
                        string harga = row[1].ToString();
                        string jumlah = row[3].ToString();
                        string subtotal = row[4].ToString();

                        query =
                            $"INSERT INTO djual VALUES ( " +
                            $"'{td}','{id_menu}','{harga}','{jumlah}','{subtotal}','{id_hjual}') ";
                        cmd = new OracleCommand(query, conn);
                        cmd.ExecuteNonQuery();
                    }
                    update_meja();
                    trans.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    conn.Close();
                    MessageBox.Show(ex.Message);
                }

                tableTrans.Clear();
                gridTrans.ItemsSource = tableTrans.DefaultView;
                kodeMenuTrans.Clear();
                grandtotal = 0;
                jumlahPesanan = 0;
                lbPesanan.Content = jumlahPesanan;
                lbTotal.Content = grandtotal;
                gridTrans.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("tidak ada yang dipilih");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if(gridTrans.SelectedIndex != -1)
            {
                DataRow dr = tableTrans.Rows[gridTrans.SelectedIndex];
                grandtotal -= Convert.ToInt32(dr[4].ToString());
                jumlahPesanan -= Convert.ToInt32(dr[3].ToString());
                lbPesanan.Content = jumlahPesanan;
                lbTotal.Content = grandtotal;
                kodeMenuTrans.RemoveAt(gridTrans.SelectedIndex);
                tableTrans.Rows.RemoveAt(gridTrans.SelectedIndex);
                gridTrans.ItemsSource = tableTrans.DefaultView;
                
            }
        }

        private void rdTake_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rbt = (RadioButton)sender;
            if(rbt.Content.ToString()=="Dine In")
            {
                //if (Form_pegawai.lbtn.Count() > 0)
                //{
                //    string detail = " ";
                //    foreach (Button item in Form_pegawai.lbtn)
                //    {
                //        detail += item.Content + ",";
                //    }
                //    MessageBox.Show(detail);
                //    detail_meja_pesanan.Text = detail.Substring(0, detail.Length - 1);
                //    jumlah_meja.Text = Form_pegawai.lbtn.Count().ToString();
                //}
                //else
                //{
                //    detail_meja_pesanan.Text = "-";
                //    jumlah_meja.Text = "0";
                //}
            }
            else
            {
                detail_meja_pesanan.Text = "-";
                jumlah_meja.Text = "-";
            }
        }

        private void r(object sender, RoutedEventArgs e)
        {

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

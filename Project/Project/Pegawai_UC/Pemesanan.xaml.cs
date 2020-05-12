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
            if (Form_pegawai.lbtn.Count() > 0)
            {
                string detail = " ";
                foreach (Button item in Form_pegawai.lbtn)
                {
                    detail += item.Content + ",";
                }
                MessageBox.Show(detail);
                detail_meja_pesanan.Text = detail.Substring(0, detail.Length - 1);
                jumlah_meja.Text = Form_pegawai.lbtn.Count().ToString();
            }
            else
            {
                detail_meja_pesanan.Text = "-";
                jumlah_meja.Text = "0";
            }

            MessageBox.Show("Yong taruh function update_meja() di submit mu yo");
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
        private void update_meja()
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
        List<string> kodeMenu = new List<string>();
        private void loadMenu(string query)
        {
            
            conn.Open();
            OracleCommand cmd = new OracleCommand(query,conn);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            tableMenu = new DataTable();
            adapter.Fill(tableMenu);
            gridMenu.ItemsSource = tableMenu.DefaultView;

            kodeMenu.Clear();
            query =
                "SELECT id_menu " +
                "FROM menu " +
                $"WHERE id_kategori = '{cmbKategori.SelectedValue}' AND status = '1'";
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
                else
                {
                    MessageBox.Show("Mohon pilih menu terlebih dahulu");
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
            if (gridMenu.SelectedIndex !=-1)
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
                gridMenu.SelectedIndex = -1;
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

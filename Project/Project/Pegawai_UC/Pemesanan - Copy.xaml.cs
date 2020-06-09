using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
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
        string kodePegawai = "";
        public Pemesanan_copy(string kodePegawai)
        {
            InitializeComponent();
            this.conn = App.Connection;
            rdDelivery.Checked += rdDine_Checked;
            rdTake.Checked += rdDine_Checked;
            rdDine.Checked += rdDine_Checked;
            rdReservasi.Checked += rdDine_Checked;

            for (int i = 10; i < 21; i++)
            {
                cbJam.Items.Add(i);
            }
            for (int i = 1; i < 60; i++)
            {
                cbMenit.Items.Add(i);
            }

            gridTrans.IsReadOnly = true;
            reset_trans();
            loadMenu();
            loadKupon();
            this.kodePegawai = kodePegawai;
        }
        
        private void reset_trans()
        {
            tableTrans = new DataTable();
            tableTrans.Columns.Add("Nama Menu");
            tableTrans.Columns.Add("Harga Menu");
            tableTrans.Columns.Add("Deskripsi Menu");
            tableTrans.Columns.Add("Jumlah");
            tableTrans.Columns.Add("Subtotal");
            tableTrans.Columns.Add("Keterangan");
            gridTrans.ItemsSource = tableTrans.DefaultView;
        }

        List<kupon_member> kupons;
        private void loadKupon()
        {
            kupons = new List<kupon_member>();
            conn.Open();
            string query =
                $"SELECT k1.id_kupon,k1.id_menu,k1.nama_kupon,TO_CHAR(k1.harga_kupon),TO_CHAR(k1.sisa_kupon) " +
                $"FROM kupon k1 " +
                $"JOIN kupon_member k2 ON k2.id_kupon = k1.id_kupon " +
                $"WHERE k2.id_member = '{tbId.Text}' AND k1.status_kupon = '1' AND " +
                $"k2.status= '1' AND (TO_CHAR(k1.periode_awal_kupon) >= TO_CHAR(SYSDATE) " +
                $"AND TO_CHAR(k1.periode_akhir_kupon) <= TO_CHAR(SYSDATE)) " +
                $"AND k1.sisa_kupon >0 ";
            OracleCommand cmd = new OracleCommand(query,conn);
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                kupons.Add(new kupon_member(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
            }
            
            if (kupons.Count == 0)
            {
                kupons.Add(new kupon_member("none"," ", "tidak terdapat kupon untuk member ini"," "," "));
            }
            cmbKupon.ItemsSource = kupons;
            cmbKupon.DisplayMemberPath = "nama_kupon";
            cmbKupon.SelectedValuePath = "kode_kupon";

            conn.Close();
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
            gridTrans.IsReadOnly = true;   
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
        private void loadMenu()
        {
            conn.Open();
            int grandtotal = 0;
            foreach (App.menu item in App.lMenu)
            {
                DataRow dr = tableTrans.NewRow();
                string query = "";
                string id = "";
                string nama = "";
                string harga = "";
                string deskrip ="";
                if (item.nama.Substring(0, 2) == "ME")
                {

                    query = $"SELECT * from menu where id_menu='{item.nama}'";
                    using (OracleDataAdapter adap = new OracleDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();


                        adap.Fill(dt);
                        id = dt.Rows[0].ItemArray[0].ToString();


                        nama = dt.Rows[0].ItemArray[1].ToString();
                        harga = dt.Rows[0].ItemArray[2].ToString();
                        deskrip = dt.Rows[0].ItemArray[4].ToString();
                        grandtotal = item.jumlah * Convert.ToInt32(harga);

                        dr[0] = nama;
                        dr[1] = harga;
                        dr[2] = deskrip;
                        dr[3] = item.jumlah;
                        dr[4] = grandtotal;
                    }
                }
                else
                {

                    query = $"SELECT * from paket where id_paket='{item.nama}'";
                    using (OracleDataAdapter adap = new OracleDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adap.Fill(dt);
                         id = dt.Rows[0].ItemArray[0].ToString();
                         nama = dt.Rows[0].ItemArray[1].ToString();
                         harga = dt.Rows[0].ItemArray[2].ToString();
                         deskrip = "";
                         grandtotal = item.jumlah * Convert.ToInt32(harga);
                        dr[0] = nama;
                        dr[1] = harga;
                        dr[2] = deskrip;
                        dr[3] = item.jumlah;
                        dr[4] = grandtotal;
                    }
                }
                dr[5] = "";
                try
                {
                    query = $"SELECT id_promo from promo_paket where id_paket='{id}' and status='1'";
                    
                    OracleCommand cmd = new OracleCommand(query, conn);
                    string id_paket = cmd.ExecuteScalar().ToString();
                    System.Windows.Forms.MessageBox.Show(id_paket);
                    query = $"SELECT jenis_promo from promo where TO_CHAR(periode_awal) <= TO_CHAR(SYSDATE)AND TO_CHAR(periode_akhir) >= TO_CHAR(SYSDATE) and id_promo='{id_paket}'";
                    cmd = new OracleCommand(query, conn);
                    string jenis= cmd.ExecuteScalar().ToString();
                    System.Windows.Forms.MessageBox.Show(jenis);
                    if (jenis.ToUpper()!="X")
                    {
                        query = $"SELECT harga_promo_paket from promo_paket where id_paket='{id}' and status='1'";
                        cmd = new OracleCommand(query, conn);
                        harga = cmd.ExecuteScalar().ToString();
                        grandtotal = item.jumlah * Convert.ToInt32(harga);
                        dr[1] = harga;
                        dr[4] = grandtotal;
                        dr[5] = $"Mendapat Potongan Harga menjadi {harga}/piece";
                    }
                    else
                    {
                        dr[3] = Convert.ToInt32(dr[3].ToString())+ 1;
                        dr[5] = $"Mendapat promo by 1 get 1";
                    }
                }
                catch (Exception ex)
                {

                }
                grandtotals += Convert.ToInt32(grandtotal);
                jumlahPesanan += Convert.ToInt32(dr[3].ToString());
                tableTrans.Rows.Add(dr);
            }
            gridTrans.ItemsSource = tableTrans.DefaultView;
            conn.Close();
            lbPesanan.Content = jumlahPesanan;
            lbTotal.Content = grandtotals.ToString();
            loadPromo();
        }

        List<promo> promos;
        private void loadPromo()
        {
            promos = new List<promo>();
            conn.Open();
            string query =
                "SELECT p1.id_promo,p2.id_paket,p1.nama_promo,TO_CHAR(p2.harga_promo_paket),p1.detail_promo,p1.jenis_promo " +
                "FROM promo p1 " +
                "JOIN promo_paket p2 ON p2.id_promo = p1.id_promo " +
                "WHERE p1.status_promo = '1' AND p2.status= '1' AND " +
                "(TO_CHAR(p1.periode_awal) >= TO_CHAR(SYSDATE) AND " +
                "TO_CHAR(p1.periode_akhir) <= TO_CHAR(SYSDATE)) ";
            OracleCommand cmd = new OracleCommand(query,conn);
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < App.lMenu.Count; i++)
                {
                    if (App.lMenu[i].nama == reader.GetString(1))
                    {
                        promos.Add(new promo(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                    } 
                }
            }

            if (promos.Count == 0)
            {
                promos.Add(new promo("none", " ", "tidak terdapat promo", " ", " "," "));
            }
            cmbPromo.ItemsSource = promos;
            cmbPromo.DisplayMemberPath = "nama_promo";
            cmbPromo.SelectedValuePath = "kode_promo";
            conn.Close();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            
        }
        int grandtotals = 0;
        int jumlahPesanan = 0;
        private void btnTrans_Click(object sender, RoutedEventArgs e)
        {

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
                kode += cmd.ExecuteScalar().ToString();
                string jenisPemesanan = "";
                string waktu = cbJam.SelectedItem + ":" + cbMenit.SelectedItem;
                if (rdDine.IsChecked == true)
                {
                    jenisPemesanan = "Dine In";
                    query = "SELECT to_char(sysdate,'HH24:MI') from dual";
                    cmd = new OracleCommand(query, conn);
                    waktu = cmd.ExecuteScalar().ToString();
                }
                else if (rdTake.IsChecked == true)
                {
                    jenisPemesanan = "Take Away";
                }
                else if (rdDelivery.IsChecked == true)
                {
                    jenisPemesanan = "Delivery";
                }
                else if (rdReservasi.IsChecked == true)
                {
                    jenisPemesanan = "Reservasi";
                }


                var keterangan = $"Jumlah Meja :{jumlah_meja.Text}||Detail Meja :{detail_meja_pesanan.Text}||Alamat :{tbAlamat.Text}||Waktu : {waktu}";
                query =
                    "INSERT INTO hjual VALUES ( " +
                    $"'{kode}',TO_DATE('{tanggl_trans}','dd-mm-yyyy'),'{grandtotals}','{jenisPemesanan}','{"PEG001"}','{tbId.Text}','{keterangan}','1') ";
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
                    string id_menu = App.lMenu[index].nama;
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

                if (cmbKupon.SelectedIndex != -1)
                {
                    if (cmbKupon.SelectedValue.ToString() != "none")
                    {
                        query =
                            $"UPDATE kupon_member SET status = 0 WHERE id_member = '{tbId.Text}'";
                        cmd = new OracleCommand(query, conn);
                        cmd.ExecuteNonQuery();

                        query =
                            $"UPDATE kupon SET sisa_kupon = {Convert.ToInt32(kupons[cmbKupon.SelectedIndex].sisa_kupon) - 1} WHERE id_kupon = '{cmbKupon.SelectedValue}'";
                        cmd = new OracleCommand(query, conn);
                        cmd.ExecuteNonQuery(); 
                    }
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
            App.lMenu.Clear();
            grandtotals = 0;
            jumlahPesanan = 0;
            lbPesanan.Content = jumlahPesanan;
            lbTotal.Content = grandtotals;
            gridTrans.SelectedIndex = -1;
            loadKupon();
            MessageBox.Show("berhasil masukan transaksi");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if(gridTrans.SelectedIndex != -1)
            {
                DataRow dr = tableTrans.Rows[gridTrans.SelectedIndex];
                grandtotals -= Convert.ToInt32(dr[4].ToString());
                jumlahPesanan -= Convert.ToInt32(dr[3].ToString());
                lbPesanan.Content = jumlahPesanan;
                lbTotal.Content = grandtotals;
                tableTrans.Rows.RemoveAt(gridTrans.SelectedIndex);
                gridTrans.ItemsSource = tableTrans.DefaultView;
                gridTrans.SelectedIndex = -1;
            }
        }


        private void cmbKupon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbKupon.SelectedIndex != -1)
            {
                if (cmbKupon.SelectedValue.ToString() != "none")
                {
                    for (int i = 0; i < App.lMenu.Count; i++)
                    {
                        if (App.lMenu[i].nama == kupons[cmbKupon.SelectedIndex].kode_menu)
                        {
                            int harga_kupon = Convert.ToInt32(kupons[cmbKupon.SelectedIndex].harga);
                            harga_kupon *= App.lMenu[i].jumlah;
                            grandtotals -= harga_kupon;
                            conn.Open();
                            string query =
                                "SELECT nama_menu " +
                                "FROM menu " +
                                $"WHERE id_menu = '{kupons[cmbKupon.SelectedIndex].kode_menu}' ";
                            OracleCommand cmd = new OracleCommand(query, conn);
                            string menu = cmd.ExecuteScalar().ToString();
                            conn.Close();
                            MessageBox.Show($"berhasil diskon '{menu}' seharga Rp " + harga_kupon);
                            lbTotal.Content = grandtotals;
                        }
                    } 
                }
            }
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

            loadKupon();
        }

        private void cmbPromo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPromo.SelectedIndex != -1)
            {
                if(cmbPromo.SelectedValue.ToString() != "none")
                {
                    MessageBox.Show(promos[cmbPromo.SelectedIndex].jenis_promo);
                    //x adalah buy one get one
                    if (promos[cmbPromo.SelectedIndex].jenis_promo != "X")
                    {
                        grandtotals -= promos[cmbPromo.SelectedIndex].getPromo();
                        lbTotal.Content = grandtotals;
                        MessageBox.Show($"berhasil potong harga {promos[cmbPromo.SelectedIndex].jenis} sebesar {promos[cmbPromo.SelectedIndex].getPromo()}"); 
                    }
                    else
                    {
                        int index = -1;
                        for (int i = 0; i < App.lMenu.Count; i++)
                        {
                            if(App.lMenu[i].nama == promos[cmbPromo.SelectedIndex].kode)
                            {
                                index = i;
                            }
                        }
                        App.lMenu.Add(new App.menu(App.lMenu[index].nama, App.lMenu[index].jumlah));
                        tableTrans.Clear();
                        gridTrans.ItemsSource = tableTrans.DefaultView;
                        loadMenu();
                    }
                }
            }
        }

        private class kupon_member
        {
            public string kode_kupon { get; set; }
            public string kode_menu { get; set; }
            public string nama_kupon { get; set; }
            public string harga { get; set; }
            public string sisa_kupon { get; set; }

            public kupon_member(string kode_kupon, string kode_menu, string nama_kupon, string harga, string sisa_kupon)
            {
                this.kode_kupon = kode_kupon;
                this.kode_menu = kode_menu;
                this.nama_kupon = nama_kupon;
                this.harga = harga;
                this.sisa_kupon = sisa_kupon;
            }
        }

        private void rdDine_Checked(object sender, RoutedEventArgs e)
        {

            RadioButton rbt = (RadioButton)sender;
            if (rbt.Content.ToString() == "Dine In" || rbt.Content.ToString() =="Reservasi")
            {
                if (Form_pegawai.lbtn.Count() > 0)
                {
                    string detail = " ";
                    foreach (Button item in Form_pegawai.lbtn)
                    {
                        detail += item.Content + ",";
                    }
                    detail_meja_pesanan.Text = detail.Substring(0, detail.Length - 1).ToString();
                    jumlah_meja.Text = Form_pegawai.lbtn.Count().ToString();
                }
                else
                {
                    detail_meja_pesanan.Text = "-";
                    jumlah_meja.Text = "0";
                }
            }
            else
            {
                detail_meja_pesanan.Text = "-";
                jumlah_meja.Text = "-";
            }
        }

        private class promo
        {
            public string kode_promo { get; set; }
            public string kode { get; set; }
            public string nama_promo { get; set; }
            public string harga { get; set; }
            public string detail { get; set; }
            public string jenis_promo { get; set; }
            public string jenis { get; set; } //menu atau paket

            public promo(string kode_promo, string kode, string nama_promo, string harga, string detail, string jenis_promo)
            {
                this.kode_promo = kode_promo;
                this.kode = kode;
                this.nama_promo = nama_promo;
                this.harga = harga;
                this.detail = detail;
                this.jenis_promo = jenis_promo;

                if (kode.Length == 6)
                {
                    this.jenis = "menu";
                }
                else
                {
                    this.jenis = "paket";
                }
            }

            public int getPromo()
            {
                int promo = 0;
                for (int i = 0; i < App.lMenu.Count; i++)
                {
                    if (App.lMenu[i].nama == kode)
                    {
                        promo += App.lMenu[i].jumlah * Convert.ToInt32(harga);
                    }
                }

                return promo;
            }
        }
    }
}

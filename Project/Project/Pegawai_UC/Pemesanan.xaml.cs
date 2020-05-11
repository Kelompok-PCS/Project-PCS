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
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            tbId.IsEnabled = false;
            kategoris = new List<kategori>();
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
                "SELECT nama_menu,harga_menu,deskripsi " +
                "FROM menu " +
                $"WHERE id_kategori = '{cmbKategori.SelectedValue}' AND status = '1'";
            loadMenu(query);
        }

        private void loadMenu(string query)
        {
            
            conn.Open();
            OracleCommand cmd = new OracleCommand(query,conn);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            tableMenu = new DataTable();
            adapter.Fill(tableMenu);
            gridMenu.ItemsSource = tableMenu.DefaultView;
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

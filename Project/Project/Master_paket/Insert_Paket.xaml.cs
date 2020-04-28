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
    /// Interaction logic for Insert_Paket.xaml
    /// </summary>
    public partial class Insert_Paket : UserControl
    {
        OracleConnection connection;
        Canvas canvas;
        public Insert_Paket(Canvas canvas)
        {
            InitializeComponent();
            connection = App.Connection;
            this.canvas = canvas;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (tbNama.Text != "" && tbHarga.Text != "" && cmbKat.SelectedIndex != -1 && cmbPro.SelectedIndex != -1)
            {
                try
                {
                    int harga = Convert.ToInt32(tbHarga.Text);
                    connection.Open();
                    OracleTransaction trans = connection.BeginTransaction();
                    try
                    {
                        string kode = "PK";
                        string query =
                            "SELECT LPAD(NVL(MAX(SUBSTR(ID_PAKET,-3,3)),0)+1,3,0) " +
                            "FROM PAKET " +
                            $"WHERE ID_PAKET LIKE '{kode}%'";
                        OracleCommand cmd = new OracleCommand(query, connection);
                        kode += cmd.ExecuteScalar();
                        query =
                            $"INSERT INTO PAKET VALUES ('{kode}','{tbNama.Text}','{tbHarga.Text}','{cmbKat.SelectedValue}','{cmbPro.SelectedValue}','1')";
                        cmd = new OracleCommand(query, connection);
                        cmd.ExecuteNonQuery();

                        trans.Commit();
                        connection.Close();
                        MessageBox.Show("Berhasil Masukan Paket");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        connection.Close();
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Gagal Masukan Paket");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Harga tidak valid");
                }

            }
            else
            {
                MessageBox.Show("Data belum lengkap");
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            fillKategori();
            fillpromo();
            cmbKat.SelectedIndex = 0;
        }

        private class Promo
        {
            public string kode { get; set; }
            public string nama { get; set; }
        }

        List<Kategori> kategoris = new List<Kategori>();
        List<Promo> promos = new List<Promo>();

        private class Kategori
        {
            public string kode { get; set; }
            public string nama { get; set; }
        }
        private void fillKategori()
        {
            try
            {
                connection.Open();
                string query =
                    "SELECT ID_KATEGORI,NAMA_KATEGORI " +
                    "FROM kategori ";
                OracleCommand cmd = new OracleCommand(query, connection);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kategoris.Add(new Kategori()
                    {
                        kode = reader.GetString(0),
                        nama = reader.GetString(0) + " - " + reader.GetString(1)
                    });
                }
                cmbKat.ItemsSource = kategoris;
                cmbKat.DisplayMemberPath = "nama";
                cmbKat.SelectedValuePath = "kode";
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
                MessageBox.Show("ada yang salah dengan kategori");
            }
        }

        private void fillpromo()
        {
            try
            {
                connection.Open();
                string query =
                    "SELECT ID_PROMO,NAMA_PROMO " +
                    "FROM PROMO ";
                OracleCommand cmd = new OracleCommand(query, connection);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kategoris.Add(new Kategori()
                    {
                        kode = reader.GetString(0),
                        nama = reader.GetString(0) + " - " + reader.GetString(1)
                    });
                }
                cmbPro.ItemsSource = promos;
                cmbPro.DisplayMemberPath = "nama";
                cmbPro.SelectedValuePath = "kode";
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
                MessageBox.Show("ada yang salah dengan promo");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Paket paket = new Paket(canvas);
            canvas.Children.Add(paket);
        }
    }
}

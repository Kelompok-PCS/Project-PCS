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
        string kodeMenu;
        int harga;
        string filename;
        string gambardbase;
        string source;
        string dest;
        public Insert_Paket(Canvas canvas, string kodeMenu)
        {
            InitializeComponent();
            connection = App.Connection;
            this.canvas = canvas;
            this.kodeMenu = kodeMenu;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (kodeMenu == " ")
            {
                if (tbNama.Text != "" && tbHarga.Text != "" && cmbKat.SelectedIndex != -1 && sourcetxt.Text != "")
                {
                    try
                    {
                        harga = Convert.ToInt32(tbHarga.Text);
                        connection.Open();
                        OracleTransaction trans = connection.BeginTransaction();
                        try
                        {
                            System.IO.File.Copy(source, dest);
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
                                    $"INSERT INTO PAKET VALUES ('{kode}','{tbNama.Text}',{harga},'{sourcetxt.Text}','{cmbKat.SelectedValue}','1')";
                                cmd = new OracleCommand(query, connection);
                                cmd.ExecuteNonQuery();

                                trans.Commit();
                                connection.Close();
                                MessageBox.Show("Berhasil Masukan Paket");
                                tbNama.Text = "";
                                tbHarga.Text = "";
                                cmbKat.SelectedIndex = 0;
                                sourcetxt.Text = "";
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
                            connection.Close();
                            MessageBox.Show("Gambar sama");
                        }
                    }
                    catch (Exception)
                    {
                        connection.Close();
                        MessageBox.Show("Harga tidak valid");
                    }

                }
                else
                {
                    MessageBox.Show("Data belum lengkap");
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Apakah Anda Yakin Update Paket ini ?", "Konfirmasi", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    connection.Open();
                    OracleTransaction trans = connection.BeginTransaction();
                    try
                    {
                        string query =
                            $"UPDATE paket SET NAMA_PAKET = '{tbNama.Text}',HARGA_PAKET = {harga}, GAMBAR_PAKET = '{sourcetxt.Text}', ID_KATEGORI = '{cmbKat.SelectedValue}' WHERE ID_PAKET = '{kodeMenu}'";
                        OracleCommand cmd = new OracleCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        connection.Close();
                        MessageBox.Show("Berhasil update data paket");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        trans.Rollback();
                        connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("yahhh sedihhh");
                    MessageBox.Show("Abort Update Paket");
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            fillKategori();
            if (kodeMenu == " ")
            {
                cmbKat.SelectedIndex = 0;
                btnSubmit.Content = "Insert";
                lbJudul.Content = "Insert Menu";
            }
            else
            {
                loadMenu();
                btnSubmit.Content = "Update";
                lbJudul.Content = "Update Menu";
            }
            lbPrevData1.Content = prevName1;
        }

        
        List<Kategori> kategoris = new List<Kategori>();

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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Paket paket = new Paket(canvas);
            canvas.Children.Add(paket);
        }

        string prevName1 = "";
        private void loadMenu()
        {
            connection.Open();
            string query =
              $"SELECT nama_paket, TO_CHAR(harga_paket), gambar_paket, id_kategori FROM PAKET WHERE ID_PAKET = '{kodeMenu}'";
            OracleCommand cmd = new OracleCommand(query, connection);
            OracleDataReader reader = cmd.ExecuteReader();
            string kodeKategori = "";
            while (reader.Read())
            {
                tbNama.Text = reader.GetString(0);
                tbHarga.Text = reader.GetString(1);
                sourcetxt.Text = reader.GetString(2);
                kodeKategori = reader.GetString(3);
            }

            for (int i = 0; i < kategoris.Count; i++)
            {
                if (kategoris[i].kode == kodeKategori)
                {
                    cmbKat.SelectedIndex = i;
                    prevName1 = $"Kategori yang ingin diupdate : {kategoris[i].nama}";
                }
            }

            connection.Close();
        }

        private void loadfile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                source = fileDialog.FileName;
                filename = System.IO.Path.GetFileName(source);
                dest = @"C:\xampp\htdocs\coba\Project-PCS\Project\Project\Image\" + filename;
                gambardbase = "Image/" + filename;
                sourcetxt.Text = gambardbase;
            }
        }
    }
}

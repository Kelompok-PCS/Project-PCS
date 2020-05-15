using Oracle.DataAccess.Client;
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

namespace Project.Master_menu
{
    /// <summary>
    /// Interaction logic for Insert_menu_UC.xaml
    /// </summary>
    public partial class Insert_menu_UC : UserControl
    {
        OracleConnection connection;
        Canvas canvas;
        string kodeMenu;
        int harga;
        string filename;
        string gambardbase;
        string source;
        string dest;
        public Insert_menu_UC(Canvas canvas,string kodeMenu)
        {
            InitializeComponent();
            connection = App.Connection;
            this.canvas = canvas;
            this.kodeMenu = kodeMenu;
        }
        
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(kodeMenu == " ")
            {
                if (tbNama.Text != "" && tbHarga.Text != "" && cmbKat.SelectedIndex != -1 && sourcetxt.Text != "" && tbDesc.Text != "")
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
                                string kode = "MEN";
                                string query =
                                    "SELECT LPAD(NVL(MAX(SUBSTR(id_menu,-3,3)),0)+1,3,0) " +
                                    "FROM menu " +
                                    $"WHERE id_menu LIKE '{kode}%'";
                                OracleCommand cmd = new OracleCommand(query, connection);
                                kode += cmd.ExecuteScalar();
                                query =
                                    $"INSERT INTO menu VALUES ('{kode}','{tbNama.Text}',{harga},'{gambardbase}','{tbDesc.Text}','{cmbKat.SelectedValue}','1')";
                                cmd = new OracleCommand(query, connection);
                                cmd.ExecuteNonQuery();

                                trans.Commit();
                                connection.Close();
                                MessageBox.Show("Berhasil Masukan Menu");
                                tbNama.Text = "";
                                tbHarga.Text = "";
                                cmbKat.SelectedIndex = 0;
                                sourcetxt.Text = "";
                                tbDesc.Text = "";
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                connection.Close();
                                MessageBox.Show(ex.Message);
                                MessageBox.Show("Gagal Masukan Menu");
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
                MessageBoxResult result = MessageBox.Show("Apakah Anda Yakin Update Menu ini ?", "Konfirmasi", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    connection.Open();
                    OracleTransaction trans = connection.BeginTransaction();
                    try
                    {
                        string query =
                            $"UPDATE menu SET NAMA_MENU = '{tbNama.Text}',HARGA_MENU = '{tbHarga.Text}',GAMBAR = '{sourcetxt.Text}',DESKRIPSI = '{tbDesc.Text}' WHERE id_menu = '{kodeMenu}'";
                        OracleCommand cmd = new OracleCommand(query,connection);
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        connection.Close();
                        MessageBox.Show("Berhasil update data menu");
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
                    MessageBox.Show("Abort Update Menu");
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
            lbPrevData.Content = prevName;
        }

        private class Kategori
        {
            public string kode { get; set; }
            public string nama { get; set; }
        }
        List<Kategori> kategoris;
        private void fillKategori()
        {
            kategoris = new List<Kategori>();
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
            Menu_makanan_UC menu_makanan = new Menu_makanan_UC(canvas);
            canvas.Children.Add(menu_makanan);
        }
        string prevName = "";
        private void loadMenu()
        {
            connection.Open();
            string query =
              $"SELECT nama_menu,TO_CHAR(harga_menu),gambar,DESKRIPSI,id_kategori FROM menu WHERE id_menu = '{kodeMenu}'";
            OracleCommand cmd = new OracleCommand(query,connection);
            OracleDataReader reader = cmd.ExecuteReader();
            string kodeKategori = "";
            while (reader.Read())
            {
                tbNama.Text = reader.GetString(0);
                tbHarga.Text = reader.GetString(1);
                sourcetxt.Text = reader.GetString(2);
                tbDesc.Text = reader.GetString(3);
                kodeKategori = reader.GetString(4);
            }

            for (int i = 0; i < kategoris.Count; i++)
            {
                if(kategoris[i].kode == kodeKategori)
                {
                    cmbKat.SelectedIndex = i;
                    prevName = $"Kategori yang ingin diupdate : {kategoris[i].nama}";
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

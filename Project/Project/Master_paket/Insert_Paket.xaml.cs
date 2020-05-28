using Microsoft.Win32;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
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
        string target;
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
                if (tbNama.Text != "" && tbHarga.Text != "" && cmbKat.SelectedIndex != -1 && sourcetxt.Text != "" && cmbMen1.SelectedIndex != cmbMen2.SelectedIndex)
                {
                    try
                    {
                        harga = Convert.ToInt32(tbHarga.Text);
                        connection.Open();
                        OracleTransaction trans = connection.BeginTransaction();
                        try
                        {
                            File.Copy(filename, target);
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

                                 string query2 =
                                    $"INSERT INTO PAKET_MENU VALUES ('{kode}','{cmbMen1.SelectedValue}')";
                                cmd = new OracleCommand(query2, connection);
                                cmd.ExecuteNonQuery();

                                string query3 =
                                   $"INSERT INTO PAKET_MENU VALUES ('{kode}','{cmbMen2.SelectedValue}')";
                                cmd = new OracleCommand(query3, connection);
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
                    MessageBox.Show("Data belum lengkap/ Data Menu Kembar");
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
            fillmenu1();
            fillmenu2();
            if (kodeMenu == " ")
            {
                cmbKat.SelectedIndex = 0;
                cmbMen1.SelectedIndex = 0;
                cmbMen2.SelectedIndex = 0;
                btnSubmit.Content = "Insert";
                lbJudul.Content = "Insert Paket";
            }
            else
            {
                loadMenu();
                btnSubmit.Content = "Update";
                lbJudul.Content = "Update Paket";
            }
            lbPrevData1.Content = prevName1;
            lbPrevData2.Content = prevName2;
            lbPrevData3.Content = prevName3;
        }

        
        List<Kategori> kategoris = new List<Kategori>();
        List<Menu> menus = new List<Menu>();

        private class Kategori
        {
            public string kode { get; set; }
            public string nama { get; set; }
        }

        private class Menu
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

        private void fillmenu1()
        {
            try
            {
                connection.Open();
                string query =
                    "SELECT ID_MENU,NAMA_MENU " +
                    "FROM MENU ";
                OracleCommand cmd = new OracleCommand(query, connection);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    menus.Add(new Menu()
                    {
                        kode = reader.GetString(0),
                        nama = reader.GetString(0) + " - " + reader.GetString(1)
                    });
                }
                cmbMen1.ItemsSource = menus;
                cmbMen1.DisplayMemberPath = "nama";
                cmbMen1.SelectedValuePath = "kode";
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
                MessageBox.Show("ada yang salah dengan menu");
            }
        }

        private void fillmenu2()
        {
            try
            {
                connection.Open();
                string query =
                    "SELECT ID_MENU,NAMA_MENU " +
                    "FROM MENU ";
                OracleCommand cmd = new OracleCommand(query, connection);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    menus.Add(new Menu()
                    {
                        kode = reader.GetString(0),
                        nama = reader.GetString(0) + " - " + reader.GetString(1)
                    });
                }
                cmbMen2.ItemsSource = menus;
                cmbMen2.DisplayMemberPath = "nama";
                cmbMen2.SelectedValuePath = "kode";
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
                MessageBox.Show("ada yang salah dengan menu");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Paket paket = new Paket(canvas);
            canvas.Children.Add(paket);
        }

        string prevName1 = "";
        string prevName2 = "";
        string prevName3 = "";
        private void loadMenu()
        {
            connection.Open();
            string query =
              $"SELECT nama_paket, TO_CHAR(harga_paket), gambar_paket, id_kategori FROM PAKET WHERE ID_PAKET = '{kodeMenu}'";

            string query2 =
              $"SELECT id_menu from PAKET_MENU WHERE ID_PAKET = '{kodeMenu}'";

            OracleCommand cmd = new OracleCommand(query, connection);
            OracleCommand cmd2 = new OracleCommand(query2, connection);
            OracleDataReader reader = cmd.ExecuteReader();
            OracleDataReader reader2 = cmd2.ExecuteReader();
            string kodeKategori = "";
            while (reader.Read())
            {
                tbNama.Text = reader.GetString(0);
                tbHarga.Text = reader.GetString(1);
                sourcetxt.Text = reader.GetString(2);
                kodeKategori = reader.GetString(3);
            }

            List<string> tmp = new List<string>();
            tmp.Clear();

            string kodemenu1 = "";
            while (reader2.Read())
            {
                kodemenu1 = reader2.GetString(0);
                tmp.Add(kodemenu1);
            }

            for (int i = 0; i < kategoris.Count; i++)
            {
                if (kategoris[i].kode == kodeKategori)
                {
                    cmbKat.SelectedIndex = i;
                    prevName1 = $"Kategori yang ingin diupdate : {kategoris[i].nama}";
                }
            }

            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].kode == tmp[0])
                {
                    cmbMen1.SelectedIndex = i;
                    prevName2 = $"Menu yang ingin diupdate : {menus[i].nama}";
                }
                if (menus[i].kode == tmp[1])
                {
                    cmbMen2.SelectedIndex = i;
                    prevName3 = $"Menu yang ingin diupdate : {menus[i].nama}";
                }
            }

            connection.Close();
        }

        private void loadfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                filename = dlg.FileName;
                string[] paths = filename.Split('\\');
                string strImage = paths[paths.Length - 1];
                string directoryProject = Environment.CurrentDirectory;
                paths = directoryProject.Split('\\');
                for (int i = 0; i < 7; i++)
                {
                    target += paths[i] + "\\";
                }
                target += "Image\\" + strImage;

                sourcetxt.Text = "Image/" + strImage;
            }
        }
    }
}

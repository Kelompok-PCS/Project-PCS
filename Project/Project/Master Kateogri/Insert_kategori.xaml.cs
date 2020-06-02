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
namespace Project
{
    /// <summary>
    /// Interaction logic for Insert_kategori.xaml
    /// </summary>
    public partial class Insert_kategori : UserControl
    {
        OracleConnection connection;
        Canvas can;
        string kodeMenu;
        public Insert_kategori(Canvas can, string kodeMenu)
        {
            InitializeComponent();
            connection = App.Connection;
            this.can = can;
            this.kodeMenu = kodeMenu;
        }

        private void btnSubmit_Click_1(object sender, RoutedEventArgs e)
        {
            if (kodeMenu == " ")
            {
                if (tbNama.Text != "" && (rbMakanan.IsChecked == true || rbMinuman.IsChecked == true))
                {
                    try
                    {
                        connection.Open();
                        OracleTransaction trans = connection.BeginTransaction();
                        try
                        {
                            string kode = "KAT";
                            string query =
                                "SELECT LPAD(NVL(MAX(SUBSTR(id_kat,-3,3)),0)+1,3,0) " +
                                "FROM kategori " +
                                $"WHERE id_kategori LIKE '{kode}%'";
                            OracleCommand cmd = new OracleCommand(query, connection);
                            kode += cmd.ExecuteScalar();
                            string jenis = "";
                            if (rbMakanan.IsChecked == true)
                            {
                                jenis = "Makanan";
                            }
                            else
                            {
                                jenis = "Minuman";
                            }
                            query =
                                $"INSERT INTO menu VALUES ('{kode}','{tbNama.Text}','{jenis}','1')";
                            cmd = new OracleCommand(query, connection);
                            cmd.ExecuteNonQuery();

                            trans.Commit();
                            connection.Close();
                            MessageBox.Show("Berhasil Masukan Kategori");
                            tbNama.Text = "";
                            rbMakanan.IsChecked = false;
                            rbMinuman.IsChecked = false;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            connection.Close();
                            MessageBox.Show(ex.Message);
                            MessageBox.Show("Gagal Masukan Kategori");
                        }
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Gagal");
                    }
                }
                else
                {
                    MessageBox.Show("Data belum lengkap");
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Apakah Anda Yakin Update Kategori ini ?", "Konfirmasi", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    connection.Open();
                    OracleTransaction trans = connection.BeginTransaction();
                    try
                    {
                        string jenis = "";
                        if (rbMakanan.IsChecked == true)
                        {
                            jenis = "Makanan";
                        }
                        else
                        {
                            jenis = "Minuman";
                        }
                        string query =
                            $"UPDATE kategori SET NAMA_KATEGORI = '{tbNama.Text}', JENIS_KATEGORI = '{jenis}' WHERE ID_KATEGORI = '{kodeMenu}'";
                        OracleCommand cmd = new OracleCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                        connection.Close();
                        MessageBox.Show("Berhasil update data kategori");
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
                    MessageBox.Show("Abort Update Kategori");
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            can.Children.Clear();
            Master_Kategori_UC panel = new Master_Kategori_UC(can);
            can.Children.Add(panel);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (kodeMenu == " ")
            {

                btnSubmit.Content = "Insert";
                lbJudul.Content = "Insert Menu";
            }
            else
            {
                loadMenu();
                btnSubmit.Content = "Update";
                lbJudul.Content = "Update Menu";
            }
        }

        private void loadMenu()
        {
            connection.Open();
            string query =
              $"SELECT nama_kategori, jenis_kategori FROM kategori WHERE id_kategori = '{kodeMenu}'";
            OracleCommand cmd = new OracleCommand(query, connection);
            OracleDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                tbNama.Text = reader.GetString(0);
                string jenis = reader.GetString(1);
                if (jenis == "Makanan")
                {
                    rbMakanan.IsChecked = true;
                }
                else
                {
                    rbMinuman.IsChecked = true;
                }

            }
            connection.Close();
        }
    }

    
}

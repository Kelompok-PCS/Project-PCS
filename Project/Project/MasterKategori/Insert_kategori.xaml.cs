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
        public Insert_kategori(OracleConnection conn)
        {
            InitializeComponent();
            connection = conn;
        }

        private void btnSubmit_Click_1(object sender, RoutedEventArgs e)
        {
            if (tbNama.Text != "" )
            {
                connection.Open();
                OracleTransaction trans = connection.BeginTransaction();
                try
                {
                    string kode = "KAT";
                    string query =
                        "SELECT LPAD(NVL(MAX(SUBSTR(id_kategori,-3,3)),'0')+1,3,0) FROM kategori ";
                    OracleCommand cmd = new OracleCommand(query, connection);
                    kode += cmd.ExecuteScalar();
                    string jenis = "";
                    if ((bool)rbMakanan.IsChecked)
                    {
                        jenis = "makanan";
                    }else if ((bool)rbMinuman.IsChecked)
                    {
                        jenis = "minuman";
                    }
                    query =
                        $"INSERT INTO kategori VALUES ('{kode}','{tbNama.Text}','{jenis}',1)";
                    MessageBox.Show(query);
                    cmd = new OracleCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                    connection.Close();
                    MessageBox.Show("Berhasil Masukan Menu");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    connection.Close();
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("Gagal Masukan Menu");
                }

            }
            else
            {
                MessageBox.Show("Data belum lengkap");
            }
        }
    }
}

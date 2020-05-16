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
using System.Data;
namespace Project
{
    /// <summary>
    /// Interaction logic for Master_Kategori_UC.xaml
    /// </summary>
    public partial class Master_Kategori_UC : UserControl
    {
        OracleConnection con;
        DataSet ds;
        Canvas can;
        string id_kategori;
        public Master_Kategori_UC(Canvas can)
        {
            InitializeComponent();
            this.can = can;
            con = App.Connection;
            callKategori();
        }
        private void callKategori()
        {
            using(OracleDataAdapter adap = new OracleDataAdapter("SELECT * from kategori", con))
            {
                ds = new DataSet();
                adap.Fill(ds);
                dtgKategori.ItemsSource = ds.Tables[0].DefaultView;
            }
        }
        private void btnSubmit_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string nama = tbNama.Text;
            string jenis = "";
            if (rbMakanan.IsChecked==true)
            {
                jenis = "makanan";
            }
            else
            {
                jenis = "minuman";
            }
            string status = Convert.ToInt32(cbxStatus.IsChecked).ToString();
            //MessageBox.Show(status);
            con.Open();
            string query = $"UPDATE kategori set nama_kategori='{nama}',jenis_kategori='{jenis}',status_kategori={status} where id_kategori='{id_kategori}'";
            MessageBox.Show(query);
            OracleCommand cmd = new OracleCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            callKategori();
        }

        private void dtgKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgKategori.SelectedIndex!=-1)
            {
                id_kategori = ds.Tables[0].Rows[dtgKategori.SelectedIndex]["id_kategori"].ToString();
                tbNama.Text = ds.Tables[0].Rows[dtgKategori.SelectedIndex]["nama_kategori"].ToString();
                string jenis = ds.Tables[0].Rows[dtgKategori.SelectedIndex]["jenis_kategori"].ToString();
                if (jenis == "minuman") rbMinuman.IsChecked = true;
                else rbMakanan.IsChecked = true;
                cbxStatus.IsChecked = Convert.ToBoolean(Convert.ToInt32(ds.Tables[0].Rows[dtgKategori.SelectedIndex]["status_kategori"].ToString()));

            }
        }

        private void btnSubmit_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            can.Children.Clear();
            Insert_kategori panel = new Insert_kategori(con,can);
            can.Children.Add(panel);
        }
    }
}

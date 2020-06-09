using Oracle.DataAccess.Client;
using Project.Pegawai;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for Form_pegawai.xaml
    /// </summary>
    public partial class Form_pegawai : Window
    {
        OracleConnection conn;
        Login_pegawai logpeg;
        public static List<Button> lbtn = new List<Button>();
        string kodePegawai = "";
        public Form_pegawai(string kodePegawai, Login_pegawai logpeg)
        {
            InitializeComponent();
            this.conn = App.Connection;
            this.kodePegawai = kodePegawai;
            this.logpeg = logpeg;
            try
            {
                conn.Open();
                string query = $"SELECT NAMA FROM PEGAWAI WHERE ID_PEGAWAI = '{kodePegawai}'";
                OracleCommand cmd = new OracleCommand(query, conn);
                string tmp = cmd.ExecuteScalar().ToString();
                tbNamaPeg.Text = tmp;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.StackTrace);
            }
        }

        //private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    MessageBox.Show("hore");
        //}

        private void getClick(TextBlock tb)
        {
            //tb.Background = 
        }

        private void tbPemesan_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvas.Children.Clear();
            Pemesanan_copy pemesanan = new Pemesanan_copy(kodePegawai);
            canvas.Children.Add(pemesanan);
        }

		private void TbPendaftaran_MouseDown(object sender, MouseButtonEventArgs e)
		{
			canvas.Children.Clear();
			Regis_Member daftar = new Regis_Member();
			canvas.Children.Add(daftar);
		}
        private void Struk_mouseDown(object sender, MouseButtonEventArgs e)
        {
            canvas.Children.Clear();
            struk daftar = new struk();
            canvas.Children.Add(daftar);
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
		{
			canvas.Children.Clear();
			Pilih_Meja Pilih_Meja = new Pilih_Meja();
			canvas.Children.Add(Pilih_Meja);
		}

		private void TbMenu_MouseDown(object sender, MouseButtonEventArgs e)
		{
			canvas.Children.Clear();
			Homemenu menu = new Homemenu();
			canvas.Children.Add(menu);
			
		}

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            logpeg.Show();
        }
    }
      
    
}

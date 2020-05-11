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
        public Form_pegawai()
        {
            InitializeComponent();
            this.conn = App.Connection;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("hore");
        }

        private void getClick(TextBlock tb)
        {
            //tb.Background = 
        }

        private void tbPemesan_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvas.Children.Clear();
            Pemesanan pemesanan = new Pemesanan();
            canvas.Children.Add(pemesanan);
        }
    }
}

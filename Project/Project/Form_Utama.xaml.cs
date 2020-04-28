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
using System.Windows.Shapes;
using Oracle.DataAccess.Client;
using Project.Master_menu;
using Project.Master_paket;

namespace Project
{
    /// <summary>
    /// Interaction logic for Form_Utama.xaml
    /// </summary>
    public partial class Form_Utama : Window
    {
        OracleConnection conn;
        public Form_Utama(OracleConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }
        public void checkConnection(OracleConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
        }
        private void Insert_Kategori_Click(object sender, RoutedEventArgs e)
        {
            checkConnection(conn);
            canvas.Children.Clear();
            Insert_kategori panel = new Insert_kategori(conn);
            canvas.Children.Add(panel);
        }

        private void tbShowMembers_Click(object sender, RoutedEventArgs e)
        {
            checkConnection(conn);
            canvas.Children.Clear();
            Master_User_UC panel = new Master_User_UC(conn);
            canvas.Children.Add(panel);
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Master_menu.Menu_makanan_UC menu_Makanan = new Menu_makanan_UC(canvas);
            canvas.Children.Add(menu_Makanan);
        }

        private void tbInsertPegawai_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Insert_Pegawai_UC panel = new Insert_Pegawai_UC();
            canvas.Children.Add(panel);
            
        }

        private void Insert_Paket_Click(object sender, RoutedEventArgs e)
        {
            checkConnection(conn);
            canvas.Children.Clear();
            Insert_Paket panel = new Insert_Paket(canvas);
            canvas.Children.Add(panel);
        }

        private void btnPaket_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Paket panel = new Paket(canvas);
            canvas.Children.Add(panel);
        }
    }
}

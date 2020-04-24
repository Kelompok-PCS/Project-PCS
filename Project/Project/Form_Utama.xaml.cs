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

        private void Insert_Kategori_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Insert_kategori panel = new Insert_kategori(conn);
            canvas.Children.Add(panel);
        }
    }
}

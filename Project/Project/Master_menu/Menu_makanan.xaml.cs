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
using System.Windows.Shapes;

namespace Project.Master_menu
{
    /// <summary>
    /// Interaction logic for InsertMenu.xaml
    /// </summary>
    public partial class Menu_makanan : Window
    {
        OracleConnection conn;
        public Menu_makanan()
        {
            InitializeComponent();
            this.conn = App.Connection;
            
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            InsertMenu insert = new InsertMenu();
            insert.ShowDialog();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("button");
        }
    }
}

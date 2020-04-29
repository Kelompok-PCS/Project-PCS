using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Project.Master_menu
{
    /// <summary>
    /// Interaction logic for Menu_makanan_UC.xaml
    /// </summary>
    public partial class Menu_makanan_UC : UserControl
    {
        OracleConnection conn;
        Canvas canvas;
        public Menu_makanan_UC(Canvas canvas)
        {
            InitializeComponent();
            conn = App.Connection;
            this.canvas = canvas;
        }

        DataTable tableMenu;

        private void loadMenu(string status,DataGrid grid)
        {
            try
            {
                tableMenu = new DataTable();
                conn.Open();
                string query =
                    $"SELECT nama_menu \"Nama Menu\",to_char(harga_menu) \"Harga Menu\",DESKRIPSI \"Deskripsi Menu\" FROM menu WHERE status = '{status}'";
                OracleCommand cmd = new OracleCommand(query, conn);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(tableMenu);
                DataColumn column = new DataColumn();
                column.ColumnName = "Action";
                tableMenu.Columns.Add(column);
                foreach (DataRow dataRow in tableMenu.Rows)
                {
                    string money = "Rp ";
                    int harga = Convert.ToInt32(dataRow[1].ToString());
                    string depan = (harga / 1000).ToString();
                    string belakang = ".000,00";
                    money += depan + belakang;
                    dataRow[1] = money;
                    Button btn = new Button();
                }
                grid.ItemsSource = tableMenu.DefaultView;

                foreach (DataRow dataRow in tableMenu.Rows)
                {
                    Button btn = new Button();
                    btn.Content = dataRow[0].ToString();

                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("ada kesalahan saat load menu");
                conn.Close();
            }
        }

        private void gridMenu_Loaded(object sender, RoutedEventArgs e)
        {
            loadMenu("1",gridMenu);
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Insert_menu_UC insert_Menu = new Insert_menu_UC(canvas);
            canvas.Children.Add(insert_Menu);
        }

        private void gridPurgatory_Loaded(object sender, RoutedEventArgs e)
        {
            loadMenu("0",gridPurgatory);
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
        }
    }
}

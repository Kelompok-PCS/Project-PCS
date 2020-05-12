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
using System.Data;

namespace Project
{
    /// <summary>
    /// Interaction logic for Homemenu.xaml
    /// </summary>
    public partial class Homemenu : Window
    {
        OracleConnection conn;
        DataSet db;
        public Homemenu()
        {
            InitializeComponent();
            this.conn = App.Connection;

            showmenu();

        }

        int jumlah = 0;
        List<string> idmenu = new List<string>();
        List<Button> listbtn = new List<Button>();
        private void showmenu()
        {
            try
            {
                conn.Open();
                string query = $"SELECT count(ID_MENU) from MENU";
                OracleCommand cmd = new OracleCommand(query, conn);
                jumlah = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                string query2 = "SELECT * FROM MENU";
                OracleCommand cmd2 = new OracleCommand(query2, conn);
                cmd2 = new OracleCommand(query2, conn);
                cmd2.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd2);
                db = new DataSet();
                adapter.Fill(db);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
            Button btn = new Button();
            int posx = 5;
            int posy = 70;
            int ctr = 0;
            int counter = 0;
            for (int i = 0; i < jumlah; i++)
            {
                idmenu.Add(db.Tables[0].Rows[i]["id_menu"].ToString());
                btn = new Button();
                btn.Width = 120;
                btn.Height = 50;
                btn.Background = Brushes.Transparent;
                btn.Foreground = Brushes.Black;
                btn.Content = db.Tables[0].Rows[i]["nama_menu"].ToString();
                btn.Name = "btns" + ctr;
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.Margin = new Thickness((counter * 30) + posx, posy, 0, 0);
                counter++;
                if (counter == 3)
                {
                    posy += 55;
                    posx = 5;
                    counter = 0;
                }
                else
                {
                    posx += 105;
                }
                btn.Click += Btn_Click;
                listbtn.Add(btn);
                ctr++;
            }
            for (int i = 0; i < listbtn.Count; i++)
            {
               gridmenu.Children.Add(listbtn[i]);
            }
            
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button name = ((Button)sender);
            string tmp = name.Name.ToString();
            string[] pt = tmp.Split('s');
            int row = Convert.ToInt32(pt[1]);
            string namamenu = db.Tables[0].Rows[row]["nama_menu"].ToString();
            string desmenu = db.Tables[0].Rows[row]["deskripsi"].ToString();
            string hargamenu = "Rp ";
            int harga = Convert.ToInt32(db.Tables[0].Rows[row]["harga_menu"].ToString());
            string depan = (harga / 1000).ToString();
            string belakang = ".000,00";
            hargamenu += depan + belakang;
            nmenu.Content = namamenu;
            hmenu.Content = hargamenu;
            dmenu.Content = desmenu;
            gambarmenu.Source = new BitmapImage(new Uri(db.Tables[0].Rows[row]["gambar"].ToString(), UriKind.Relative));

        }
    }
}

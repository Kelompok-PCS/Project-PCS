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
using System.Windows.Threading;

namespace Project
{
    /// <summary>
    /// Interaction logic for Pilih_Meja.xaml
    /// </summary>
    public partial class Pilih_Meja : UserControl
    {
        OracleConnection con;
        DataSet ds;
        public Pilih_Meja()
        {
            InitializeComponent();
            con = App.Connection;
            callMeja();
            DispatcherTimerSample();
            this.lbtn = Form_pegawai.lbtn;
            getDetail_Meja();
        }
        List<Button> lbtn = new List<Button>();
        private void getDetail_Meja()
        {
            string detail=" ";
            foreach (Button item in lbtn)
            {
                detail += item.Content + ",";
            }
            detail_meja.Text = detail.Substring(0,detail.Length-1);
            jumlah_meja.Text = lbtn.Count().ToString();
        }
        private void callMeja()
        {
            
            grid_meja.Children.Clear();
            using(OracleDataAdapter adap = new OracleDataAdapter("select * from meja", con))
            {
                ds = new DataSet();
                adap.Fill(ds);
            }
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Button btn = new Button();
                btn.Width = 50;
                btn.Height = 50;
                if (item.ItemArray[3].ToString() == "2") btn.Background = Brushes.Red;
                else btn.Background = Brushes.Green;

                foreach (Button items in Form_pegawai.lbtn)
                {
                    if (items.Content.ToString()== item.ItemArray[0].ToString())
                    {
                        btn.Background = Brushes.Blue;
                    }
                }

                btn.Tag = item.ItemArray[3].ToString();
                foreach (Button items in Form_pegawai.lbtn)
                {
                    if (items.Content.ToString() == item.ItemArray[0].ToString())
                    {
                        btn.Background = Brushes.Blue;
                        btn.Tag = "3";
                    }
                }
                btn.Content = item.ItemArray[0].ToString();
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.VerticalAlignment = VerticalAlignment.Top;
                int kanan = 60*(Convert.ToInt32(item.ItemArray[1].ToString())-1)+10;
                int atas = 60*(Convert.ToInt32(item.ItemArray[2].ToString())-1)+10;
                btn.Margin = new Thickness(kanan,atas,0,0);
                btn.Click += ubah_meja;
                grid_meja.Children.Add(btn);
            }
        }

        private void ubah_meja(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Tag.ToString() == "2")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show($"Apakah Meja Nomor {btn.Content} sudah selesai ?", "Confirmasi Selesai Meja", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    btn.Tag = "1";
                    update_meja(1, Convert.ToInt32(btn.Content));
                    btn.Background = Brushes.Green;
                }
            }
            else if (btn.Tag.ToString() == "1")
            {
                btn.Tag = "3";
                btn.Background = Brushes.Blue;
                lbtn.Add(btn);
            }
            else if (btn.Tag.ToString() == "3")
            {
                btn.Tag = "1";
                btn.Background = Brushes.Green;
                foreach (Button item in lbtn)
                {
                    if (item.Content.ToString()==btn.Content.ToString())
                    {
                        System.Windows.Forms.MessageBox.Show("Test");
                        lbtn.Remove(item);
                        break;
                    }
                }
            }
            getDetail_Meja();
        }

        private void update_meja(int nomor,int kode)
        {
            App.Connection.Open();
            string query = $"UPDATE meja set status={nomor} where id_meja={kode}";
            OracleCommand cmd = new OracleCommand(query, App.Connection);
            cmd.ExecuteNonQuery();
            App.Connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string meja = "";
            for (int i = 0; i < lbtn.Count; i++)
            {
                meja += lbtn[i].Content + " ";
            }
            MessageBox.Show(meja);
        }

        private void DispatcherTimerSample()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            callMeja();
        }
    }
}

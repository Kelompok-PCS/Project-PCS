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
    /// Interaction logic for struk.xaml
    /// </summary>
    public partial class struk : UserControl
    {
        private class struk_c
        {
            public string nama { get; set; }
            public string kode { get; set; }

            public struk_c(string nama, string kode)
            {
                this.nama = nama;
                this.kode = kode;
            }
        }
        List<struk_c> lstruk = new List<struk_c>();
        public struk()
        {
            InitializeComponent();
            using (OracleDataAdapter adap = new OracleDataAdapter("SELECT * from hjual where to_char(TANGGAL_TRANSAKSI)= to_char(sysdate)", App.Connection))
            {
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    lstruk.Add(new struk_c(item[3].ToString(),item[0].ToString()));
                }
                tbId.ItemsSource = lstruk;
                tbId.DisplayMemberPath = "nama";
                tbId.SelectedValuePath = "kode";
                tbId.SelectedIndex = 0;
            }
        }
        string id_hjual;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrystalReport1 rpt = new CrystalReport1();
            rpt.SetDatabaseLogon("proyek", "proyek123", "orcl", "");
            rpt.SetParameterValue("id_hjual", tbId.SelectedValue.ToString());
            viewer.ViewerCore.ReportSource = rpt;

            id_hjual = tbId.SelectedValue.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.Connection.Open();
            string query = $"SELECT keterangan from hjual where id_hjual='{id_hjual}' ";
            OracleCommand cmd = new OracleCommand(query, App.Connection);
            string keterangan = cmd.ExecuteScalar().ToString();

            query = $"SELECT jenis_pemesanan from hjual where id_hjual='{id_hjual}' ";
            cmd = new OracleCommand(query, App.Connection);
            string jenis_p = cmd.ExecuteScalar().ToString();
            int stat = 1;
            if (jenis_p == "Reservasi")
            {
                query = $"SELECT status from hjual where id_hjual='{id_hjual}' ";
                cmd = new OracleCommand(query, App.Connection);
                string status_p = cmd.ExecuteScalar().ToString();
                if (status_p == "1")
                {
                    query = $"UPDATE hjual set status='2' where id_hjual='{id_hjual}'";
                    cmd = new OracleCommand(query, App.Connection);
                    cmd.ExecuteNonQuery();
                    stat = 2;
                }
            }
            string[] detail_keterangan = keterangan.Split(new string[] { "||" }, StringSplitOptions.None);

            string[] detail = detail_keterangan[1].Split(':');
            if (detail_keterangan[0].Split(':')[1] != "-")
            {

                try
                {
                    detail = detail[1].Split(',');
                    foreach (var item in detail)
                    {
                        query = $"UPDATE meja set status={stat} where id_meja={item}";
                        cmd = new OracleCommand(query, App.Connection);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show(detail_keterangan[0].Split(':')[1]);
                }
            }

            App.Connection.Close();
        }
    }
}

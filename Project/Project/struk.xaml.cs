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
        public struk()
        {
            InitializeComponent();
        }
        string id_hjual;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CrystalReport2 rpt = new CrystalReport2();
            rpt.SetDatabaseLogon(App.user, App.password, App.source, "");
            //rpt.SetParameterValue("id_hjual", tbId.Text);
            viewer.ViewerCore.ReportSource = rpt;

            id_hjual = tbId.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.Connection.Open();
            string query = $"SELECT keterangan from hjual where id_hjual='{id_hjual}'";
            OracleCommand cmd = new OracleCommand(query, App.Connection);
            string keterangan = cmd.ExecuteScalar().ToString();
            string[] detail_keterangan = keterangan.Split(new string[] { "||" }, StringSplitOptions.None);

            string[] detail = detail_keterangan[1].Split(':');
            detail = detail[1].Split(',');
            foreach (var item in detail)
            {
                query = $"UPDATE meja set status=1 where id_meja={item}";
                cmd = new OracleCommand(query, App.Connection);
                cmd.ExecuteNonQuery();
            }

            App.Connection.Close();
        }
    }
}

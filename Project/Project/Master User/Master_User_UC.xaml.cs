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
    /// Interaction logic for Master_User_UC.xaml
    /// </summary>
    public partial class Master_User_UC : UserControl
    {
        //CLASS
        private class provinsi
        {
            public string kode { get; set; }
            public string nama { get; set; }

            public provinsi(string kode, string nama)
            {
                this.kode = kode;
                this.nama = nama;
            }
        }
        private class kota
        {
            public string kode { get; set; }
            public string nama { get; set; }

            public kota(string kode, string nama)
            {
                this.kode = kode;
                this.nama = nama;
            }
        }

        OracleConnection connection;
        DataSet dsMembers;
        List<provinsi> liProvinsi = new List<provinsi>();
        List<kota> likota= new List<kota>();
        string id_members;
        public Master_User_UC(OracleConnection conn)
        {
            InitializeComponent();
            connection = conn;
            getTableMembers();
            getProvinsi();
        }

        //FUNCTION PROCEDURE
        private void getTableMembers()
        {
            using (OracleDataAdapter adap = new OracleDataAdapter("SELECT m.fullname,m.username,m.email,k.nama_kota,m.status from members m join kota k on m.kota=k.kode_kota", connection))
            {
                dsMembers = new DataSet();
                adap.Fill(dsMembers);
                dtgMembers.ItemsSource = dsMembers.Tables[0].DefaultView;
            }
        }
        private void getKota()
        {
            likota = new List<kota>();
            using (OracleDataAdapter adap = new OracleDataAdapter($"SELECT * from kota where kode_daerah='{cbPronvinsi.SelectedValue}'", connection))
            {
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    kota getkota = new kota(item.ItemArray[0].ToString(), item.ItemArray[1].ToString());
                    likota.Add(getkota);
                }
                cbKota.ItemsSource = likota;
                cbKota.DisplayMemberPath = "nama";
                cbKota.SelectedValuePath = "kode";
                cbKota.SelectedIndex = 0;
            }
        }
        private void getProvinsi()
        {
            using (OracleDataAdapter adap = new OracleDataAdapter("SELECT * from daerah", connection))
            {
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    provinsi getProvinsi = new provinsi(item.ItemArray[0].ToString(), item.ItemArray[1].ToString());
                    liProvinsi.Add(getProvinsi);
                }
                cbPronvinsi.ItemsSource = liProvinsi;
                cbPronvinsi.DisplayMemberPath = "nama";
                cbPronvinsi.SelectedValuePath = "kode";
                cbPronvinsi.SelectedIndex = 0;
            }
        }
        public void checkConnection(OracleConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
        }
        //EVENT
        private void cbPronvinsi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getKota();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            try
            {
                string status = "";
                if ((bool)checkStatus.IsChecked)status = "1";
                else status = "0";
                string query = $"UPDATE MEMBERS SET fullname='{tbFullname.Text}',username='{tbUsername.Text}',password='{tbPassword.Text}',email='{tbEmail.Text}',alamat='{tbAlamat.Text}',no_hp={tbNo_Hp.Text},kota='{cbKota.SelectedValue}',kecematan='{cbPronvinsi.SelectedValue}',kode_pos={tbKode_pos.Text},status='{status}' where id_member='{id_members}'";
                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.ExecuteNonQuery();
                checkConnection(connection);
                getTableMembers();
                MessageBox.Show("Berhasil Update");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            connection.Close();
        }

        private void dtgMembers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            connection.Open();
            if (dtgMembers.SelectedIndex != -1)
            {
                try
                {
                    DataTable dt = dsMembers.Tables[0];
                    using (OracleDataAdapter adap = new OracleDataAdapter($"SELECT * from members where email='{dt.Rows[dtgMembers.SelectedIndex]["email"]}'", connection))
                    {
                        dt = new DataTable();
                        adap.Fill(dt);
                        id_members = dt.Rows[0].ItemArray[0].ToString();
                        tbFullname.Text = dt.Rows[0].ItemArray[1].ToString();
                        tbUsername.Text = dt.Rows[0].ItemArray[2].ToString();
                        tbPassword.Text = dt.Rows[0].ItemArray[3].ToString();
                        tbEmail.Text = dt.Rows[0].ItemArray[4].ToString();
                        tbAlamat.Text = dt.Rows[0].ItemArray[5].ToString();
                        tbNo_Hp.Text = dt.Rows[0].ItemArray[6].ToString();

                        OracleCommand cmd = new OracleCommand($"SELECT NAMA_KOTA FROM KOTA WHERE KODE_KOTA='{dt.Rows[0].ItemArray[7].ToString()}'", connection);
                        cbKota.Text = cmd.ExecuteScalar().ToString();

                        cmd = new OracleCommand($"SELECT NAMA_DAERAH FROM DAERAH WHERE KODE_DAERAH='{dt.Rows[0].ItemArray[8].ToString()}'", connection);
                        cbPronvinsi.Text = cmd.ExecuteScalar().ToString();

                        tbKode_pos.Text = dt.Rows[0].ItemArray[9].ToString();
                        if (dt.Rows[0].ItemArray[10].ToString() == "1")
                        {
                            checkStatus.IsChecked = true;
                        }
                        else
                        {
                            checkStatus.IsChecked = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
            connection.Close();
        }
    }
    
}

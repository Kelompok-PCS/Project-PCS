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
    public partial class Master_Pegawai_UC : UserControl
    {
        //CLASS
        private class jabatan
        {
            public string kode { get; set; }
            public string nama { get; set; }

            public jabatan(string kode, string nama)
            {
                this.kode = kode;
                this.nama = nama;
            }
        }

        OracleConnection connection;
        DataSet dsMembers;
        List<jabatan> liJabatan = new List<jabatan>();
        string id_pegawai = "";
        Canvas canvas;
        public Master_Pegawai_UC(Canvas canvas)
        {
            InitializeComponent();
            connection = App.Connection;
            this.canvas = canvas;
            getTablePegawai();
            getJabatan();
        }

        //FUNCTION PROCEDURE
        private void getTablePegawai()
        {
            using (OracleDataAdapter adap = new OracleDataAdapter("SELECT * from pegawai", connection))
            {
                dsMembers = new DataSet();
                adap.Fill(dsMembers);
                dtgMembers.ItemsSource = dsMembers.Tables[0].DefaultView;
            }
        }
        
        private void getJabatan()
        {
            using (OracleDataAdapter adap = new OracleDataAdapter("SELECT * from jabatan", connection))
            {
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    jabatan getjabatan = new jabatan(item.ItemArray[0].ToString(), item.ItemArray[1].ToString());
                    liJabatan.Add(getjabatan);
                }
                cbJabatan.ItemsSource = liJabatan;
                cbJabatan.DisplayMemberPath = "nama";
                cbJabatan.SelectedValuePath = "kode";
                cbJabatan.SelectedIndex = 0;
            }
        }
        public void checkConnection(OracleConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
        }
        //EVENT
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            try
            {
                string status = "";
                if ((bool)checkStatus.IsChecked)status = "1";
                else status = "0";
                string query = $"UPDATE pegawai SET nama='{tbFullname.Text}',jabatan='{cbJabatan.SelectedValue.ToString()}',email='{tbEmail.Text}',nohp={tbNo_Hp.Text},password='{tbPassword.Text}',status='{status}' where id_pegawai='{id_pegawai}'";
                MessageBox.Show(query);
                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.ExecuteNonQuery();
                checkConnection(connection);
                getTablePegawai();
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
                    using (OracleDataAdapter adap = new OracleDataAdapter($"SELECT * from pegawai where id_pegawai='{dt.Rows[dtgMembers.SelectedIndex]["id_pegawai"]}'", connection))
                    {
                        dt = new DataTable();
                        adap.Fill(dt);
                        id_pegawai= dt.Rows[0].ItemArray[0].ToString();
                        tbFullname.Text = dt.Rows[0].ItemArray[1].ToString();
                        cbJabatan.SelectedValue = dt.Rows[0].ItemArray[2].ToString();
                        tbEmail.Text = dt.Rows[0].ItemArray[3].ToString();
                        tbNo_Hp.Text = dt.Rows[0].ItemArray[4].ToString();
                        tbPassword.Text = dt.Rows[0].ItemArray[5].ToString();

                        if (dt.Rows[0].ItemArray[6].ToString() == "1")
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

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Insert_Pegawai_UC panel = new Insert_Pegawai_UC(canvas);
            canvas.Children.Add(panel);
        }
    }
    
}

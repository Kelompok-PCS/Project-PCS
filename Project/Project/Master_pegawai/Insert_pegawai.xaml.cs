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
namespace Project.Master_pegawai
{
    /// <summary>
    /// Interaction logic for Insert_pegawai.xaml
    /// </summary>
    public partial class Insert_pegawai : UserControl
    {
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
        private void getJabatan()
        {
            using (OracleDataAdapter adap = new OracleDataAdapter("SELECT * from jabatan", connection))
            {
                DataTable dt = new DataTable();
                adap.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    lsJabatan.Add(new jabatan(item.ItemArray[0].ToString(), item.ItemArray[1].ToString()));
                }
                cbJabatan.ItemsSource = lsJabatan;
                cbJabatan.DisplayMemberPath = "nama";
                cbJabatan.SelectedValuePath = "kode";
                cbJabatan.SelectedIndex = 0;
            }
        }
        OracleConnection connection;
        List<jabatan> lsJabatan = new List<jabatan>();
        Canvas can;
        public Insert_pegawai(Canvas can)
        {
            InitializeComponent();
            connection = App.Connection;
            getJabatan();
            this.can = can;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (tbNama.Text != "" && tbEmail.Text != "" && tbNohp.Text != "" && cbJabatan.SelectedIndex != -1)
            {
                try
                {
                    connection.Open();
                    OracleTransaction trans = connection.BeginTransaction();
                    try
                    {
                        string kode = "";
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = App.Connection;
                            cmd.CommandText = "autogen_IDpegawai";

                            cmd.Parameters.Add(new OracleParameter()
                            {
                                ParameterName = "pKode",
                                Direction = ParameterDirection.ReturnValue,
                                //size=32767 - 
                                Size = 20
                            });
                            cmd.ExecuteNonQuery();
                            kode = cmd.Parameters["pKode"].Value.ToString();
                        }
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = connection;
                            cmd.CommandText = "proc_Insert_pegawai";

                            cmd.Parameters.Add("kode", kode);
                            cmd.Parameters.Add("nama", tbNama.Text);
                            cmd.Parameters.Add("jabatan", cbJabatan.SelectedValue);
                            cmd.Parameters.Add("email", tbEmail.Text);
                            cmd.Parameters.Add("nohp", tbNohp.Text);
                            cmd.Parameters.Add("password", kode + tbNohp.Text);

                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                        connection.Close();
                        MessageBox.Show("Berhasil Masukan Pegawai");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        connection.Close();
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Gagal Masukan Pegawai");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Harga tidak valid");
                }

            }
            else
            {
                MessageBox.Show("Data belum lengkap");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Update_Pegawai_UC panel = new Update_Pegawai_UC(can);
            can.Children.Add(panel);
        }
    }
}

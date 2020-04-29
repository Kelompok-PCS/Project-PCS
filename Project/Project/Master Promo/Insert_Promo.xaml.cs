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
using Oracle.DataAccess.Client;
namespace Project
{
    /// <summary>
    /// Interaction logic for Insert_kategori.xaml
    /// </summary>
    public partial class Insert_Promo : UserControl
    {
        OracleConnection connection;
		string id;
		DataTable db;
        public Insert_Promo(OracleConnection conn, string id)
        {
            InitializeComponent();
            connection = conn;
			this.id = id;
			if (id.Equals("null"))
			{
				judul.Content = "Insert Promo";
			}
			else
			{
				MessageBox.Show(id);
				judul.Content = "Update Promo";
				db = new DataTable();
				loadpromo();
			}
        }

		public void loadpromo()
		{
			try
			{

				connection.Open();
				string query =
					$"SELECT nama_promo \"Nama Promo\",harga_promo \"Harga Promo\",periode_awal\"Awal Periode\" , periode_akhir\"Akhir Periode\" FROM promo WHERE id_promo = '{id}'";
				OracleCommand cmd = new OracleCommand(query, connection);
				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				adapter.Fill(db);
				foreach (DataRow dataRow in db.Rows)
				{
					tbHarga.Text = dataRow[1].ToString();
					tbNama.Text = dataRow[0].ToString();
					DateTime t1 =(DateTime) dataRow[2];
					//awalP.Text = t1.ToString("dd/mm/yyyy");
					DateTime t2 = (DateTime)dataRow[3];
					//akhirP.Text = t2.ToString("dd/mm/yyyy");

					awalP.SelectedDate = t1;
					akhirP.SelectedDate = t2;
				}


				connection.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				MessageBox.Show("ada kesalahan saat load promo");
				connection.Close();
			}
		}
        private void btnSubmit_Click_1(object sender, RoutedEventArgs e)
        {
			DateTime date = (DateTime)awalP.SelectedDate;
			DateTime date2 = (DateTime)akhirP.SelectedDate;
			bool ch = false;
			int harga = 0;
			if (tbHarga.Text != "")
			{
				if (date < date2)
				{
					if (tbNama.Text != "")
					{
						try
						{
							harga = Convert.ToInt32(tbHarga.Text);
							ch = true;
						}
						catch
						{
							ch = false;
							MessageBox.Show("format harga salah");
						}
					}
					else
					{
						MessageBox.Show("Data belum lengkap");
					}
				}
				else
				{
					MessageBox.Show("format tanggal akhir salah");

				}
			}
			else
			{
				MessageBox.Show("Harga masih kosong");
			}


			if (ch== true  && id.Equals("null"))
            {
                connection.Open();
                OracleTransaction trans = connection.BeginTransaction();
                try
                {
                    string kode = "PRO";
                    string query =
                        "SELECT LPAD(NVL(MAX(SUBSTR(id_promo,-3,3)),'0')+1,3,0) FROM promo ";
                    OracleCommand cmd = new OracleCommand(query, connection);
                    kode += cmd.ExecuteScalar();
					string waktu = date.ToString("yyyy-MM-dd");
					string waktu2 = date2.ToString("yyyy-MM-dd");

					int hargapromo = Convert.ToInt32(tbHarga.Text);
                    query =
                        $"INSERT INTO promo VALUES ('{kode}','{tbNama.Text}',{hargapromo},to_Date('{waktu}','YYYY-MM-DD'),to_Date('{waktu2}','YYYY-MM-DD'),'a',1)";
                    MessageBox.Show(query);
                    cmd = new OracleCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    trans.Commit();
                    connection.Close();
                    MessageBox.Show("Berhasil Masukan Promo");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    connection.Close();
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("Gagal Masukan Promo");
                }

            }
			else
			{
				connection.Open();
				OracleTransaction trans = connection.BeginTransaction();
				try
				{
					
					string waktu = date.ToString("yyyy-MM-dd");
					string waktu2 = date2.ToString("yyyy-MM-dd");

					int hargapromo = Convert.ToInt32(tbHarga.Text);
					string query =
						$"UPDATE promo SET nama_promo= '{tbNama.Text}',harga_promo = {hargapromo}, periode_awal= to_Date('{waktu}','YYYY-MM-DD'),periode_akhir = to_Date('{waktu2}','YYYY-MM-DD') WHERE id_promo = '{id}'";
					MessageBox.Show(query);
					OracleCommand cmd = new OracleCommand(query, connection);
					cmd.ExecuteNonQuery();

					trans.Commit();
					connection.Close();
					MessageBox.Show("Berhasil Update Promo");
				}
				catch (Exception ex)
				{
					trans.Rollback();
					connection.Close();
					MessageBox.Show(ex.StackTrace);
					MessageBox.Show(ex.Message);
					MessageBox.Show("Gagal Update Promo");
				}
			}
            
        }

		
		private void TbHarga_TextChanged(object sender, TextChangedEventArgs e)
		{
			
		}
	}
}

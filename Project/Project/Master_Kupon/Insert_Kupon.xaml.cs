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
    public partial class Insert_Kupon : UserControl
    {
        OracleConnection connection;
		string id;
        Canvas canvas;
		DataTable db;
        public Insert_Kupon(Canvas canvas, string id)
        {
            InitializeComponent();
            connection = App.Connection;
            this.canvas = canvas;
			this.id = id;
			if (id.Equals(" "))
			{
				judul.Content = "Insert Kupon";
                btnSubmit.Content = "Insert";
			}
			else
			{
				judul.Content = "Update Kupon";
                btnSubmit.Content = "Update";
                db = new DataTable();
				loadpromo();
			}
			loadcombo();
        }

		List<string> idmenu;
		List<string> idmenu2;
		public void loadcombo()
		{
			
				try
				{
					connection.Open();
					idmenu = new List<string>();
					idmenu2 = new List<string>();
					string query = "select id_menu from menu";
					OracleCommand cmd = new OracleCommand(query, connection);
					OracleDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						idmenu.Add(reader.GetString(0));
					}

					query = "SELECT ID_MENU FROM KUPON where status_kupon = 1";
					cmd = new OracleCommand(query, connection);
					reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						idmenu2.Add(reader.GetString(0));
					}

					for (int i = 0; i < idmenu2.Count; i++)
					{
						for (int j = 0; j < idmenu.Count; j++)
						{
							if (idmenu2[i].Equals(idmenu[j]))
							{
								idmenu.RemoveAt(j);
							}
						}
					}
					cmbMen.Items.Clear();
					for (int i = 0; i < idmenu.Count; i++)
					{
						cmbMen.Items.Add(idmenu[i]);
					}

					connection.Close();
				}
				catch (Exception ex)
				{
					connection.Close();
					MessageBox.Show(ex.StackTrace);
				MessageBox.Show(ex.Message);
				}
			if (id.Equals(" "))
			{
				cmbMen.SelectedIndex = 0;
			}
			else
			{
				connection.Open();
				string query = $"select id_menu from kupon where id_kupon = '{id}'";
				OracleCommand cmd = new OracleCommand(query, connection);
				string idx = cmd.ExecuteScalar().ToString();
				cmbMen.Items.Add(idx);
				cmbMen.SelectedIndex = idmenu.Count;
				//while (reader.Read())
				//{
				//	if (idmenu2[ctr].Equals(reader.GetString(0)))
				//	{
				//		cmbMen.Items.Add(idmenu2[ctr]);

				//		cmbMen.SelectedIndex = idmenu.Count + ctr;
				//	}
				//	ctr++;
				//}

				connection.Close();
			}
		}

		public void loadpromo()
		{
			try
			{

				connection.Open();
				string query =
					$"SELECT nama_kupon \"Nama Kupon\", harga_kupon \"Harga Kupon\", periode_awal_kupon \"Awal Periode\", periode_akhir_kupon \"Akhir Periode\", sisa_kupon \"Sisa Kupon\" FROM kupon WHERE id_kupon = '{id}'";
				OracleCommand cmd = new OracleCommand(query, connection);
				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				adapter.Fill(db);
				foreach (DataRow dataRow in db.Rows)
				{
					tbNama.Text = dataRow[0].ToString();
					tbHarga.Text = dataRow[1].ToString();
					DateTime t1 =(DateTime) dataRow[2];
					//awalP.Text = t1.ToString("dd/mm/yyyy");
					DateTime t2 = (DateTime)dataRow[3];
					//akhirP.Text = t2.ToString("dd/mm/yyyy");
					tbStok.Text = dataRow[4].ToString();
					awalP.SelectedDate = t1;
					akhirP.SelectedDate = t2;
				}


				connection.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				MessageBox.Show("ada kesalahan saat load kupon");
				connection.Close();
			}
		}
		DateTime date;
		DateTime date2;
        private void btnSubmit_Click_1(object sender, RoutedEventArgs e)
        {
			if (id.Equals(" "))
			{
				bool ch0 = false;
				try
				{
					date = (DateTime)awalP.SelectedDate;
					date2 = (DateTime)akhirP.SelectedDate;
					ch0 = true;
				}
				catch (Exception)
				{
					ch0 = false;
					MessageBox.Show("Belum memilih tanggal");
				}
				bool ch = false;
				//	int harga = 0;

				if (date < date2)
				{
					if (tbNama.Text != "" && tbHarga.Text != "" && tbStok.Text != "")
					{
						ch = true;
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




				if (ch == true && ch0 == true)
				{
					connection.Open();
					OracleTransaction trans = connection.BeginTransaction();
					int harga = 0;
					int stok = 0;
					try
					{
						harga = Convert.ToInt32(tbHarga.Text);
						stok = Convert.ToInt32(tbStok.Text);
						try
						{
							string kode = "KUP";
							string query =
								"SELECT LPAD(NVL(MAX(SUBSTR(id_kupon,-3,3)),'0')+1,3,0) FROM kupon";
							OracleCommand cmd = new OracleCommand(query, connection);
							kode += cmd.ExecuteScalar();
							string waktu = date.ToString("yyyy-MM-dd");
							string waktu2 = date2.ToString("yyyy-MM-dd");

							//int hargakupon = Convert.ToInt32(tbHarga.Text);
							query =
								$"INSERT INTO kupon VALUES ('{kode}','{tbNama.Text}','{cmbMen.SelectedValue}',{harga},to_Date('{waktu}','YYYY-MM-DD'),to_Date('{waktu2}','YYYY-MM-DD'),{stok},1)";
							cmd = new OracleCommand(query, connection);
							cmd.ExecuteNonQuery();

							trans.Commit();
							connection.Close();
							MessageBox.Show("Berhasil Masukan Kupon");
							tbNama.Text = "";
							tbHarga.Text = "";
							tbStok.Text = "";
							cmbMen.SelectedIndex = 0;
						}
						catch (Exception ex)
						{
							trans.Rollback();
							connection.Close();
							MessageBox.Show(ex.Message);
							MessageBox.Show("Gagal Masukan Kupon");
						}
					}
					catch (Exception)
					{
						connection.Close();
						MessageBox.Show("Harga atau stok tidak valid");
					}
				}
			}
			else
			{
				connection.Open();
				OracleTransaction trans = connection.BeginTransaction();
				int harga = 0;
				int stok = 0;
				try
				{
					string waktu = date.ToString("yyyy-MM-dd");
					string waktu2 = date2.ToString("yyyy-MM-dd");
					harga = Convert.ToInt32(tbHarga.Text);
					stok = Convert.ToInt32(tbStok.Text);
					string query =
						$"UPDATE kupon SET nama_kupon= '{tbNama.Text}', id_menu = '{cmbMen.SelectedValue}', harga_kupon = {harga}, periode_awal= to_Date('{waktu}','YYYY-MM-DD'),periode_akhir = to_Date('{waktu2}','YYYY-MM-DD'), stok_kupon = {stok} WHERE id_kupon = '{id}'";
					MessageBox.Show(query);
					OracleCommand cmd = new OracleCommand(query, connection);
					cmd.ExecuteNonQuery();

					trans.Commit();
					connection.Close();
					MessageBox.Show("Berhasil Update Kupon");
				}
				catch (Exception ex)
				{
					trans.Rollback();
					connection.Close();
					MessageBox.Show(ex.StackTrace);
					MessageBox.Show(ex.Message);
					MessageBox.Show("Gagal Update Kupon");
				}
			}
            
        }

		
		private void TbHarga_TextChanged(object sender, TextChangedEventArgs e)
		{
			
		}

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Master_kupon masterkupo = new Master_kupon(canvas);
            canvas.Children.Add(masterkupo);
        }

		private void CmbMen_Loaded(object sender, RoutedEventArgs e)
		{
			loadcombo();
		}
	}
}

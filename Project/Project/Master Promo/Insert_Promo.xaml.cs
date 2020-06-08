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
using Microsoft.Win32;

namespace Project
{
    /// <summary>
    /// Interaction logic for Insert_kategori.xaml
    /// </summary>
    public partial class Insert_Promo : UserControl
    {
        OracleConnection connection;
		string id;
        Canvas canvas;
		DataTable db;
		string filename;
		string target;
		public Insert_Promo(Canvas canvas, string id)
        {
            InitializeComponent();
            connection = App.Connection;
            this.canvas = canvas;
			this.id = id;
			if (id.Equals(" "))
			{
				judul.Content = "Insert Promo";
                btnSubmit.Content = "Insert";

			}
			else
			{
				judul.Content = "Update Promo";
                btnSubmit.Content = "Update";
                db = new DataTable();
				loadpromo();
			}
			loadcombo();
        }

		private void loadcombo()
		{
			cmbJen.Items.Clear();
			cmbJen.Items.Add("Hemat");
			cmbJen.Items.Add("Hari Raya");
			cmbJen.Items.Add("Menu");
			cmbJen.Items.Add("Buy 1 Get 1 Free");
			if(id.Equals(" "))
			{
				cmbJen.SelectedIndex = 0;
			}
			else
			{
				try
				{
					connection.Open();
					string query = $"select jenis_promo from promo where id_promo = '{id}'";
					OracleCommand cmd = new OracleCommand(query, connection);
					string temp = cmd.ExecuteScalar().ToString();
					if (temp.Equals("H"))
					{
						cmbJen.SelectedIndex = 0;
					}else if (temp.Equals("HR"))
					{
						cmbJen.SelectedIndex = 1;
					}
					else if (temp.Equals("M"))
					{
						cmbJen.SelectedIndex = 2;
					}
					else
					{
						cmbJen.SelectedIndex = 3;
					}
					connection.Close();
				}catch(Exception ex)
				{
					connection.Close();
					MessageBox.Show(ex.StackTrace);
				}
			}
		
		}

		public void loadpromo()
		{
			try
			{

				connection.Open();
				string query =
					$"SELECT nama_promo \"Nama Promo\",periode_awal \"Awal Periode\" , periode_akhir\"Akhir Periode\", detail_promo \"Detail\", gambar_promo FROM promo WHERE id_promo = '{id}'";
				OracleCommand cmd = new OracleCommand(query, connection);
				OracleDataAdapter adapter = new OracleDataAdapter(cmd);
				adapter.Fill(db);
				foreach (DataRow dataRow in db.Rows)
				{
					tbNama.Text = dataRow[0].ToString();
					DateTime t1 =(DateTime) dataRow[1];
					//awalP.Text = t1.ToString("dd/mm/yyyy");
					DateTime t2 = (DateTime)dataRow[2];
					//akhirP.Text = t2.ToString("dd/mm/yyyy");
					tbDet.Text = dataRow[3].ToString();
					sourcetxt.Text = dataRow[4].ToString();
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
		DateTime date;
		DateTime date2;
        private void btnSubmit_Click_1(object sender, RoutedEventArgs e)
        {
			if (id.Equals(" "))
			{
				bool ch0 = false;
				string jenis = "";
				string temp = cmbJen.SelectedValue.ToString();
				if (temp.Equals("Hemat"))
				{
					jenis = "H";
				}
				else if (temp.Equals("Hari Raya"))
				{
					jenis = "HR";
				}
				else if (temp.Equals("Menu"))
				{
					jenis = "M";
				}
				else
				{
					jenis = "X";
				}
				try
				{
					date = (DateTime)awalP.SelectedDate;
					date2 = (DateTime)akhirP.SelectedDate;
					ch0 = true;
				}
				catch
				{
					ch0 = false;
					MessageBox.Show("Belum memilih tanggal");
				}

				bool ch = false;
				//	int harga = 0;

				if (date < date2)
				{
					if (tbNama.Text != "" && tbDet.Text != "" && sourcetxt.Text != "")
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
					try
					{
						System.IO.File.Copy(filename, target);
						try
						{
							string kode = "PR";
							string query =
								"SELECT LPAD(NVL(MAX(SUBSTR(id_promo,-3,3)),'0')+1,3,0) FROM promo ";
							OracleCommand cmd = new OracleCommand(query, connection);
							kode += cmd.ExecuteScalar();
							string waktu = date.ToString("yyyy-MM-dd");
							string waktu2 = date2.ToString("yyyy-MM-dd");

							//int hargapromo = Convert.ToInt32(tbHarga.Text);
							query =
								$"INSERT INTO promo VALUES ('{kode}','{tbNama.Text}',to_Date('{waktu}','YYYY-MM-DD'),to_Date('{waktu2}','YYYY-MM-DD'),'{sourcetxt.Text}','{tbDet.Text}','{jenis}',1)";
							MessageBox.Show(query);
							cmd = new OracleCommand(query, connection);
							cmd.ExecuteNonQuery();

							trans.Commit();
							connection.Close();
							MessageBox.Show("Berhasil Masukan Promo");
							tbNama.Text = "";
							cmbJen.SelectedIndex = 0;
							tbDet.Text = "";
							sourcetxt.Text = "";
							awalP.Text = "";
							akhirP.Text = "";
						}
						catch (Exception ex)
						{
							trans.Rollback();
							connection.Close();
							MessageBox.Show(ex.Message);
							MessageBox.Show("Gagal Masukan Promo");
						}
					}
					catch (Exception)
					{

						throw;
					}
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

					string query =
						$"UPDATE promo SET nama_promo= '{tbNama.Text}', periode_awal= to_Date('{waktu}','YYYY-MM-DD'),periode_akhir = to_Date('{waktu2}','YYYY-MM-DD') WHERE id_promo = '{id}'";
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Menu_Promo menu_promo = new Menu_Promo(canvas);
            canvas.Children.Add(menu_promo);
        }

		private void loadfile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.DefaultExt = ".png";
			dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

			Nullable<bool> result = dlg.ShowDialog();
			if (result == true)
			{
				filename = dlg.FileName;
				string[] paths = filename.Split('\\');
				string strImage = paths[paths.Length - 1];
				string directoryProject = Environment.CurrentDirectory;
				paths = directoryProject.Split('\\');
				for (int i = 0; i < 6; i++)
				{
					target += paths[i] + "\\";
				}
				target += "Image\\" + strImage;

				sourcetxt.Text = "Image/" + strImage;
			}
		}

		private void CmbJen_Loaded(object sender, RoutedEventArgs e)
		{
			loadcombo();
		}
	}
}

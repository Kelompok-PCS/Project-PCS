using Oracle.DataAccess.Client;
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

namespace Project.Pegawai
{
    /// <summary>
    /// Interaction logic for Regis_Member.xaml
    /// </summary>
    public partial class Regis_Member : UserControl
    {
		OracleConnection conn;
		private class daerah
		{
			public string kode { get; set; }
			public string nama { get; set; }

			public daerah(string kode, string nama)
			{
				this.kode = kode;
				this.nama = nama;
			}
		}

		private class kotakecamatan
		{
			public string kode { get; set; }
			public string nama { get; set; }

			public kotakecamatan(string kode, string nama)
			{
				this.kode = kode;
				this.nama = nama;
			}
		}


		List<daerah> kota;

		public Regis_Member()
        {
            InitializeComponent();
			this.conn = App.Connection;
			loaddaerah();
		}

		void loaddaerah()
		{
			kota = new List<daerah>();
			try
			{
				conn.Open();
				string query1 =
					"SELECT * FROM daerah ";
				OracleCommand cmd = new OracleCommand(query1, conn);
				OracleDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					kota.Add(new daerah(reader.GetString(0), reader.GetString(1)));
				}
				combokota.ItemsSource = kota;
				combokota.DisplayMemberPath = "nama";
				combokota.SelectedValuePath = "kode";
				conn.Close();
			}
			catch (Exception ex)
			{
				conn.Close();
				MessageBox.Show(ex.Message);
			}
			combokota.SelectedIndex = 0;
			string query =
				"SELECT * " +
				"FROM kota " +
				$"WHERE kode_daerah = '{combokota.SelectedValue}'";
			loadkec(query);
		}
		List<kotakecamatan> namakota;
		void loadkec(string query)
		{
			namakota = new List<kotakecamatan>();
			conn.Open();
			OracleCommand cmd = new OracleCommand(query, conn);
			
			OracleDataReader reader = cmd.ExecuteReader();
			string id_daerah ="";
			while (reader.Read())
			{
				namakota.Add(new kotakecamatan(reader.GetString(0),reader.GetString(1)));
				id_daerah = reader.GetString(2);
			}
			//combokecamatan.Items.Clear();
			combokecamatan.ItemsSource = namakota;
			combokecamatan.DisplayMemberPath = "nama";
			combokecamatan.SelectedValuePath = "nama";
			combokecamatan.SelectedIndex = 0;

			query = $"select nama_daerah from daerah where kode_daerah = '{id_daerah}'";

			cmd = new OracleCommand(query, conn);
			
			prov = cmd.ExecuteScalar().ToString();
			

			conn.Close();
			//MessageBox.Show(prov);
		}
		string prov ="";
		private void Combokota_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
			string query =
				"SELECT * " +
				"FROM kota " +
				$"WHERE kode_daerah = '{combokota.SelectedValue}'";
			loadkec(query);
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			bool cek = false;
			bool cek2 = false;
			bool cek3 = false;
			bool cek4 = false;
			int tlp = 0;
			
				if (!tbFullname.Text.Equals(""))
				{
					if (!tbTelp.Text.Equals(""))
					{
						if(tbTelp.Text.Length <= 13)
						{
							try
							{
								tlp = Convert.ToInt32(tbTelp.Text);

								conn.Open();
								string query = $"select count(*) from members where no_hp ={tlp} ";
								OracleCommand cmd = new OracleCommand(query, conn);
								int banyak = 0;
								banyak = Convert.ToInt32(cmd.ExecuteScalar().ToString());
								if(banyak > 0)
								{
									MessageBox.Show("no telp sudah terdaftar");
									tbTelp.Text = "";
								}
								else
								{
									cek = true;
								}

								conn.Close();
							}
							catch (Exception ex)
							{
							MessageBox.Show("yang in ");
								MessageBox.Show(ex.StackTrace);
								tbTelp.Text = "";
							}
						}
						else
						{
							MessageBox.Show("Nomor terlalu panjang");
							tbTelp.Text = "";
						}
					}
				}
				else
				{
					MessageBox.Show("Full Name  masih Kosong");
				}
			
			


			if (!tbPass.Password.Equals(""))
			{
				if (tbPass.Password.Equals(tbPass_Copy.Password))
				{
					cek2 = true;
				}
				else
				{
					MessageBox.Show("Password tidak cocok dengan confirm password");
					tbPass.Password = "";
					tbPass_Copy.Password = "";
				}
			}
			else
			{
				MessageBox.Show("Password tidak boleh kosong");
				
			}

			if (!tbEmail1.Text.Equals(""))
			{
				conn.Open();
				string query = $"select count(*) from members where upper(email) = upper('{tbEmail1.Text}') ";
				OracleCommand cmd = new OracleCommand(query, conn);
				int banyak = 0;
				banyak = Convert.ToInt32(cmd.ExecuteScalar().ToString());
				if (banyak > 0)
				{
					MessageBox.Show("Email sudah terdaftar");
					tbEmail1.Text = "";
				}
				else
				{
					 query = $"select count(*) from members where upper(username) = upper('{userName.Text}') ";
					 cmd = new OracleCommand(query, conn);
					int banyak2 = 0;
					banyak2 = Convert.ToInt32(cmd.ExecuteScalar().ToString());

					if (banyak2 > 0)
					{
						MessageBox.Show("Ussername sudah terdaftar");
						userName.Text = "";
					}
					else
					{
						cek3 = true;
					}
				}


				conn.Close();
			}

			int kdepos = 0;

			if (!tbAlamat.Text.Equals(""))
			{
				if (kodepos.Text.Length <= 5)
				{
					try
					{
						kdepos = Convert.ToInt32(kodepos.Text);
						cek4 = true;
					}
					catch
					{
						MessageBox.Show("format kode pos salah");
						kodepos.Text = "";
					}
				}
				else
				{
					MessageBox.Show("format kode pos salah");
					kodepos.Text = "";

				}
			}



			if(cek== true && cek2==true && cek3== true && cek4 == true)
			{
				try
				{
					conn.Open();
					string que = $"SELECT count(*) from members where id_member like '%{ userName.Text.Substring(0, 2).ToUpper() }%'  ";
					OracleCommand cmd = new OracleCommand(que, conn);
					int banyak = Convert.ToInt32(cmd.ExecuteScalar().ToString())+1;
					string bk = "";
					if(banyak < 10)
					{
						bk = "000" + banyak;
					}
					else
					{
						bk = "00" + banyak;
					}
					string id_baru = userName.Text.Substring(0, 2).ToUpper()+bk;


					string query = $"INSERT INTO MEMBERS VALUES ('{id_baru}','{tbFullname.Text}','{userName.Text}','{tbPass.Password}','{tbEmail1.Text}','{tbAlamat.Text}',{tlp},'{prov}','{combokecamatan.SelectedValue}',{kdepos},0,'1')";
					MessageBox.Show(query);
					cmd = new OracleCommand(que, conn);
					cmd.ExecuteNonQuery();
					MessageBox.Show("Berhasil mendaftar");
					conn.Close();

					tbAlamat.Text= "";
					userName.Text = "";
					tbTelp.Text = "";
					tbFullname.Text = "";
					tbPass.Password = "";
					kodepos.Text = "";
					tbPass_Copy.Password = "";
					tbEmail1.Text = "";
				}catch(Exception ex)
				{
					MessageBox.Show(ex.StackTrace);
					MessageBox.Show(ex.Message);
					conn.Close();
				}
			}

		}
	}
}

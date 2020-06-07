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
using System.Windows.Shapes;
using Oracle.DataAccess.Client;
using System.Data;

namespace Project
{
    /// <summary>
    /// Interaction logic for Homemenu.xaml
    /// </summary>
    public partial class Homemenu : UserControl
	{
        OracleConnection conn;
        DataSet db;
		//Canvas can;

		class kategori
		{
			public string kode { get; set; }
			public string nama { get; set; }
		}
		
        public Homemenu()
        {
            InitializeComponent();
            this.conn = App.Connection;
			
			rbmenu.IsChecked = true;
			
		}

        int jumlah = 0;
        List<string> idmenu = new List<string>();
        List<Button> listbtn = new List<Button>();
		List<kategori> listkat = new List<kategori>();

		private void fillcombo()
		{
			listkat.Clear();
			try
			{

				conn.Open();
				string query = "SELECT ID_KATEGORI, NAMA_KATEGORI FROM KATEGORI WHERE STATUS_KATEGORI = 1";
				OracleCommand cmd = new OracleCommand(query,conn);
				OracleDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{

					listkat.Add(new kategori()
					{
						kode = reader.GetString(0),
						nama = reader.GetString(1)
					});
				}
				cmbKategori.ItemsSource = listkat;
				cmbKategori.DisplayMemberPath = "nama";
				cmbKategori.SelectedValuePath = "kode";
				cmbKategori.SelectedIndex = 0;
				conn.Close();

			}catch(Exception ex)
			{
				conn.Close();
				MessageBox.Show(ex.StackTrace);
			}
			
		}

		private void cekpromo(string id)
		{
			try
			{
				//MessageBox.Show(id);
				conn.Open();
				string idpromo = "";
				string namapromo = "";
				string hargapromo = "";
				string query = $"SELECT * FROM PROMO_PAKET WHERE ID_PAKET = '{id}' and status = 1";
				OracleCommand cmd = new OracleCommand(query, conn);
				OracleDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					idpromo = reader.GetString(0);
					hargapromo = reader.GetDecimal(2).ToString();
				}
				string query2 = $"SELECT * FROM promo WHERE id_promo = '{idpromo}'";
				cmd = new OracleCommand(query2, conn);
				reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					namapromo = reader.GetString(1);

				}
				
				if (!namapromo.Equals(""))
				{
					string hargamenu = "Rp ";
					int harga = Convert.ToInt32(hargapromo);
					string depan = (harga / 1000).ToString();
					string belakang = ".000,00";
					hargamenu += depan + belakang;
					hmenu.Content = hargamenu;
					hpromo.Foreground = new SolidColorBrush(Colors.Red);
					hpromo.Content = "**" + namapromo;
				}
				conn.Close();
			}
			catch (Exception ex)
			{
				conn.Close();
				MessageBox.Show("Gagal load harga promo");
				MessageBox.Show(ex.StackTrace);
				MessageBox.Show(ex.Message);
			
			}
		}

        private void showmenu()
        {
			listbtn.Clear();
			gridlistmenu.Children.Clear();
			conn.Close();
            try
            {
                conn.Open();
				string idkat = cmbKategori.SelectedValue.ToString();
				//MessageBox.Show(idkat);
				string query = "";
				string query2 = "";
				if (rbmenu.IsChecked == true)
				{
					query = $"SELECT count(ID_MENU) from MENU where id_kategori ='{idkat}' and status = 1";
					query2 = $"SELECT * FROM MENU where id_kategori ='{idkat}' and status  = 1";
				} else
				{
					query = $"SELECT count(ID_paket) from paket where id_kategori ='{idkat}' and status = 1 ";
					query2 = $"SELECT * FROM paket where id_kategori ='{idkat}' and status =1";
				}
				
                OracleCommand cmd = new OracleCommand(query, conn);
                jumlah = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                 

				OracleCommand cmd2 = new OracleCommand(query2, conn);
                //cmd2 = new OracleCommand(query2, conn);
                cmd2.ExecuteReader();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd2);
                db = new DataSet();
                adapter.Fill(db);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
            Button btn = new Button();
            int posx = 5;
            int posy = 70;
            int ctr = 0;
            int counter = 0;
            for (int i = 0; i < jumlah; i++)
            {
				if(rbmenu.IsChecked == true)
				{
					idmenu.Add(db.Tables[0].Rows[i]["id_menu"].ToString());
				}else
				{
					idmenu.Add(db.Tables[0].Rows[i]["id_paket"].ToString());
				}
                
                btn = new Button();
                btn.Width = 120;
                btn.Height = 50;
                btn.Background = Brushes.Transparent;
                btn.Foreground = Brushes.Black;
				if(rbmenu.IsChecked == true)
				{
					btn.Content = db.Tables[0].Rows[i]["nama_menu"].ToString();
					btn.Tag = db.Tables[0].Rows[i]["id_menu"].ToString();
				} else
				{
					btn.Content = db.Tables[0].Rows[i]["nama_paket"].ToString();
					btn.Tag = db.Tables[0].Rows[i]["id_paket"].ToString();
				}
                btn.Name = "btns" + ctr;
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.Margin = new Thickness((counter * 30) + posx, posy, 0, 0);
                counter++;
                if (counter == 3)
                {
                    posy += 55;
                    posx = 5;
                    counter = 0;
                }
                else
                {
                    posx += 105;
                }
                btn.Click += Btn_Click;
                listbtn.Add(btn);
				
                ctr++;
            }
            for (int i = 0; i < listbtn.Count; i++)
            {
               gridlistmenu.Children.Add(listbtn[i]);
            }
			conn.Close();
            
        }
        string id_menu = "";
        private void Btn_Click(object sender, RoutedEventArgs e)
        {

            Button name = ((Button)sender);
            string tmp = name.Name.ToString();
            string[] pt = tmp.Split('s');
            int row = Convert.ToInt32(pt[1]);
			int harga = 0;
			string namamenu = "";
			string desmenu = "";
			if(rbmenu.IsChecked== true)
			{
				 namamenu = db.Tables[0].Rows[row]["nama_menu"].ToString();
				 desmenu = db.Tables[0].Rows[row]["deskripsi"].ToString();
				 harga = Convert.ToInt32(db.Tables[0].Rows[row]["harga_menu"].ToString());
				gambarmenu.Source = new BitmapImage(new Uri(db.Tables[0].Rows[row]["gambar"].ToString(), UriKind.Relative));

			}else
			{
				namamenu = db.Tables[0].Rows[row]["nama_paket"].ToString();
				harga = Convert.ToInt32(db.Tables[0].Rows[row]["harga_paket"].ToString());
				gambarmenu.Source = new BitmapImage(new Uri(db.Tables[0].Rows[row]["gambar_paket"].ToString(), UriKind.Relative));
			
			}

			string hargamenu = "Rp ";
           
            string depan = (harga / 1000).ToString();
            string belakang = ".000,00";
			
            hargamenu += depan + belakang;
            nmenu.Content = namamenu;
            hmenu.Content = hargamenu;
            dmenu.Content = desmenu;
			if(rbmenu.IsChecked== true)
			{
				cekpromo(db.Tables[0].Rows[row]["id_menu"].ToString());
			}else
			{
				cekpromo(db.Tables[0].Rows[row]["id_paket"].ToString());
			}
			
			id_menu = name.Tag.ToString();

        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
			
			
            if (App.lMenu.Count != 0 )
            {
                int ctr = 0;
                for (int i = 0; i < App.lMenu.Count; i++)
                {
                    if (App.lMenu[i].nama == id_menu)
                    {
                        int jum = Convert.ToInt32(tbJumlah.Text);
                        App.lMenu[i].jumlah += jum;
                        ctr = 1;
                        System.Windows.Forms.MessageBox.Show("berhasil tambah jumlah");
                    }
                }
                if (ctr == 0)
                {
                    App.lMenu.Add(new App.menu(id_menu, Convert.ToInt32(tbJumlah.Text)));
                    System.Windows.Forms.MessageBox.Show("Mohon input jumlah menu");
                }
            }
            else
            {
                App.lMenu.Add(new App.menu(id_menu, Convert.ToInt32(tbJumlah.Text)));
                System.Windows.Forms.MessageBox.Show("gagal tambah menu");
            }
        }

        private void jumlah_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int jum = Convert.ToInt32(tbJumlah.Text);
            }
            catch (Exception ex)
            {
                try
                {
                    tbJumlah.Text = tbJumlah.Text.Substring(0, tbJumlah.Text.Length - 1);
                    if (tbJumlah.Text == "")
                    {
                        tbJumlah.Text = "0";
                    }
                }
                catch (Exception)
                {
                    tbJumlah.Text = "0";
                }
            }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            int jum = Convert.ToInt32(tbJumlah.Text);
            jum++;
            tbJumlah.Text = jum.ToString();
        }

        private void min_Click(object sender, RoutedEventArgs e)
        {
            int jum = Convert.ToInt32(tbJumlah.Text);
            jum--;
            if (jum < 0)
            {
                jum = 0;
            }
            tbJumlah.Text = jum.ToString();
        }

		private void CmbKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			showmenu();
		}

		private void Rbmenu_Checked(object sender, RoutedEventArgs e)
		{
			judul.Content = "Menu";
			fillcombo();
			showmenu();
		}

		private void RbPaket_Checked(object sender, RoutedEventArgs e)
		{
			judul.Content = "Paket";
			fillcombo();
			showmenu();
		}
	}
}

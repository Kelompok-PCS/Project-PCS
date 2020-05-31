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

namespace Project
{
	/// <summary>
	/// Interaction logic for Laporan.xaml
	/// </summary>
	public partial class Laporan : UserControl
	{
		Canvas can;
		string tipe = "";
		string tipe2 = "";
		string tipe3 = "";

		public Laporan(Canvas can)
		{
			InitializeComponent();
			this.can = can;
		}

		private void print_Click(object sender, RoutedEventArgs e)
		{
			//Detillaporan det = new Detillaporan();
			//det.ShowDialog();
			//MessageBox.Show(inptgl.SelectedDate.ToString());
			LaporanHari rpt;
			LaporanFav rpt2;
			LaporanBanyak rpt3;
			LaporanBanyak2 rpt4;
			if (tipe2.Equals("TRANSAKSI"))
			{
				rpt = new LaporanHari();

				string tgl = "";
				string[] input;
				char[] sp = { '/' };
				if (tipe.Equals("HARIAN"))
				{
					tgl = inptgl.Text;
				}
				else
				{
					tgl = inpbln.Text;
					input = tgl.Split(sp);
					tgl = input[1];
					MessageBox.Show(tgl);
				}
				rpt.SetDatabaseLogon(App.user, App.password, App.source, "");
				rpt.SetParameterValue("tlap", tipe);
				rpt.SetParameterValue("tanggal", tgl);
				viewer.ViewerCore.ReportSource = rpt;
			}
			else if (tipe2.Equals("FAVORIT"))
			{
				rpt2 = new LaporanFav();
				string tgl = "";
				string[] input;
				char[] sp = { '/' };
				if (tipe.Equals("HARIAN"))
				{
					tgl = inptgl.Text;
				}
				else
				{
					tgl = inpbln.Text;
					input = tgl.Split(sp);
					tgl = input[1];
					MessageBox.Show(tgl);
				}
				rpt2.SetDatabaseLogon(App.user, App.password, App.source, "");
				rpt2.SetParameterValue("tlap", tipe);
				rpt2.SetParameterValue("tanggal", tgl);
				viewer.ViewerCore.ReportSource = rpt2;
			}
			else
			{
				string tgl = "";
				string[] input;
				char[] sp = { '/' };
				tgl = inpbln.Text;
				input = tgl.Split(sp);
				tgl = input[1];
				if (tipe3.Equals("MEMBER"))
				{
					rpt3 = new LaporanBanyak();
					rpt3.SetDatabaseLogon(App.user, App.password, App.source, "");
					rpt3.SetParameterValue("tlap", tipe);
					rpt3.SetParameterValue("tanggal", tgl);
					viewer.ViewerCore.ReportSource = rpt3;
				}
				else
				{
					rpt4 = new LaporanBanyak2();
					rpt4.SetDatabaseLogon(App.user, App.password, App.source, "");
					rpt4.SetParameterValue("tlap", tipe);
					rpt4.SetParameterValue("tanggal", tgl);
					viewer.ViewerCore.ReportSource = rpt4;
				}
			}

			//viewer.Owner = Window.GetWindow(this);
		}

		private void rb1_Checked(object sender, RoutedEventArgs e)
		{
			inpbln.IsEnabled = false;
			inptgl.IsEnabled = true;
			tipe = "HARIAN";
			tipe2 = "TRANSAKSI";

		}

		private void rb3_Checked(object sender, RoutedEventArgs e)
		{
			inpbln.IsEnabled = true;
			inptgl.IsEnabled = false;
			tipe = "BULANAN";
			tipe2 = "TRANSAKSI";
		}

		private void rb4_Checked(object sender, RoutedEventArgs e)
		{
			inpbln.IsEnabled = false;
			inptgl.IsEnabled = true;
			tipe2 = "FAVORIT";
		}

		private void rb5_Checked(object sender, RoutedEventArgs e)
		{
			inpbln.IsEnabled = true;
			inptgl.IsEnabled = false;
			tipe2 = "FAVORIT";
		}

		private void rb6_Checked(object sender, RoutedEventArgs e)
		{
			inpbln.IsEnabled = true;
			inptgl.IsEnabled = false;
			tipe = "BULANAN";
			tipe2 = "TERBANYAK";
			tipe3 = "MEMBER";
		}

		private void rb7_Checked(object sender, RoutedEventArgs e)
		{
			inpbln.IsEnabled = true;
			inptgl.IsEnabled = false;
			tipe = "BULANAN";
			tipe2 = "TERBANYAK";
			tipe2 = "PEMESANAN";
		}
	}
}

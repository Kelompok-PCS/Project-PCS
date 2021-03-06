﻿using System;
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
using Project.Master_menu;
using Project.Master_paket;

namespace Project
{
    /// <summary>
    /// Interaction logic for Form_Utama.xaml
    /// </summary>
    public partial class Form_Utama : Window
    {
        OracleConnection conn;
        Login_pegawai logpeg;
        public Form_Utama(OracleConnection conn, Login_pegawai logpeg)
        {
            InitializeComponent();
            this.conn = conn;
            this.logpeg = logpeg;
        }
        public void checkConnection(OracleConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == true)
            {
                tt_menu.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_menu.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            mm.Text = "MM";
            MK.Text = "MK";
            mp.Text = "MP";
            mpr.Text = "MR";
            kp.Text = "KP";
            mb.Text = "MB";
            pg.Text = "PG";
            lpr.Text = "LP";
            logout.Text = "LOG";
        }

        private void to_menu_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void to_menu_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            canvas.Children.Clear();
            Master_menu.Menu_makanan_UC menu_Makanan = new Menu_makanan_UC(canvas);
            canvas.Children.Add(menu_Makanan);
        }

        private void to_kategori_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkConnection(conn);
            canvas.Children.Clear();
            Master_Kategori_UC panel = new Master_Kategori_UC(canvas);
            canvas.Children.Add(panel);

        }

        private void to_paket_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkConnection(conn);
            canvas.Children.Clear();
            Paket panel = new Paket(canvas);
            canvas.Children.Add(panel);
        }

        private void to_promo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkConnection(conn);
            canvas.Children.Clear();
            Menu_Promo panel = new Menu_Promo(canvas);
            canvas.Children.Add(panel);
        }

        private void to_kupon_MouseDown(object sender, MouseButtonEventArgs e)
        {
			checkConnection(conn);
			canvas.Children.Clear();
			Master_kupon panel = new Master_kupon(canvas);
			canvas.Children.Add(panel);
		}

        private void to_member_MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkConnection(conn);
            canvas.Children.Clear();
            Master_User_UC panel = new Master_User_UC(conn);
            canvas.Children.Add(panel);
        }

        private void to_pegawai_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvas.Children.Clear();
            Update_Pegawai_UC panel = new Update_Pegawai_UC(canvas);
            canvas.Children.Add(panel);
        }

        private void to_laporan_MouseDown(object sender, MouseButtonEventArgs e)
        {
			canvas.Children.Clear();
			Laporan lap= new Laporan(canvas);
			canvas.Children.Add(lap);
		}

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            mm.Text = "Master Menu";
            MK.Text = "Master Kategori";
            mp.Text = "Master Paket";
            mpr.Text = "Master Promo";
            kp.Text = "Master Kupon";
            mb.Text = "Master Member";
            pg.Text = "Master Pegawai";
            lpr.Text = "Laporan";
            logout.Text = "Log Out";
        }

        private void to_logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            logpeg.Show();
        }
    }
}

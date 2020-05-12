﻿﻿using System;
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

namespace Project
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : Window
    {
        OracleConnection conn;
        public Homepage()
        {
            InitializeComponent();
            this.conn = App.Connection;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Form_Utama Form_Utama = new Form_Utama(conn);
            Form_Utama.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Form_pegawai pegawai = new Form_pegawai();
            pegawai.ShowDialog();
        }
    }
}
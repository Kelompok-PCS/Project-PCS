using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static OracleConnection Connection = new OracleConnection();
        public static string user ="proyek";
        public static string password ="proyek123";
        public static string source ="orcl";

        public class menu
        {
            public string nama { get; set; }
            public int jumlah { get; set; }

            public menu(string nama, int jumlah)
            {
                this.nama = nama;
                this.jumlah = jumlah;
            }
        }
        public static List<menu> lMenu = new List<menu>();
    }
}

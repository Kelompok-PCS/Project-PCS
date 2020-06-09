using Oracle.DataAccess.Client;
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

namespace Project
{
    /// <summary>
    /// Interaction logic for Login_pegawai.xaml
    /// </summary>
    public partial class Login_pegawai : Window
    {
        OracleConnection conn;
        public Login_pegawai()
        {
            InitializeComponent();
            this.conn = App.Connection;

        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void tbUser_GotFocus(object sender, RoutedEventArgs e)
        {
            tbEmail.Text = "";
        }

        private void btnLoginPeg_Click(object sender, RoutedEventArgs e)
        {
            string kodePegawai = "test";
            if (tbEmail.Text.ToLower().Equals("admin") && tbPass.Text.ToLower().Equals("admin"))
            {
                Form_Utama form = new Form_Utama(conn, this);
                this.Hide();
                form.ShowDialog();
                
            }
            else
            {
                try
                {
                    conn.Open();
                    string query =
                        $"SELECT id_pegawai FROM pegawai WHERE email = '{tbEmail.Text}' AND password = '{tbPass.Text}'";
                    OracleCommand cmd = new OracleCommand(query, conn);
                    try
                    {
                        kodePegawai = cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception)
                    {
                        conn.Close();
                        MessageBox.Show("email atau password tidak benar");
                    }

                    if (kodePegawai != "")
                    {
                        conn.Close();
                        Form_pegawai pegawai = new Form_pegawai(kodePegawai,this);
                        this.Hide();
                        pegawai.ShowDialog();
                    }

                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("email atau password tidak benar");
                }
            }
        }

        private void tbPass_GotFocus(object sender, RoutedEventArgs e)
        {
            
            tbPass.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbEmail.Text = "Email / Username";
            tbPass.Text = "Password";
        }
    }
}

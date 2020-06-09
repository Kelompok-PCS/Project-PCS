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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.Master_menu;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OracleConnection conn;
        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            string datasource = $"data source=orcl; user id=proyekpcs; password=proyek123";
            conn = new OracleConnection(datasource);
            App.Connection = conn;
           Login_pegawai homepage = new Login_pegawai();
           // this.Hide();
            homepage.ShowDialog();
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.user = tbUser.Text;
                App.password = tbPass.Text;
                App.source = tbData.Text;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void tbData_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void tbData_MouseEnter(object sender, MouseEventArgs e)
        {
             
        }

        private void tbUser_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void tbPass_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void tbData_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void tbData_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbData.Text == "Source")
            {
                tbData.Text = ""; 
            }
        }

        private void tbUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbUser.Text == "Username")
            {
                tbUser.Text = ""; 
            }
        }

        private void tbPass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPass.Text == "Password")
            {
                tbPass.Text = ""; 
            }
        }
    }
}

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
                string datasource = $"data source={tbData.Text}; user id={tbUser.Text}; password={tbPass.Text}";
                conn = new OracleConnection(datasource);
                App.Connection = conn;
                Homepage homepage = new Homepage();
                homepage.ShowDialog();
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
             tbData.Text = "";
        }

        private void tbUser_MouseEnter(object sender, MouseEventArgs e)
        {
            tbUser.Text = "";
        }

        private void tbPass_MouseEnter(object sender, MouseEventArgs e)
        {
            tbPass.Text = "";
        }

        private void tbData_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}

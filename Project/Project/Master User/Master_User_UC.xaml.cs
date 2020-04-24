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
using Oracle.DataAccess.Client;
using System.Data;
namespace Project
{
    /// <summary>
    /// Interaction logic for Master_User_UC.xaml
    /// </summary>
    public partial class Master_User_UC : UserControl
    {
        OracleConnection connection;
        DataSet dsMembers;
        public Master_User_UC(OracleConnection conn)
        {
            InitializeComponent();
            connection = conn;
            getTableMembers();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void getTableMembers()
        {
            using (OracleDataAdapter adap= new OracleDataAdapter("SELECT fullname,username,email,kota,status from members", connection))
            {
                dsMembers = new DataSet();
                adap.Fill(dsMembers);
                dtgMembers.ItemsSource = dsMembers.Tables[0].DefaultView;
            }
        }

    }
}

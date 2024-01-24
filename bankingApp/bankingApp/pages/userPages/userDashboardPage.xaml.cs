using bankingApp.pages.adminPages;
using bankingApp.windows;
using MaterialDesignThemes.Wpf;
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

namespace bankingApp.pages.userPages
{
    /// <summary>
    /// Interaction logic for userDashboardPage.xaml
    /// </summary>
    public partial class userDashboardPage : Page
    {
        bsappEntities db;
        public userDashboardPage(bsappEntities db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void btnAddCard_Click(object sender, RoutedEventArgs e)
        {
            string foo = "";
            var wdw = new selectManagerWindow(db);
            wdw.ShowDialog();
            foo = wdw.foo;

            if (!string.IsNullOrEmpty(foo))
            {
                
            }
        }
    }
}

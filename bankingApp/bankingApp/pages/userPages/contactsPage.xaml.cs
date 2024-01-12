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
    /// Interaction logic for contactsPage.xaml
    /// </summary>
    public partial class contactsPage : Page
    {
        bsappDataContext db;
        public contactsPage(bsappDataContext db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrevPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRezolved_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

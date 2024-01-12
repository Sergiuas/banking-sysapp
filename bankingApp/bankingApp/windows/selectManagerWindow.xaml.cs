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

namespace bankingApp.windows
{
    /// <summary>
    /// Interaction logic for selectManagerWindow.xaml
    /// </summary>
    public partial class selectManagerWindow : Window
    {
        public string foo {
            set; get; }

        bsappDataContext db;
        public selectManagerWindow(bsappDataContext db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = string.Empty;
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            User user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text);
            if (user==null)
                txtUsername.Text = string.Empty;
            Close();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.foo = txtUsername.Text.Trim();
        }
    }
}

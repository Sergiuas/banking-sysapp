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
    /// Interaction logic for selectFriendWindow.xaml
    /// </summary>
    public partial class selectFriendWindow : Window
    {
        public string foo
        {
            set; get;
        }
        public List<string> users { set; get; }

        bsappEntities db;
        public selectFriendWindow(bsappEntities db)
        {
            this.db = db;
            InitializeComponent();
            users = db.Users.Where(u => u.Type == "manager")
                .Select(u => u.Username).ToList();
            cbUsername.ItemsSource = users;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cbUsername.Text = string.Empty;
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            User user = db.Users.FirstOrDefault(u => u.Username == cbUsername.Text);
            if (user == null)
                cbUsername.Text = string.Empty;
            Close();
        }

        private void cbUsername_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbUsername.SelectedItem != null)
                this.foo = cbUsername.SelectedValue.ToString();
        }

        private void cbUsername_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = cbUsername.Text.ToLower();

            if (e.Key == System.Windows.Input.Key.Back || e.Key == System.Windows.Input.Key.Delete)
            {
                // If Backspace or Delete is pressed, reset the filtering
                cbUsername.ItemsSource = users;
            }
            else
            {
                // Filter the items based on the entered text
                cbUsername.ItemsSource = users
                    .Where(item => item.ToLower().Contains(searchText))
                    .ToList();
            }

            cbUsername.IsDropDownOpen = true;
        }
    }
}

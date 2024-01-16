using bankingApp.classes;
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
    /// Interaction logic for ticketPage.xaml
    /// </summary>
    public partial class ticketPage : Page
    {
        bsappDataContext db = new bsappDataContext();
        public string managerName { get; set; }
        public List<string> managers { set; get; }
        public ticketPage(bsappDataContext db)
        {
            this.db = db;
            InitializeComponent();
            managers = db.Users.Where(u => u.Type == "manager")
                .Select(u => u.Username).ToList();
            cbManagerName.ItemsSource = managers;
        }

        private void cbManagerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbManagerName.SelectedItem != null)
                this.managerName = cbManagerName.SelectedValue.ToString();
        }

        private void cbManagerName_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = cbManagerName.Text.ToLower();

            if (e.Key == System.Windows.Input.Key.Back || e.Key == System.Windows.Input.Key.Delete)
            {
                // If Backspace or Delete is pressed, reset the filtering
                cbManagerName.ItemsSource = managers;
            }
            else
            {
                // Filter the items based on the entered text
                cbManagerName.ItemsSource = managers
                    .Where(item => item.ToLower().Contains(searchText))
                    .ToList();
            }

            cbManagerName.IsDropDownOpen = true;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
           if (string.IsNullOrEmpty(tbBody.Text) || string.IsNullOrEmpty(tbSubject.Text) || string.IsNullOrEmpty(cbManagerName.Text))
            { return; }

           Ticket ticket = new Ticket();
            ticket.Subject = tbSubject.Text;
            ticket.Body = tbBody.Text;
            ticket.UserID = UserSingleton.Instance.UserID;

            var manager = db.Users.SingleOrDefault(u=> u.Username == cbManagerName.Text.ToString());
            ticket.ManagerID = manager.UserID;

            ticket.Timestamp = DateTime.Now;
            ticket.Resolved = false;

            db.Tickets.InsertOnSubmit(ticket);
            db.SubmitChanges();

            tbBody.Clear();
            tbSubject.Clear();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            userDashboardPage page = new userDashboardPage();
            MainContentFrame.Content = page;
        }
    }
}

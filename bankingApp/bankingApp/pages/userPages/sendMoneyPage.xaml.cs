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
    /// Interaction logic for sendMoneyPage.xaml
    /// </summary>
    public partial class sendMoneyPage : Page
    {
        bsappDataContext db;
        public string friendName;
        public string iban;
        public List<string> friends;
        public List<string> ibans;
        public sendMoneyPage(bsappDataContext db)
        {
            this.db = db;
            InitializeComponent();
            friends = friends = db.Contacts
                .Where(f => f.UserID == UserSingleton.Instance.UserID)
                .Join(db.Users,
                      contact => contact.FriendID,
                      user => user.UserID,
                      (contact, user) => user.Username)
                .ToList();
            ibans = db.Cards
                .Select(a => a.CardNumber)
                .ToList();
        }
        private void cbFriendname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFriendname.SelectedItem != null)
                this.friendName = cbFriendname.SelectedValue.ToString();
        }

        private void cbFriendname_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = cbFriendname.Text.ToLower();

            if (e.Key == System.Windows.Input.Key.Back || e.Key == System.Windows.Input.Key.Delete)
            {
                // If Backspace or Delete is pressed, reset the filtering
                friendName = "";
                cbFriendname.ItemsSource = friends;
            }
            else
            {
                // Filter the items based on the entered text
                cbFriendname.ItemsSource = friends
                    .Where(item => item.ToLower().Contains(searchText))
                    .ToList();
            }

            cbFriendname.IsDropDownOpen = true;
        }

        private void cbIban_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbIban.SelectedItem != null)
                this.iban = cbIban.SelectedValue.ToString();
        }

        private void cbIban_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = cbIban.Text.ToLower();

            if (e.Key == System.Windows.Input.Key.Back || e.Key == System.Windows.Input.Key.Delete)
            {
                // If Backspace or Delete is pressed, reset the filtering
                iban = "";
                cbIban.ItemsSource = ibans;
            }
            else
            {
                // Filter the items based on the entered text
                cbIban.ItemsSource = ibans
                    .Where(item => item.Contains(searchText))
                    .ToList();
            }
            
            cbIban.IsDropDownOpen = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if(rb == radioBtnContact)
            {
                txtReceiver.Text = "Friend Name";
                cbFriendname.Visibility = Visibility.Visible;
                if (cbIban == null)
                txtIban.Text = "Friend Account";
                txtIban.Visibility = Visibility.Visible;
                cbIban.Visibility = Visibility.Visible;
                tbIban.Visibility = Visibility.Hidden;
            }
            else
            {
                txtReceiver.Text = "Account Number";
                cbFriendname.Visibility = Visibility.Hidden;
                txtIban.Visibility = Visibility.Hidden;
                cbIban.Visibility = Visibility.Hidden;
                tbIban.Visibility = Visibility.Visible;
            }

        }
    }
}

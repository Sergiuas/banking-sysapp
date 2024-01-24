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
        Card card;
        public string friendName;
        public string iban;
        public List<string> friends;
        public List<string> useribans;
        public List<string> friendibans;
        public sendMoneyPage(bsappDataContext db)
        {
            this.db = db;
            card = new Card();
            card.Balance = 0;
            DataContext = card;
            InitializeComponent();
            //friends = db.Contacts
            //    .Where(f => f.UserID == UserSingleton.Instance.UserID)
            //    .Join(db.Users,
            //          contact => contact.FriendID,
            //          user => user.UserID,
            //          (contact, user) => user.Username)
            //    .ToList();
            //ibans = db.Cards
            //    .Select(a => a.CardNumber)
            //    .ToList();

            this.useribans = db.Cards.Where(u =>  u.UserID == UserSingleton.Instance.UserID)
                            .Select(u => u.CardNumber)
                            .ToList();

            cbUserIban.ItemsSource = useribans;

            //this.useribans = (from cards in db.Cards
            //                  where cards.UserID == UserSingleton.Instance.UserID
            //                  select cards.CardNumber.ToString()).ToList();

            this.friends = (from contacts in db.Contacts
                      join users in db.Users on contacts.FriendID equals users.UserID
                      where contacts.UserID == UserSingleton.Instance.UserID && users.Type=="user"
                      select users.Username.ToString()).ToList();

            cbFriendname.ItemsSource = friends;
            
        }
        private void cbFriendname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFriendname.SelectedItem != null)
                this.friendName = cbFriendname.SelectedValue.ToString();

            this.friendibans = (from cards in db.Cards
                                        join users in db.Users on cards.UserID equals users.UserID
                                        where users.Username == this.friendName
                                        select cards.CardNumber
                                        ).ToList() ;

            cbIban.ItemsSource = friendibans;
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

            //List<string> useriban = (from cards in db.Cards
            //                  where cards.UserID == UserSingleton.Instance.UserID).ToList();
        }

        private void cbIban_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = cbIban.Text.ToLower();

            if (e.Key == System.Windows.Input.Key.Back || e.Key == System.Windows.Input.Key.Delete)
            {
                // If Backspace or Delete is pressed, reset the filtering
                iban = "";
                cbIban.ItemsSource = friendibans;
            }
            else
            {
                // Filter the items based on the entered text
                cbIban.ItemsSource = friendibans
                    .Where(item => item.Contains(searchText))
                    .ToList();
            }
            
            cbIban.IsDropDownOpen = true;
        }


        private void cbUserIban_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = cbIban.Text.ToLower();

            if (e.Key == System.Windows.Input.Key.Back || e.Key == System.Windows.Input.Key.Delete)
            {
                // If Backspace or Delete is pressed, reset the filtering
                iban = "";
                cbIban.ItemsSource = useribans;
            }
            else
            {
                // Filter the items based on the entered text
                cbIban.ItemsSource = friendibans
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

        private void btnSend_Click(object sender, RoutedEventArgs e) // De verificat sumele
        {
            float sum = float.Parse(txtSum.Text.ToString());
            card.Balance = card.Balance - (decimal)sum;

            Card destCard = db.Cards.Single(u => u.CardNumber == cbIban.SelectedItem.ToString());
            destCard.Balance = destCard.Balance + (decimal)sum;

            Transaction t = new Transaction();
            t.CardSourceID = card.CardID;
            t.CardDestID = destCard.CardID;
            t.Amount = (decimal)sum;
            t.Timestamp = DateTime.Now;

            db.Transactions.InsertOnSubmit(t);
            db.SubmitChanges();

            // De adaugat mesaj pentru tranzactie
            balanceLabel.Text = card.Balance.ToString();
        }

        private void cbUserIban_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            card = db.Cards.Single(u => u.CardNumber == cbUserIban.SelectedItem.ToString());
            balanceLabel.Text = card.Balance.ToString();
        }
    }
}

using bankingApp.classes;
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
            List<ShowCard> showCards = (from c in db.Cards
                                        where c.UserID == UserSingleton.Instance.UserID && c.Active==true
                                        select new ShowCard
                                        {
                                            cardnumber = c.CardNumber,
                                            username = UserSingleton.Instance.username,
                                            balance = (float)c.Balance,
                                            expirydate = c.ExpiryDate
                                        }).ToList();

            lbCarduri.ItemsSource= showCards;

            List<Friend> friends = (from c in db.Contacts
                                    join u in db.Users on c.FriendID equals u.UserID
                                    where c.UserID == UserSingleton.Instance.UserID
                                    select new Friend
                                    {
                                        username = u.Username,
                                        name = u.FirstName + " " + u.LastName,
                                        id = u.UserID,
                                        email = u.Email
                                    } ).ToList();
           
            lbFriends.ItemsSource= friends;


        }

        private void btnAddCard_Click(object sender, RoutedEventArgs e)
        {
            string foo = "";
            var wdw = new selectManagerWindow(db);
            wdw.ShowDialog();
            foo = wdw.foo;

            if (!string.IsNullOrEmpty(foo))
            {
                var manager = db.Users.SingleOrDefault(u => u.Username == foo);

                if (manager == null)
                {
                    return;
                }

                var card = new Card();
                card.Active = false;
                card.ManagerID = manager.UserID;
                card.UserID = UserSingleton.Instance.UserID;
                card.Balance = 0;
                DateTime curr = DateTime.Now;
                card.ExpiryDate = curr.AddYears(5);
                var random = new Random();
                string s = string.Empty;
                for (int i = 0; i < 16; i++)
                    s = String.Concat(s, random.Next(10).ToString());

                card.CardNumber = s;

                db.Cards.Add(card);
                db.SaveChanges();

            }
        }

        private void lbCarduri_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            // Find the ScrollViewer inside the ListBox
            ScrollViewer scrollViewer = FindVisualChild<ScrollViewer>(listBox);

            // Set the VerticalOffset to the maximum value
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToEnd();
            }
        }
        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);

                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }

            return null;
        }
        private void lbCarduri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbFriends_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            // Find the ScrollViewer inside the ListBox
            ScrollViewer scrollViewer = FindVisualChild<ScrollViewer>(listBox);

            // Set the VerticalOffset to the maximum value
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToEnd();
            }
        }

        private void lbFriends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbCarduri_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowCard card = (ShowCard)lbCarduri.SelectedItem;
            if (card != null)
            {
                var page = new showCardPage(db, card);
                NavigationService.Navigate(page);

            }
        }

        private void btnContacts_Click(object sender, RoutedEventArgs e)
        {
            var page = new contactsPage(db);
            NavigationService.Navigate(page);
        }
    }
}
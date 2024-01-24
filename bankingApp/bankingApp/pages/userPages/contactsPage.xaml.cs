using bankingApp.classes;
using bankingApp.pages.adminPages;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
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
        bsappEntities db;
        private List<Friend> friends;
        public contactsPage(bsappEntities db)
        {
            this.db = db;
            this.DataContext = new UserListDataContext();
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid(string searchedFriend = "")
        {
            if (string.IsNullOrEmpty(searchedFriend))
            {
                friends = db.Contacts.Where(c => c.UserID == UserSingleton.Instance.UserID )
                    .Join(db.Users, c => c.UserID, u => u.UserID, (c, u) => new 
                {
                    
                    id = c.FriendID,
                }).Join(db.Users, c => c.id, u => u.UserID, (c, u) => new Friend
                {
                    id = u.UserID,
                    name = u.FirstName + " " + u.LastName,
                    username = u.Username,
                    email = u.Email
                }).ToList();
            }
            else
            {
                friends = db.Contacts.Where(c => c.UserID == UserSingleton.Instance.UserID)
                    .Join(db.Users, c => c.UserID, u => u.UserID, (c, u) => new 
                {
                    id = c.FriendID,
                }).Join(db.Users, c => c.id, u => u.UserID, (c, u) => new Friend
                {
                    id = u.UserID,
                    name = u.FirstName + " " + u.LastName,
                    username = u.Username,
                    email = u.Email
                }).Where(f => f.username.Contains(searchedFriend) || f.name.Contains(searchedFriend) || f.email.Contains(searchedFriend)).ToList();
            }

            lblFriends.Text = friends.Count.ToString() + " Users";
            
            if (friends.Count > 10)
            {
                List<Friend> usersShown = friends.GetRange(0, 10);
                userTable.ItemsSource = usersShown;
                ((UserListDataContext)this.DataContext).NumberOfPages = friends.Count / 10;
                if (friends.Count % 10 != 0) ((UserListDataContext)this.DataContext).NumberOfPages++;
            }
            else
            {
                List<Friend> usersShown = friends.GetRange(0, friends.Count);
                userTable.ItemsSource = usersShown;
                ((UserListDataContext)this.DataContext).NumberOfPages = 1;
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (((UserListDataContext)this.DataContext).CurrentPage == ((UserListDataContext)this.DataContext).NumberOfPages) return;
            int start = ((UserListDataContext)this.DataContext).CurrentPage * 10;
            int count;
            if (start + 10 > friends.Count)
                count = friends.Count - start;
            else count = 10;
            List<Friend> usersShown = friends.GetRange(start, count);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).CurrentPage++;
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            int start = (((UserListDataContext)this.DataContext).NumberOfPages - 1) * 10;
            int count;
            if (start + 10 > friends.Count)
                count = friends.Count - start;
            else count = 10;
            List<Friend> usersShown = friends.GetRange(start, count);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).CurrentPage = ((UserListDataContext)this.DataContext).NumberOfPages;
        }

        private void btnPrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (((UserListDataContext)this.DataContext).CurrentPage == 1) return;
            ((UserListDataContext)this.DataContext).CurrentPage--;
            int start = (((UserListDataContext)this.DataContext).CurrentPage - 1) * 10;
            int count;
            if (start + 10 > friends.Count)
                count = friends.Count - start;
            else count = 10;
            List<Friend> usersShown = friends.GetRange(start, count);
            userTable.ItemsSource = usersShown;
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            int count;
            if (10 > friends.Count)
                count = friends.Count;
            else count = 10;
            List<Friend> usersShown = friends.GetRange(0, count);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).CurrentPage = 1;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRezolved_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtFriends_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtFriends.Text.Trim();
            InitializeDataGrid(searchText);
        }

        private void userTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int userID = UserSingleton.Instance.UserID;
            Friend friend = userTable.SelectedItems[0] as Friend;
            int friendID = friend.id;
            var Page = new sendMessagePage( userID, friendID, db);
            NavigationService.Navigate(Page);
            
        }
    }
}

using bankingApp.classes;
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

namespace bankingApp.pages.adminPages
{
    /// <summary>
    /// Interaction logic for ManagerListPage.xaml
    /// </summary>
    public partial class ManagerListPage : Page
    {
        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        bsappDataContext db;
        private List<ShowUser> users;
        public ManagerListPage(bool isDarkTheme, PaletteHelper _paletteHelper, bsappDataContext db)
        {
            this.isDarkTheme = isDarkTheme;
            this._paletteHelper = _paletteHelper;
            this.db = db;
            this.DataContext = new UserListDataContext();
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid(string searchedUsername = "")
        {
            if (string.IsNullOrEmpty(searchedUsername))
            {
                users = db.Users.Where(u => u.Type == "manager")
                    .Select(u => new ShowUser
                    {
                        Name = $"{u.FirstName} {u.LastName}",
                        Email = u.Email,
                        Username = u.Username,
                        Cards = 0,
                        LastLogin = u.LastLogin
                    })
                    .ToList();
            }
            else
            {
                users = db.Users.Where(u => u.Type == "manager" && u.Username.Contains(searchedUsername))
                        .Select(u => new ShowUser
                        {
                            Name = $"{u.FirstName} {u.LastName}",
                            Email = u.Email,
                            Username = u.Username,
                            Cards = 0,
                            LastLogin = u.LastLogin
                        })
                    .ToList();
            }
            lblManagers.Text = $"{users.Count} Managers";
            if (users.Count > 10) { 
            List<ShowUser> usersShown = users.GetRange(0, 10);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).NumberOfPages = users.Count / 10;
            if (users.Count % 10 != 0) ((UserListDataContext)this.DataContext).NumberOfPages++; }
            else
            {
                List<ShowUser> usersShown = users.GetRange(0, users.Count);
                userTable.ItemsSource = usersShown;
                ((UserListDataContext)this.DataContext).NumberOfPages = 1;
            }
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = _paletteHelper.GetTheme();

            if (isDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                isDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                isDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            _paletteHelper.SetTheme(theme);
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            if (((UserListDataContext)this.DataContext).CurrentPage == ((UserListDataContext)this.DataContext).NumberOfPages) return;
            int start = ((UserListDataContext)this.DataContext).CurrentPage * 10;
            int count;
            if (start + 10 > users.Count)
                count = users.Count - start;
            else count = 10;
            List<ShowUser> usersShown = users.GetRange(start, count);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).CurrentPage++;
        }

        private void btnPrevPage_Click_1(object sender, RoutedEventArgs e)
        {
            int start = (((UserListDataContext)this.DataContext).NumberOfPages - 1) * 10;
            int count;
            if (start + 10 > users.Count)
                count = users.Count - start;
            else count = 10;
            List<ShowUser> usersShown = users.GetRange(start, count);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).CurrentPage = ((UserListDataContext)this.DataContext).NumberOfPages;
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (((UserListDataContext)this.DataContext).CurrentPage == 1) return;
            ((UserListDataContext)this.DataContext).CurrentPage--;
            int start = (((UserListDataContext)this.DataContext).CurrentPage - 1) * 10;
            int count;
            if (start + 10 > users.Count)
                count = users.Count - start;
            else count = 10;
            List<ShowUser> usersShown = users.GetRange(start, count);
            userTable.ItemsSource = usersShown;
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            int count;
            if (10 > users.Count)
                count = users.Count;
            else count = 10;
            List<ShowUser> usersShown = users.GetRange(0, count);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).CurrentPage = 1;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (userTable.SelectedCells.Count == 0) return;

            ShowUser selectedUser = (ShowUser)userTable.SelectedItem;
            string selectedUsername = selectedUser.Username;

            editManagerPage Page = new editManagerPage(isDarkTheme, _paletteHelper, db, selectedUsername);
            MainContentFrame.Content = Page;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ShowUser selectedUser = (ShowUser)userTable.SelectedItem;
            string selectedUsername = selectedUser.Username;

            User user = db.Users.Single(u => u.Username == selectedUsername);
            db.Users.DeleteOnSubmit(user);
            db.SubmitChanges();

            string searchText = txtSearchUsername.Text.Trim();
            InitializeDataGrid(searchText);
        }

        private void txtSearchUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearchUsername.Text.Trim();
            InitializeDataGrid(searchText);
        }
    }
}

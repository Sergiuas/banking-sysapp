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

        private void InitializeDataGrid()
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
            lblManagers.Text = $"{users.Count} Managers";
            List<ShowUser> usersShown = users.GetRange(0, 5);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).NumberOfPages = users.Count / 5;
            if (users.Count % 5 != 0) ((UserListDataContext)this.DataContext).NumberOfPages++;
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

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (((UserListDataContext)this.DataContext).CurrentPage == ((UserListDataContext)this.DataContext).NumberOfPages) return;
            int start = ((UserListDataContext)this.DataContext).CurrentPage * 5;
            int count;
            if (start + 5 > users.Count)
                count = users.Count - start;
            else count = 5;
            List<ShowUser> usersShown = users.GetRange(start, count);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).CurrentPage++;
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            int start = (((UserListDataContext)this.DataContext).NumberOfPages - 1) * 5;
            int count;
            if (start + 5 > users.Count)
                count = users.Count - start;
            else count = 5;
            List<ShowUser> usersShown = users.GetRange(start, count);
            userTable.ItemsSource = usersShown;
            ((UserListDataContext)this.DataContext).CurrentPage = ((UserListDataContext)this.DataContext).NumberOfPages;
        }

        private void btnPrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (((UserListDataContext)this.DataContext).CurrentPage == 1) return;
            ((UserListDataContext)this.DataContext).CurrentPage--;
            int start = (((UserListDataContext)this.DataContext).CurrentPage - 1) * 5;
            int count;
            if (start + 5 > users.Count)
                count = users.Count - start;
            else count = 5;
            List<ShowUser> usersShown = users.GetRange(start, count);
            userTable.ItemsSource = usersShown;
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            int count;
            if (5 > users.Count)
                count = users.Count;
            else count = 5;
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

        }
    }
}

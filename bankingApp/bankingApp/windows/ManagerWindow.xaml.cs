using bankingApp.classes;
using bankingApp.pages;
using bankingApp.pages.adminPages;
using bankingApp.pages.mangerPages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bankingApp.windows
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        bsappEntities db;
        private UserSingleton userInstance = UserSingleton.Instance;
        public ManagerWindow(bool isDarkTheme, PaletteHelper _paletteHelper, bsappEntities db)
        {
            this.isDarkTheme = isDarkTheme;
            this._paletteHelper = _paletteHelper;
            this.db = db;
            DataContext = userInstance;
            InitializeComponent();
            Page dash = new managerDashboardPage(db);
            page.Content = dash;
        }

        private void WindowDragMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Visible;
            btnOpenMenu.Visibility = Visibility.Collapsed;
            //gridMenu.ColumnDefinitions[2].Width = new GridLength(300);
            //gridMenu.ColumnDefinitions[1].Width = new GridLength(660);
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(2000));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseOut };

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = 100;
            animation.To = 300;
            Storyboard.SetTarget(animation, gridMenu.ColumnDefinitions[2]);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(ColumnDefinition.MaxWidth)"));

            storyboard.Begin();
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnCloseMenu.Visibility = Visibility.Collapsed;
            btnOpenMenu.Visibility = Visibility.Visible;
            //gridMenu.ColumnDefinitions[2].Width = new GridLength(100);
            //gridMenu.ColumnDefinitions[1].Width = new GridLength(660);
            Storyboard storyboard = new Storyboard();

            Duration duration = new Duration(TimeSpan.FromMilliseconds(2000));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseOut };

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            storyboard.Children.Add(animation);
            animation.From = 300;
            animation.To = 100;
            Storyboard.SetTarget(animation, gridMenu.ColumnDefinitions[2]);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(ColumnDefinition.MaxWidth)"));

            storyboard.Begin();

        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Log out user
            MainWindow main = new MainWindow();
            main.Opacity = 0;
            main.Show();
            this.Close();
            main.Opacity = 1;
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
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage Page = new SettingsPage(isDarkTheme, _paletteHelper, db);
            page.Content = Page;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            transactionsPage Page = new transactionsPage(isDarkTheme, _paletteHelper, db);
            page.Content = Page;
        }

        private void btnBankAccounts_Click(object sender, RoutedEventArgs e)
        {
            bankAccountsPage Page = new bankAccountsPage(isDarkTheme, _paletteHelper, db);
            page.Content = Page;
        }

        private void btnWaitinglist_Click(object sender, RoutedEventArgs e)
        {
            waitlistPage Page = new waitlistPage(isDarkTheme, _paletteHelper, db);
            page.Content = Page;
        }

        private void btnTickets_Click(object sender, RoutedEventArgs e)
        {
            ticketsPage Page = new ticketsPage(db);
            page.Content = Page;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            editUserPage Page = new editUserPage(isDarkTheme, _paletteHelper, db, userInstance.username);
            page.Content = Page;
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Dashboard
        {
            managerDashboardPage Page = new managerDashboardPage(db);
            page.Content = Page;
        }
    }
}

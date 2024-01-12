using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.IO;
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
using MaterialDesignThemes.Wpf;
using System.Windows.Media.Animation;
using bankingApp.classes;
using bankingApp.pages.userPages;
using bankingApp.pages;

namespace bankingApp
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        private UserSingleton userInstance = UserSingleton.Instance;
        private bsappDataContext db = new bsappDataContext();

        public UserWindow(bool isDarkTheme, PaletteHelper _paletteHelper, bsappDataContext db)
        {
            this.isDarkTheme = isDarkTheme;
            this._paletteHelper = _paletteHelper;
            this.db = db;
            DataContext = userInstance;

            InitializeComponent();
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            var Page = new userDashboardPage();
            page.Content = Page;
        }

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            var Page = new transactionsPage(db);
            page.Content = Page;
        }

        private void btnSendMoney_Click(object sender, RoutedEventArgs e)
        {
            var Page = new sendMoneyPage(db);
            page.Content = Page;
        }

        private void btnContacts_Click(object sender, RoutedEventArgs e)
        {
            var Page = new contactsPage(db);
            page.Content = Page;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var Page = new SettingsPage(isDarkTheme, _paletteHelper, db);
            page.Content = Page;
        }
    }
}
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
            
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPrevPage_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

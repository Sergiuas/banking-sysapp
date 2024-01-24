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

namespace bankingApp.pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        bsappEntities db;
        private UserSingleton userInstance = UserSingleton.Instance;
        public SettingsPage(bool isDarkTheme, PaletteHelper _paletteHelper, bsappEntities db)
        {
            this.isDarkTheme = isDarkTheme;
            this._paletteHelper = _paletteHelper;
            this.db = db;
            DataContext = userInstance;
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

        private void Button_Click(object sender, RoutedEventArgs e) //username
        {
            var user = db.Users.SingleOrDefault(u => u.Username == txtUsername.Text.ToString());
            if (user != null)
            {
                return;
            }
                db.SaveChanges(); //db.submitchanges
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //email
        {
            var user = db.Users.SingleOrDefault(u => u.Email == txtEmail.Text.ToString());
            if (user != null)
            {
                return;
            }
            db.SaveChanges();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // pass
        {
            if (txtPasword.Text != txtConfirmPasword.Text && txtPasword.Text.Length>0)
            {
                return;
            }
            db.SaveChanges();
        }
    }
}

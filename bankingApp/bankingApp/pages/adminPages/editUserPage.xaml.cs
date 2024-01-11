using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class editUserPage : Page
    {
        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        bsappDataContext db;
        User user;
        public editUserPage(bool isDarkTheme, PaletteHelper _paletteHelper, bsappDataContext db, string username)
        {
            this.isDarkTheme = isDarkTheme;
            this._paletteHelper = _paletteHelper;
            this.db = db;
            this.user = this.db.Users.Single(u => u.Username == username);
            DataContext = this.user;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string password = txtPasword.Text;
            string confirmPassword = txtConfirmPasword.Text;

            if (password.Length > 0 && password!=confirmPassword) {
            
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            password = encoded;
            this.user.Password = password;
            }

            db.SubmitChanges();

            UserListPage Page = new UserListPage(isDarkTheme, _paletteHelper, db);
            MainContentFrame.Content = Page;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            UserListPage Page = new UserListPage(isDarkTheme, _paletteHelper, db);
            MainContentFrame.Content = Page;
        }
    }
}

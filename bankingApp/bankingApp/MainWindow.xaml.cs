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
using System.Windows.Media.Animation;
using System.Data.Linq;
using System.Security.Cryptography;

namespace bankingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bsappDataContext db;
        public MainWindow()
        {
            InitializeComponent();
            this.db = new bsappDataContext();

        }

        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
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

        private void exitApp(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Password;


            bool isAuthenticated = CheckLogin(username, password);

            if (isAuthenticated)
            {
               // MessageBox.Show("Login successful!");
                UserWindow userWindow = new UserWindow(isDarkTheme, _paletteHelper);
                userWindow.Show();
                this.Close();
            }
            else
            {
                incorrectDataLabel.Visibility = Visibility.Visible;
                txtPassword.Clear();
                txtUsername.Clear();
            }
        }

        private void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow(isDarkTheme, _paletteHelper);
            signUpWindow.Show();
            this.Close();
        }

        private bool CheckLogin(string username, string password)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            var user = db.Users.SingleOrDefault(u => (u.Username == username && u.PasswordHash == encoded) || (u.Email == username && u.PasswordHash == encoded));

            if(user == null)
            return false;
            
            return true;
        }
    }
}

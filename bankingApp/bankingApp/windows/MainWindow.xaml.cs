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
using bankingApp.windows;
using bankingApp.classes;

namespace bankingApp
{
    public partial class MainWindow : Window
    {
        private bsappEntities db;
        public MainWindow()
        {
            InitializeComponent();
            this.db = new bsappEntities();

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


            User user = CheckLogin(username, password);

            UserSingleton userInstance = UserSingleton.Instance;
            if (user == null)
            {
                incorrectDataLabel.Visibility = Visibility.Visible;
                txtPassword.Clear();
                txtUsername.Clear();
                return;
            }

            switch (user.Type)
            {
               
                case "admin":
                    userInstance.build(user.UserID, user.Username, user.Password, user.Email, user.FirstName, user.LastName, UserTypes.ADMIN);
                    AdminWindow adminWindow = new AdminWindow(isDarkTheme, _paletteHelper, db);
                    adminWindow.Show();
                    this.Close();
                    break;
                case "manager":
                    userInstance.build(user.UserID, user.Username, user.Password, user.Email, user.FirstName, user.LastName, UserTypes.MANAGER);
                    ManagerWindow managerwindow = new ManagerWindow(isDarkTheme, _paletteHelper,db);
                    managerwindow.Show();
                    this.Close();
                    break;
                case "user":
                    userInstance.build(user.UserID, user.Username, user.Password, user.Email, user.FirstName, user.LastName, UserTypes.USER);
                    UserWindow userWindow = new UserWindow(isDarkTheme, _paletteHelper,db);
                    userWindow.Show();
                    this.Close();
                    break;
                default:
                    incorrectDataLabel.Visibility = Visibility.Visible;
                    txtPassword.Clear();
                    txtUsername.Clear();
                    break;
            }
        }

        private void signupBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow(isDarkTheme, _paletteHelper);
            signUpWindow.Show();
            this.Close();
        }

        private User CheckLogin(string username, string password)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            var user = db.Users.SingleOrDefault(u => (u.Username == username && u.Password == encoded) || (u.Email == username && u.Password == encoded));

            return user;

        }
    }
}

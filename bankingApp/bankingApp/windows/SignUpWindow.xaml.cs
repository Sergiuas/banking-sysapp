using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using System.Windows.Shapes;

namespace bankingApp
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        private enum SignUpError
        {
            PASSWORD_MATCH,
            PASSWORD_FORMAT,
            INVALID_MAIL,
            MAIL_USED,
            INVALID_USERNAME,
            USERNAME_USED,
            OK
        };

        public SignUpWindow(bool isDarkTheme, PaletteHelper _paletteHelper)
        {
            this.isDarkTheme = isDarkTheme;
            this._paletteHelper = _paletteHelper;
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

        private void exitApp(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Password.ToString()) || string.IsNullOrEmpty(txtConfirmPassword.Password.ToString())) {

                MessageBox.Show("Complete all the textboxes first.");
                return;
            }
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password.ToString();
            string confirmPassword = txtConfirmPassword.Password.ToString();
            string firstName = txtFirstName.ToString();
            string lastName = txtLastName.ToString();

            switch(checkSignUp(username, email, firstName, lastName, password, confirmPassword))
            {
                case SignUpError.PASSWORD_MATCH:
                    incorrectDataLabel.Content = "Passwords do not match.";
                    incorrectDataLabel.Visibility = Visibility.Visible;
                    return;
                case SignUpError.PASSWORD_FORMAT:
                    incorrectDataLabel.Content = "Invalid password format."; // TODO: use a message more useful
                    incorrectDataLabel.Visibility = Visibility.Visible;
                    return;
                case SignUpError.INVALID_USERNAME:
                    incorrectDataLabel.Content = "Invalid username";
                    incorrectDataLabel.Visibility = Visibility.Visible;
                    return;
                case SignUpError.INVALID_MAIL:
                    incorrectDataLabel.Content = "Invalid email";
                    incorrectDataLabel.Visibility = Visibility.Visible;
                    return;
                case SignUpError.USERNAME_USED:
                    incorrectDataLabel.Content = "Username already used.";
                    incorrectDataLabel.Visibility = Visibility.Visible;
                    return;
                case SignUpError.MAIL_USED:
                    incorrectDataLabel.Content = "Email already used.";
                    incorrectDataLabel.Visibility = Visibility.Visible;
                    return;
                default:
                    break;
            }

            bsappDataContext db = new bsappDataContext();
            User newUser = new User();
            newUser.Username = username;
            newUser.Email = email;
            newUser.Type = "Client";
            newUser.FirstName = firstName;
            newUser.LastName = lastName;

            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            newUser.Password = encoded;
            db.Users.InsertOnSubmit(newUser);
            db.SubmitChanges();

            MessageBox.Show("Signup successful!");
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();

        }
        private SignUpError checkSignUp(string username, string email, string firstName, string lastName, string password, string confirmPassword)
        {
            bsappDataContext db = new bsappDataContext();

            if (password != confirmPassword)
                return SignUpError.PASSWORD_MATCH;

            if (!MailIsValid(email))
                return SignUpError.INVALID_MAIL;

            var user = db.Users.SingleOrDefault(u => u.Email == email);
            if (user != null)
                return SignUpError.MAIL_USED;

            user = db.Users.SingleOrDefault(u => u.Username == username);
            if (user != null)
                return SignUpError.USERNAME_USED;

            //if (!password.Any(char.IsDigit) || !password.Any(char.IsSymbol))
            //    return SignUpError.PASSWORD_FORMAT;

            return SignUpError.OK;
        }
        private void btnBackLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

         private bool MailIsValid(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }   
}

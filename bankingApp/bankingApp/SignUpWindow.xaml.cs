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


            // TODO: write red text above every text box is not filled;
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Password.ToString()) || string.IsNullOrEmpty(txtConfirmPassword.Password.ToString())) {

                MessageBox.Show("Complete all the textboxes first.");
                return;
            }
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password.ToString();
            string confirmPassword = txtConfirmPassword.Password.ToString();

           
            if (password!=confirmPassword)
            {
                MessageBox.Show("Passwords dont match");
                return;
            }

            bsappDataContext db = new bsappDataContext();


            // TODO: verificare username imediat ce se completeaza textboxul
            var user = db.users.SingleOrDefault(u => u.username == username || u.email == email);
            if (user != null)
            {
                MessageBox.Show("Username/Mail deja folosit");
                return;
            }

                // TODO: verificare daca userul exista / daca datele sunt bune 
                user newUser = new user();
            newUser.username = username;
            newUser.email = email;
            newUser.password = password;
            db.users.InsertOnSubmit(newUser);
            db.SubmitChanges();

            MessageBox.Show("Signup successful!");
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();

        }
    }
}

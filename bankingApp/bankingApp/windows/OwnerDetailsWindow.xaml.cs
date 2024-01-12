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

namespace bankingApp.windows
{

    public partial class OwnerDetailsWindow : Window
    {
        bsappDataContext db;
        public OwnerDetailsWindow(User user)
        {
            
            InitializeComponent();
            InitializeContent(user);
        }

        void InitializeContent(User user)
        {
            lblUsername.Content ="Username: " + user.Username;
            lblFirstName.Content ="First Name: " + user.FirstName;
            lblLastName.Content ="Last Name: " + user.LastName;
            lblEmail.Content ="Email: " + user.Email;
            lblPhoneNumber.Content ="Phone Number: " + user.PhoneNumber;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

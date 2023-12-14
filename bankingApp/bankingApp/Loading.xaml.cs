using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace bankingApp
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {

       
        private DispatcherTimer dispatcherTimer;

        public Loading()
        {
            InitializeComponent();

            // Initialize the dispatcher timer
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OpenLoginPage);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        private void OpenLoginPage(object sender, EventArgs e)
        {
            // Stop the timer
            dispatcherTimer.Stop();

            var loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();

            
        }
    }
}

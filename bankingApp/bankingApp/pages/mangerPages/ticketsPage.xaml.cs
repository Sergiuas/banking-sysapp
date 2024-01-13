using bankingApp.classes;
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

namespace bankingApp.pages.mangerPages
{
    /// <summary>
    /// Interaction logic for ticketsPage.xaml
    /// </summary>
    public partial class ticketsPage : Page
    {
        bsappDataContext db;
        TicketBody selectedTicket;
        public ticketsPage(bsappDataContext db)
        {
            
            this.db = db;
            InitializeComponent();
            List<TicketBody> tickets = new List<TicketBody> { 
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody(),
                new TicketBody()
            };
            lbTickets.ItemsSource = tickets;
        }
        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            // Find the ScrollViewer inside the ListBox
            ScrollViewer scrollViewer = FindVisualChild<ScrollViewer>(listBox);

            // Set the VerticalOffset to the maximum value
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToEnd();
            }
        }

        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);

                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }

            return null;
        }

        private void lbTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTicket = (TicketBody)lbTickets.SelectedItem;
            DataContext = selectedTicket;
        }
    }
}

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
            List<TicketBody> tickets = (from t in db.Tickets
                                       join u in db.Users on t.UserID equals u.UserID
                                       where t.ManagerID == UserSingleton.Instance.UserID
                                       select new TicketBody
                                       {
                                           username = u.Username,
                                           subject = t.Subject,
                                           body = t.Body,
                                           id = t.TicketID,
                                           status = (bool)t.Resolved,
                                           date =(DateTime)t.Timestamp
                                       }
                                       ).ToList();

            lbTickets.ItemsSource = tickets;
        }

        private void Initiliaze_Tickets(string searched="")
        {
            List<TicketBody> tickets;
            if (!string.IsNullOrEmpty(searched))
            {

                 tickets = (from t in db.Tickets
                                            join u in db.Users on t.UserID equals u.UserID
                                            where t.ManagerID == UserSingleton.Instance.UserID && (t.Subject.Contains(searched) || u.Username.Contains(searched))
                                            select new TicketBody
                                            {
                                                username = u.Username,
                                                subject = t.Subject,
                                                body = t.Body,
                                                id = t.TicketID,
                                                status = (bool)t.Resolved,
                                                date = (DateTime)t.Timestamp
                                            }
                                            ).ToList();

                lbTickets.ItemsSource = tickets;

                return;
            }

            tickets = (from t in db.Tickets
                                        join u in db.Users on t.UserID equals u.UserID
                                        where t.ManagerID == UserSingleton.Instance.UserID
                                        select new TicketBody
                                        {
                                            username = u.Username,
                                            subject = t.Subject,
                                            body = t.Body,
                                            id = t.TicketID,
                                            status = (bool)t.Resolved,
                                            date = (DateTime)t.Timestamp
                                        }
                           ).ToList();

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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            selectedTicket = (TicketBody)lbTickets.SelectedItem;
            var ticket = db.Tickets.SingleOrDefault(u => u.TicketID == selectedTicket.id);
            if (ticket != null)
            {
                return;
            }

            db.Tickets.DeleteOnSubmit(ticket);

            db.SubmitChanges();
            Initiliaze_Tickets();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            selectedTicket = (TicketBody)lbTickets.SelectedItem;
            var ticket = db.Tickets.SingleOrDefault(u => u.TicketID == selectedTicket.id);
            if (ticket != null)
            {
                return;
            }
            ticket.Resolved = true;

            db.SubmitChanges();
            Initiliaze_Tickets();
        }

        private void txtTransactions_TextChanged(object sender, TextChangedEventArgs e)
        {
            Initiliaze_Tickets(txtTransactions.Text.Trim());
        }
    }
}

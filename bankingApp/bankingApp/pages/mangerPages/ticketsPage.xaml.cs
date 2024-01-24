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
using System.Net;
using System.Net.Mail;

namespace bankingApp.pages.mangerPages
{
    /// <summary>
    /// Interaction logic for ticketsPage.xaml
    /// </summary>
    public partial class ticketsPage : Page
    {
        bsappEntities db;
        TicketBody selectedTicket;
        public ticketsPage(bsappEntities db)
        {
            
            this.db = db;
            InitializeComponent();
            Initiliaze_Tickets();
        }

        private void Initiliaze_Tickets(string searched="")
        {
            List<TicketBody> tickets;
            if (!string.IsNullOrEmpty(searched))
            {

                 tickets = (from t in db.Tickets
                                            join u in db.Users on t.UserID equals u.UserID
                                            where t.ManagerID == UserSingleton.Instance.UserID && (t.Subject.Contains(searched) || u.Username.Contains(searched) && t.Resolved==false)
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
                                        where t.ManagerID == UserSingleton.Instance.UserID && t.Resolved == false
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
            if (ticket == null)
            {
                return;
            }

            //db.Tickets.DeleteOnSubmit(ticket);
            //db.SubmitChanges();

            db.Tickets.Remove(ticket);
            db.SaveChanges();
            Initiliaze_Tickets();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            selectedTicket = (TicketBody)lbTickets.SelectedItem;
            var ticket = db.Tickets.SingleOrDefault(u => u.TicketID == selectedTicket.id);
            if (ticket == null)
            {
                return;
            }
            ticket.Resolved = true;

            db.SaveChanges();
            Initiliaze_Tickets();

            var user = db.Users.SingleOrDefault(u => u.UserID == ticket.UserID);

            string fromMail = "hamsik569@gmail.com";
            string fromPassword = "vjcoxfvzihgzlfpu";

            MailMessage message = new MailMessage();
            message.From= new MailAddress(fromMail);
            message.Subject = "[BankingSystems]Ticket " + ticket.TicketID.ToString() + " solved";
            message.To.Add(new MailAddress(user.Email.ToString())); 
            message.Body = "<html><body> Ticket-ul a fost solutionat. </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        private void txtTransactions_TextChanged(object sender, TextChangedEventArgs e)
        {
            Initiliaze_Tickets(txtTransactions.Text.Trim());
        }
    }
}

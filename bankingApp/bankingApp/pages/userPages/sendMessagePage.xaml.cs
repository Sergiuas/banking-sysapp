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
using bankingApp.classes;

namespace bankingApp.pages.userPages
{
    /// <summary>
    /// Interaction logic for sendMessagePage.xaml
    /// </summary>
    public partial class sendMessagePage : Page
    {
        bsappDataContext db;
        public int userID { get; set; }
        public int friendID { get; set; }
        public sendMessagePage(int userID,int friendID,bsappDataContext db)
        {
            this.db = db;
            this.userID = userID;
            this.friendID = friendID;
            InitializeComponent();
            List<MessageBody> list = db.MessagesViews.Where(x => (x.SenderID == userID && x.RecipientID == friendID) || (x.RecipientID == userID && x.SenderID == friendID))
                .OrderBy(x => x.Timestamp).Select(x => new MessageBody
            {
                message = x.Body,
                sender = x.SenderID == UserSingleton.Instance.UserID ? "You" : x.SenderUsername,
                date = (DateTime)x.Timestamp,
                isFriend = x.RecipientID == UserSingleton.Instance.UserID ? Visibility.Visible : Visibility.Hidden,
                isUser = x.RecipientID == UserSingleton.Instance.UserID ? Visibility.Hidden : Visibility.Visible
            }).ToList();
            lbMessage.ItemsSource = list;

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

    }
}

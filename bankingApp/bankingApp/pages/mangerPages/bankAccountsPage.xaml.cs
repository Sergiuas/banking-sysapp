using bankingApp.classes;
using bankingApp.windows;
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

namespace bankingApp.pages.mangerPages
{
    /// <summary>
    /// Interaction logic for bankAccountsPage.xaml
    /// </summary>
    public partial class bankAccountsPage : Page
    {
        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        bsappEntities db;
        private List<ShowCard> cards;
        public bankAccountsPage(bool isDarkTheme, PaletteHelper _paletteHelper, bsappEntities db)
        {
            this.isDarkTheme = isDarkTheme;
            this._paletteHelper = _paletteHelper;
            this.db = db;
            this.DataContext = new CardListDataContext();
            InitializeComponent();
            InitializeDataGrid();
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

        void InitializeDataGrid(string searched = "")
        {
            if (string.IsNullOrEmpty(searched))
            {
                cards = (from cards in db.Cards
                         join users in db.Users on cards.UserID equals users.UserID
                         where cards.Active == true
                         select new ShowCard
                         {
                             username = users.Username,
                             cardnumber = cards.CardNumber,
                             balance = (float)cards.Balance,
                             expirydate = cards.ExpiryDate
                         }).ToList();
            }
            else
            {
                cards = (from cards in db.Cards
                         join users in db.Users on cards.UserID equals users.UserID
                         where (cards.CardNumber.Contains(searched) || users.Username.Contains(searched)) && cards.Active == true
                         select new ShowCard
                         {
                             username = users.Username,
                             cardnumber = cards.CardNumber,
                             balance = (float)cards.Balance,
                             expirydate = cards.ExpiryDate
                         }).ToList();
            }

            lblWaitlist.Text = cards.Count.ToString() + " Bank accounts";

            if (cards.Count > 10)
            {
                List<ShowCard> cardsShown = cards.GetRange(0, 10);
                cardsTable.ItemsSource = cardsShown;
                ((CardListDataContext)this.DataContext).NumberOfPages = cards.Count / 10;
                if (cards.Count % 10 != 0) ((CardListDataContext)this.DataContext).NumberOfPages++;
            }
            else
            {
                List<ShowCard> cardsShown = cards.GetRange(0, cards.Count);
                cardsTable.ItemsSource = cardsShown;
                ((CardListDataContext)this.DataContext).NumberOfPages = 1;
            }
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            int count;
            if (10 > cards.Count)
                count = cards.Count;
            else count = 10;
            List<ShowCard> cardsShown = cards.GetRange(0, count);
            cardsTable.ItemsSource = cardsShown;
            ((CardListDataContext)this.DataContext).CurrentPage = 1;
        }

        private void btnPrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (((CardListDataContext)this.DataContext).CurrentPage == 1) return;
            ((CardListDataContext)this.DataContext).CurrentPage--;
            int start = (((CardListDataContext)this.DataContext).CurrentPage - 1) * 10;
            int count;
            if (start + 10 > cards.Count)
                count = cards.Count - start;
            else count = 10;
            List<ShowCard> cardsShown = cards.GetRange(start, count);
            cardsTable.ItemsSource = cardsShown;
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (((CardListDataContext)this.DataContext).CurrentPage == ((CardListDataContext)this.DataContext).NumberOfPages) return;
            int start = ((CardListDataContext)this.DataContext).CurrentPage * 10;
            int count;
            if (start + 10 > cards.Count)
                count = cards.Count - start;
            else count = 10;
            List<ShowCard> cardsShown = cards.GetRange(start, count);
            cardsTable.ItemsSource = cardsShown;
            ((CardListDataContext)this.DataContext).CurrentPage++;
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            int start = (((CardListDataContext)this.DataContext).NumberOfPages - 1) * 10;
            int count;
            if (start + 10 > cards.Count)
                count = cards.Count - start;
            else count = 10;
            List<ShowCard> cardsShown = cards.GetRange(start, count);
            cardsTable.ItemsSource = cardsShown;
            ((CardListDataContext)this.DataContext).CurrentPage = ((CardListDataContext)this.DataContext).NumberOfPages;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ShowCard selectedCard = (ShowCard)cardsTable.SelectedItem;
            string selectedNumber = selectedCard.cardnumber;

            Card card = db.Cards.Single(u => u.CardNumber == selectedNumber);
            //db.Cards.DeleteOnSubmit(card);
            //db.SubmitChanges();
            db.Cards.Remove(card);
            db.SaveChanges();

            string searchText = txtCardsSearch.Text.Trim();
            InitializeDataGrid(searchText);
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ShowCard selectedCard = (ShowCard)cardsTable.SelectedItem;
            string username = selectedCard.username;

            User user = db.Users.Single(u => u.Username == username);
            if (user == null) return;

            var wdw = new OwnerDetailsWindow(user);
            wdw.ShowDialog();
        }

        private void txtCardsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtCardsSearch.Text.Trim();
            InitializeDataGrid(searchText);
        }
    }
}

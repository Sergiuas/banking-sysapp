using bankingApp.classes;
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
    /// Interaction logic for transactionsPage.xaml
    /// </summary>
    public partial class transactionsPage : Page
    {
        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        bsappEntities db;
        private List<ShowTransaction> transactions;
        public transactionsPage(bool isDarkTheme, PaletteHelper _paletteHelper, bsappEntities db)
        {
            this.isDarkTheme = isDarkTheme;
            this._paletteHelper = _paletteHelper;
            this.db = db;
            this.DataContext = new TransactionListDataContext();
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid(string searched = "")
        {
            if (string.IsNullOrEmpty(searched))
            {
                transactions = (from transactions in db.Transactions
                                join cards1 in db.Cards on transactions.CardSourceID equals cards1.CardID
                                join cards2 in db.Cards on transactions.CardDestID equals cards2.CardID
                                join users1 in db.Users on cards1.UserID equals users1.UserID
                                join users2 in db.Users on cards2.UserID equals users2.UserID
                                select new ShowTransaction
                                {
                                    SrcUsername = users1.Username,
                                    DestUsername = users2.Username,
                                    Amount = (float)transactions.Amount,
                                    Timestamp = transactions.Timestamp,
                                }).ToList();
            }
            else
            {
                    transactions = (from transactions in db.Transactions
                                join cards1 in db.Cards on transactions.CardSourceID equals cards1.CardID
                                join cards2 in db.Cards on transactions.CardDestID equals cards2.CardID
                                join users1 in db.Users on cards1.UserID equals users1.UserID
                                join users2 in db.Users on cards2.UserID equals users2.UserID
                                where users1.Username.Contains(searched) || users2.Username.Contains(searched)
                                select new ShowTransaction
                                {
                                    SrcUsername = users1.Username,
                                    DestUsername = users2.Username,
                                    Amount = (float)transactions.Amount,
                                    Timestamp = transactions.Timestamp,
                                }
                                ).ToList();
            }

            lblTransactions.Text = $"{transactions.Count} Transactions";

            if (transactions.Count > 10)
            {
                List<ShowTransaction> transactionsShown = transactions.GetRange(0, 10);
                transactionsTable.ItemsSource = transactionsShown;
                ((TransactionListDataContext)this.DataContext).NumberOfPages = transactions.Count / 10;
                if (transactions.Count % 10 != 0) ((TransactionListDataContext)this.DataContext).NumberOfPages++;
            }
            else
            {
                List<ShowTransaction> transactionsShown = transactions.GetRange(0, transactions.Count);
                transactionsTable.ItemsSource = transactionsShown;
                ((TransactionListDataContext)this.DataContext).NumberOfPages = 1;
            }
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

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            int count;
            if (10 > transactions.Count)
                count = transactions.Count;
            else count = 10;
            List<ShowTransaction> transactionsShown = transactions.GetRange(0, count);
            transactionsTable.ItemsSource = transactionsShown;
            ((TransactionListDataContext)this.DataContext).CurrentPage = 1;
        }

        private void btnPrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (((TransactionListDataContext)this.DataContext).CurrentPage == 1) return;
            ((TransactionListDataContext)this.DataContext).CurrentPage--;
            int start = (((TransactionListDataContext)this.DataContext).CurrentPage - 1) * 10;
            int count;
            if (start + 10 > transactions.Count)
                count = transactions.Count - start;
            else count = 10;
            List<ShowTransaction> transactionsShown = transactions.GetRange(start, count);
            transactionsTable.ItemsSource = transactionsShown;
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (((TransactionListDataContext)this.DataContext).CurrentPage == ((TransactionListDataContext)this.DataContext).NumberOfPages) return;
            int start = ((TransactionListDataContext)this.DataContext).CurrentPage * 10;
            int count;
            if (start + 10 > transactions.Count)
                count = transactions.Count - start;
            else count = 10;
            List<ShowTransaction> transactionsShown = transactions.GetRange(start, count);
            transactionsTable.ItemsSource = transactionsShown;
            ((TransactionListDataContext)this.DataContext).CurrentPage++;
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            int start = (((TransactionListDataContext)this.DataContext).NumberOfPages - 1) * 10;
            int count;
            if (start + 10 > transactions.Count)
                count = transactions.Count - start;
            else count = 10;
            List<ShowTransaction> transactionsShown = transactions.GetRange(start, count);
            transactionsTable.ItemsSource = transactionsShown;
            ((TransactionListDataContext)this.DataContext).CurrentPage = ((TransactionListDataContext)this.DataContext).NumberOfPages;
        }

        private void txtTransactionsSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtTransactionsSearch.Text.Trim();
            InitializeDataGrid(searchText);
        }

        private void rdoSource_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void rdoSource_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void rdoDestination_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void rdoDestination_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rdoDate_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rdoDate_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}

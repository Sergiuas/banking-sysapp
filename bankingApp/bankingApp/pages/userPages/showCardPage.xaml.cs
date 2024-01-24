using bankingApp.classes;
using LiveCharts;
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

namespace bankingApp.pages.userPages
{
    /// <summary>
    /// Interaction logic for showCardPage.xaml
    /// </summary>
    public partial class showCardPage : Page
    {
        bsappEntities db;
        public showCardPage(bsappEntities db, ShowCard card)
        {
            this.db = db;
            Card cardData = (from c in db.Cards
                             where c.CardNumber == card.cardnumber
                             select c).FirstOrDefault();
            InitializeComponent();
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
            tbFirstName.Text = UserSingleton.Instance.firstName;
            tbLastName.Text = UserSingleton.Instance.lastName;
            //tbBirthDate.Text = UserSingleton.Instance.birthdate;
            tbCardNumber.Text = card.cardnumber;
            tbAmmount.Text = card.balance.ToString();
            tbExpiryDate.Text = card.expirydate.ToString();
            tbCardStatus.Text = cardData.Active.ToString();
        }

        public Func<ChartPoint, string> PointLabel { get; set; }
    }
}

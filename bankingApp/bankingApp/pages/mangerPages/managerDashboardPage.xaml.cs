using LiveCharts.Wpf;
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
using System.Runtime.InteropServices;
using bankingApp.classes;

namespace bankingApp.pages.mangerPages
{
    /// <summary>
    /// Interaction logic for managerDashboardPage.xaml
    /// </summary>
    public partial class managerDashboardPage : Page
    {
        bsappEntities db;
        public int solvedCount;
        public int unsolvedCount;
        public int activeCount;
        public int pendingCount;
        public managerDashboardPage(bsappEntities db)
        {
            this.db = db;
            InitializeComponent();
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);


           this.solvedCount = (from t in db.Tickets
                               where t.ManagerID == UserSingleton.Instance.UserID && t.Resolved==true
                               select t).Count();

            this.unsolvedCount = (from t in db.Tickets
                                 where t.ManagerID == UserSingleton.Instance.UserID && t.Resolved == false
                                 select t).Count();

            this.pendingCount = (from c in db.Cards
                                where c.ManagerID==UserSingleton.Instance.UserID && c.Active==false
                                select c).Count();

            this.activeCount = (from c in db.Cards
                               where c.ManagerID == UserSingleton.Instance.UserID && c.Active == true
                               select c).Count();

            
            DataContext = this;

            pieActiveCards.Values = new LiveCharts.ChartValues<int> { this.activeCount };
            piePendingCards.Values = new LiveCharts.ChartValues<int> { this.pendingCount };
            pieTicketsSolved.Values = new LiveCharts.ChartValues<int> { this.solvedCount };
            pieTicketsUnsolved.Values = new LiveCharts.ChartValues<int> { this.unsolvedCount };

        }

        public Func<ChartPoint, string> PointLabel { get; set; }
        
    }
}

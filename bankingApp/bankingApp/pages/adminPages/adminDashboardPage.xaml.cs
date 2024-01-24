using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
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
using MaterialDesignThemes.Wpf;

namespace bankingApp.pages.adminPages
{
    /// <summary>
    /// Interaction logic for adminDashboardPage.xaml
    /// </summary>
    public partial class adminDashboardPage : Page
    {
        private bsappEntities db = new bsappEntities();
        public adminDashboardPage(bsappEntities db)
        {
            this.db = db;
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Cards",
                    Values = new ChartValues<double>()
                }
            };
            

            Labels = new[] { "a", "a", "a", "a" ,"a"};

            var topManagers = db.Cards
                .GroupBy(card => card.ManagerID)
                .Join(db.Users,
                      cardGroup => cardGroup.Key,
                      user => user.UserID,
                      (cardGroup, user) => new
                      {
                          ManagerID = cardGroup.Key,
                          Username = user.Username,
                          TotalCardsManaged = cardGroup.Count()
                      })
                .OrderByDescending(result => result.TotalCardsManaged)
                .Take(5).ToList();

            for (int i = 0; i < 5; i++)
            {
                if (i < topManagers.Count)
                {
                    SeriesCollection[0].Values.Add((double)topManagers[i].TotalCardsManaged);
                    Labels[i] = topManagers[i].Username;
                }
                else
                {
                    SeriesCollection[0].Values.Add((double)(i*5));
                    Labels[i] = "NoobManager";
                }
            }

            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        
    }
}

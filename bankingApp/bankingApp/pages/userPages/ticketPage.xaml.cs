﻿using System;
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
    /// Interaction logic for ticketPage.xaml
    /// </summary>
    public partial class ticketPage : Page
    {
        bsappDataContext db = new bsappDataContext();
        public string managerName { get; set; }
        public List<string> managers { set; get; }
        public ticketPage(bsappDataContext db)
        {
            this.db = db;
            InitializeComponent();
            managers = db.Users.Where(u => u.Type == "manager")
                .Select(u => u.Username).ToList();
            cbManagerName.ItemsSource = managers;
        }

        private void cbManagerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbManagerName.SelectedItem != null)
                this.managerName = cbManagerName.SelectedValue.ToString();
        }

        private void cbManagerName_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = cbManagerName.Text.ToLower();

            if (e.Key == System.Windows.Input.Key.Back || e.Key == System.Windows.Input.Key.Delete)
            {
                // If Backspace or Delete is pressed, reset the filtering
                cbManagerName.ItemsSource = managers;
            }
            else
            {
                // Filter the items based on the entered text
                cbManagerName.ItemsSource = managers
                    .Where(item => item.ToLower().Contains(searchText))
                    .ToList();
            }

            cbManagerName.IsDropDownOpen = true;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

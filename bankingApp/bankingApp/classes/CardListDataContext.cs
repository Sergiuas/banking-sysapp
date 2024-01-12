using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankingApp.classes
{
    internal class CardListDataContext : INotifyPropertyChanged
    {
        private int _currentPage = 1;
        private int _numberOfPages = 10;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged("CurrentPage");
                }
            }
        }

        public int NumberOfPages
        {
            get { return _numberOfPages; }
            set
            {
                if (_numberOfPages != value)
                {
                    _numberOfPages = value;
                    OnPropertyChanged("NumberOfPages");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

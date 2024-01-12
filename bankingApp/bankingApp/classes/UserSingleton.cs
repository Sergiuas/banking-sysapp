using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankingApp.classes
{
   public enum UserTypes
    {
        ADMIN,
        MANAGER,
        USER,
        INVALID
    };
    public class UserSingleton : INotifyPropertyChanged
    {
        private static UserSingleton instance = null;
        private static readonly object padlock = new object();

        private string _username;
        public string username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(username));
                }
            }
        }

        public string password { get; set; }
        public string email { get; set; }

        private string _firstName;
        public string firstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(firstName));
                }
            }
        }

        private string _lastName;
        public string lastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(lastName));
                }
            }
        }

        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string birthday { get; set; }
        public UserTypes userType { get; set; }

        public int UserID {  get; set; }

        public string type
        {
            get
            {
                if (userType == UserTypes.ADMIN) return "Admin";
                if (userType == UserTypes.MANAGER) return "Manager";
                if (userType == UserTypes.USER) return "User";
                return "User";
            }
            set
            {
                if (value == "Admin") userType = UserTypes.ADMIN;
                else if (value == "Manager") userType = UserTypes.MANAGER;
                else userType = UserTypes.USER;

                OnPropertyChanged(nameof(type));
            }
        }

        private UserSingleton()
        {

        }

        public static UserSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserSingleton();
                    }
                    return instance;
                }
            }
        }

        public void build(int userid_, string username_, string password_, string email_, string firstName_, string lastName_, UserTypes userType_)
        {
            this.UserID = userid_;
            this.username = username_;
            this.password = password_;
            this.email = email_;
            this.firstName = firstName_;
            this.lastName = lastName_;
            this.userType = userType_;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

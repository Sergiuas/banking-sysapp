using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankingApp.classes
{
    public enum UserType
    {
        User,
        Manager,
        Admin
    }
    public sealed class UserSingleton
    {
        private static UserSingleton instance = null;
        private static readonly object padlock = new object();

        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string birthday { get; set; }
        public UserType userType { get; set; }

        UserSingleton()
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

        public void build(string username_, string password_, string email_, string firstName_, string lastName_, UserType userType_)
        {
            this.username = username_;
            this.password = password_;
            this.email = email_;
            this.firstName = firstName_; this.lastName = lastName_;
            this.userType = userType_;
        }
    }

}

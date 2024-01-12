using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace bankingApp.classes
{
    internal class MessageBody
    {
        public string message { get; set; }
        public string sender { get; set; }
        public DateTime date { get; set; }
        
        public Visibility isFriend { get; set; }
        public Visibility isUser { get; set; }
        public MessageBody()
        {
            message = "HelloHelloHelloHelloHelloHelloHelloHelloHelloHelloHello";
            sender = "sender";
            date = DateTime.Now;
            isFriend= Visibility.Visible;
            isUser = Visibility.Hidden;
        }
    }
}

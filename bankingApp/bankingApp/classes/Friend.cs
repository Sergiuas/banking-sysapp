using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankingApp.classes
{
    internal class Friend
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Friend()
        {
            name = "name";
            username = "username";
            email = "email";
        }
    }
}

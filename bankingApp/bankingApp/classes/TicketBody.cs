using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankingApp.classes
{
    internal class TicketBody
    {
        public int id { get; set; }
        public string username { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public DateTime date { get; set; }
        public bool status { get; set; }
    }
}

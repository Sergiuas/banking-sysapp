using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankingApp.classes
{
    public class ShowCard
    {
            public string username { get; set; }

            public string cardnumber { get; set; }
            public float balance { get; set; }
            public DateTime? expirydate { get; set; }
    }
}

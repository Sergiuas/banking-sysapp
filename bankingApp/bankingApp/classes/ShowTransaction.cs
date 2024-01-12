using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankingApp.classes
{
    internal class ShowTransaction
    {
        public string SrcUsername { get; set; }

        public string DestUsername {  get; set; }
        public float Amount { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}

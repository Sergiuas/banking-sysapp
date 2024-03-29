//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bankingApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            this.Transactions = new HashSet<Transaction>();
            this.Transactions1 = new HashSet<Transaction>();
        }
    
        public int CardID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<int> ManagerID { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions1 { get; set; }
    }
}

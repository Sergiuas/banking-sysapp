﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bsappEntities : DbContext
    {
        public bsappEntities()
            : base("name=bsappEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MonthlyStatement> MonthlyStatements { get; set; }
        public virtual DbSet<SavingsGoal> SavingsGoals { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<MessagesView> MessagesViews { get; set; }
    }
}

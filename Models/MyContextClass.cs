using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Easy_Games.Models
{
    public class MyContextClass:DbContext
    {
        public MyContextClass():base("MyContextClass")
        {

        }

        public DbSet<Client> Client { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<Transaction> Transaction { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>()
                     .Property(x => x.ClientBalance)
                     .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
                     .Property(x => x.Amount)
                     .HasPrecision(18, 2);


        }

    }
}
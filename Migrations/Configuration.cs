namespace Easy_Games.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Easy_Games.Models.MyContextClass>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Easy_Games.Models.MyContextClass context)
        {
            context.Client.AddOrUpdate(x => x.ClientID,
                new Models.Client()
                {
                    ClientID=1,
                    Name="Peter",  //seeding
                    Surname="Parker",
                    ClientBalance=1000
                });
            context.Client.AddOrUpdate(x => x.ClientID,
                new Models.Client()
                {
                    ClientID = 2,
                    Name = "Tony",
                    Surname = "Stark",
                    ClientBalance = 800000
                });
            context.Client.AddOrUpdate(x => x.ClientID,
                new Models.Client()
                {
                    ClientID = 3,
                    Name = "Bruce",
                    Surname = "Banner",
                    ClientBalance = 254111
                });

            context.TransactionType.AddOrUpdate(x => x.TransactionTypeID,
                new Models.TransactionType()
                {
                    TransactionTypeID=1,
                    TransactionTypeName="Debit"
                });

            context.TransactionType.AddOrUpdate(x => x.TransactionTypeID,
               new Models.TransactionType()
               {
                   TransactionTypeID = 2,
                   TransactionTypeName = "Credit"
               });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

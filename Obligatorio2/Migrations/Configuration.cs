namespace Obligatorio2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Obligatorio2.Models.BL.OBL2P3PortLogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Obligatorio2.Models.BL.OBL2P3PortLogContext";
        }

        protected override void Seed(Obligatorio2.Models.BL.OBL2P3PortLogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

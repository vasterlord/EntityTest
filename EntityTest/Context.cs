using EntityTest.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTest
{
    public class Context : DbContext
    {
        public Context() : base("DBConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());
            Database.SetInitializer(new CreateDatabaseIfNotExists<Context>());
        }

        public DbSet<CountryProducing> CountryProducings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<Context>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}

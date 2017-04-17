using EntityTest.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTest.Data
{
    public class Context : DbContext
    {
        public Context() : base("DBConnection")
        { 

        }

        public DbSet<CountryProducing> CountryProducings { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(15,2));
            base.OnModelCreating(modelBuilder);
        }
    }
}

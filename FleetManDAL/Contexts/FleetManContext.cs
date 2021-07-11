using FleetManModel.Classes;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManDAL.Contexts
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class FleetManContext : DbContext
    {
        public FleetManContext() : base("MySqlContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FleetManContext>());
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<LoginModel> LoginDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

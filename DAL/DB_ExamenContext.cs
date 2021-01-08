using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Domain;

namespace DAL
{
    public class DB_ExamenContext : DbContext
    {
        public DB_ExamenContext()
            : base("name=DB_Examen")
        {
        }

        public virtual DbSet<Articulo> Articulos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
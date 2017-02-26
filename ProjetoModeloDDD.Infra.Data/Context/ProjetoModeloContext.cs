using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Infra.Data.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace ProjetoModeloDDD.Infra.Data.Context
{
    public class ProjetoModeloContext : DbContext
    {
        public ProjetoModeloContext()
            : base("ProjetoModeloDDD")
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            RemoveConventions(modelBuilder);
            SetCustomDataBaseConfiguration(modelBuilder);
            AddConfigurations(modelBuilder);
        }

        private static void AddConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new ProdutoConfiguration());
        }

        private static void RemoveConventions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        private static void SetCustomDataBaseConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties()
                            .Where(p => p.Name == p.ReflectedType.Name + "Id")
                            .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));
        }

        public override int SaveChanges()
        {
            var entries = GetEntries("DataCadastro");

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("DataCadastro").IsModified = false;
            }

            return base.SaveChanges();
        }

        private IEnumerable<DbEntityEntry> GetEntries(string property)
        {
            return ChangeTracker
                            .Entries()
                            .Where(entry => entry.Entity.GetType().GetProperty(property) != null);
        }
    }
}

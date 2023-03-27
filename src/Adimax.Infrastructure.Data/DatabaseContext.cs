using Adimax.Domain;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Adimax.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        // Representacao das tabelas no banco de dados.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductLog> ProductLogs { get; set; }

        // Personaliza a criacao das tabelas.
        // Metodos Override sobrescrevem metodos com o mesmo nome, nesse caso o OnModelCreating interno do Entity.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }

        // Referencia DataMappings
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=new_API;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False");
            //optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=master;trusted_Connection=true;TrustServerCertificate=True;Encrypt=False", b => b.MigrationsAssembly("Adimax.Infrastructure.Data"));
        }
    }
}
using Exam2023.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam2023.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }
        protected override void OnConfiguring
        (DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Exam2023;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}

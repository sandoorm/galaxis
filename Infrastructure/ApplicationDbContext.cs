using Microsoft.EntityFrameworkCore;

using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder)
            : base(optionsBuilder)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<FundToken> FundTokens { get; set; }
        public DbSet<TokenPriceHistory> TokenPriceHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Fund>().ToTable("Fund");
            modelBuilder.Entity<Token>().ToTable("Token");
            modelBuilder.Entity<FundToken>().ToTable("FundToken");
            modelBuilder.Entity<TokenPriceHistory>().ToTable("TokenPriceHistory");
        }
    }
}
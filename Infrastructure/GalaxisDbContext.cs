using Microsoft.EntityFrameworkCore;

using GalaxisProjectWebAPI.DataModel;

namespace GalaxisProjectWebAPI.Infrastructure
{
    public class GalaxisDbContext : DbContext
    {
        public GalaxisDbContext(DbContextOptions<GalaxisDbContext> optionsBuilder)
            : base(optionsBuilder)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<FundToken> FundTokens { get; set; }
        public DbSet<TokenPriceHistoricData> TokenPriceHistoricDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FundToken>().HasKey(f => new { f.FundId, f.TokenId, f.Timestamp });
            modelBuilder.Entity<TokenPriceHistoricData>()
                .HasIndex(x => x.TokenId)
                .IsUnique(false);
        }
    }
}
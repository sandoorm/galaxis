﻿// <auto-generated />
using GalaxisProjectWebAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GalaxisProjectWebAPI.Migrations
{
    [DbContext(typeof(GalaxisDbContext))]
    [Migration("20200925103530_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GalaxisProjectWebAPI.DataModel.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyAddress");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ContactPersonEmail");

                    b.Property<string>("ContactPersonName");

                    b.Property<string>("ContactPersonPhoneNumber");

                    b.Property<string>("OfficialEmailAddress");

                    b.Property<string>("Position");

                    b.Property<string>("RegistrationNumber");

                    b.Property<string>("vatNumber");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("GalaxisProjectWebAPI.DataModel.Fund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<double>("FloorLevel");

                    b.Property<string>("FundName");

                    b.Property<string>("InvestmentFundManagerName");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Funds");
                });

            modelBuilder.Entity("GalaxisProjectWebAPI.DataModel.FundToken", b =>
                {
                    b.Property<int>("FundId");

                    b.Property<int>("TokenId");

                    b.Property<long>("Timestamp");

                    b.Property<int>("Quantity");

                    b.HasKey("FundId", "TokenId", "Timestamp");

                    b.HasIndex("TokenId");

                    b.ToTable("FundTokens");
                });

            modelBuilder.Entity("GalaxisProjectWebAPI.DataModel.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Decimals");

                    b.Property<string>("Name");

                    b.Property<string>("Symbol");

                    b.HasKey("Id");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("GalaxisProjectWebAPI.DataModel.TokenPriceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("EUR_Price");

                    b.Property<long>("Timestamp");

                    b.Property<int>("TokenId");

                    b.Property<double>("USD_Price");

                    b.HasKey("Id");

                    b.HasIndex("TokenId")
                        .IsUnique();

                    b.ToTable("TokenPriceHistory");
                });

            modelBuilder.Entity("GalaxisProjectWebAPI.DataModel.Fund", b =>
                {
                    b.HasOne("GalaxisProjectWebAPI.DataModel.Company", "Company")
                        .WithMany("Funds")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GalaxisProjectWebAPI.DataModel.FundToken", b =>
                {
                    b.HasOne("GalaxisProjectWebAPI.DataModel.Fund", "Fund")
                        .WithMany("FundTokens")
                        .HasForeignKey("FundId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GalaxisProjectWebAPI.DataModel.Token", "Token")
                        .WithMany("FundTokens")
                        .HasForeignKey("TokenId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GalaxisProjectWebAPI.DataModel.TokenPriceHistory", b =>
                {
                    b.HasOne("GalaxisProjectWebAPI.DataModel.Token", "Token")
                        .WithOne("TokenPriceHistory")
                        .HasForeignKey("GalaxisProjectWebAPI.DataModel.TokenPriceHistory", "TokenId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

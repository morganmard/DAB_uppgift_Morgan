namespace DAB.Data;

using DAB.Models;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanHistory> LoanHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DAB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Book>()
                        .HasMany(e => e.Credits)
                        .WithOne(e => e.Book)
                        .HasForeignKey(e => e.BookID)
                        .IsRequired();

                modelBuilder.Entity<Author>()
                        .HasMany(e => e.Credits)
                        .WithOne(e => e.Author)
                        .HasForeignKey(e => e.AuthorID)
                        .IsRequired();

                modelBuilder.Entity<Loan>()
                        .HasOne(e => e.Book)
                        .WithOne(e => e.Loan)
                        .HasForeignKey<Loan>(e => e.BookID)
                        .IsRequired();
        }
}
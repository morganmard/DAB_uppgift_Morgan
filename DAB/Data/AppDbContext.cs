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
                        .HasMany(b => b.Credits)
                        .WithOne(c => c.Book)
                        .HasForeignKey(c => c.BookID)
                        .IsRequired();

                /* Make sure that Title is unique and create a index for it */
                modelBuilder.Entity<Book>()
                        .HasIndex(b => b.Title)
                        .IsUnique();

                /* Make sure that Author's name is unique and create a index for it */
                modelBuilder.Entity<Author>()
                        .HasIndex(a => a.Name)
                        .IsUnique();

                modelBuilder.Entity<Author>()
                        .HasMany(a => a.Credits)
                        .WithOne(c => c.Author)
                        .HasForeignKey(c => c.AuthorID)
                        .IsRequired();

                modelBuilder.Entity<Loan>()
                        .HasOne(l => l.Book)
                        .WithOne(b => b.Loan)
                        .HasForeignKey<Loan>(l => l.BookID)
                        .IsRequired();
        }
}
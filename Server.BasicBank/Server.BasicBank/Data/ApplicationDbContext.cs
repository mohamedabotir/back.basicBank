using Microsoft.EntityFrameworkCore;
using Server.BasicBank.Data.Entity;

namespace Server.BasicBank.Data
{
    public class ApplicationDbContext:DbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> ctx):base(ctx)
        {
            
        }

         public DbSet<Account> Accounts { get; set; }
         public DbSet<Transfers> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(e => e.Balance).HasPrecision(18, 2);
            modelBuilder.Entity<Transfers>().HasOne(e => e.Sender).WithOne().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Transfers>().HasOne(e => e.Reciever).WithOne().OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Transfers>().HasIndex(e => e.SenderId).IsUnique(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}

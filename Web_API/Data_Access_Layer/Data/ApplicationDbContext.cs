using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Show> Shows { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the base class OnModelCreating to ensure identity models are configured properly
        base.OnModelCreating(modelBuilder);

        // Ticket to ApplicationUser relationship (UserId as string)
        modelBuilder.Entity<Ticket>()
            .HasKey(t => t.TicketId); // Define primary key for Ticket

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tickets)  // Ensure this collection is defined in ApplicationUser class
            .HasForeignKey(t => t.UserId)
            .IsRequired();  // Foreign key is required

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Show)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.ShowId);
    }
}

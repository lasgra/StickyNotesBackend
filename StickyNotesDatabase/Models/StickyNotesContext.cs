using Microsoft.EntityFrameworkCore;

namespace StickyNotesDatabase.Models;

public class StickyNotesContext : DbContext
{
    public StickyNotesContext(DbContextOptions<StickyNotesContext> options) : base(options) { }
        
    public virtual DbSet<StickyNote> StickyNotes { get; set; }
    public virtual DbSet<User> Users { get; set; }

    // many-to-many
    public virtual DbSet<LikedNote> LikedNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StickyNotesContext).Assembly);
    }
}
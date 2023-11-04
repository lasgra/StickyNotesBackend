using Microsoft.EntityFrameworkCore;
using StickyNotesDatabase.Models;
using System.Reflection.Metadata;
using System.Text.Json;

namespace StickyNotesDatabase.Models
{
    public class StickyNotesContext : DbContext
    {
        public StickyNotesContext(DbContextOptions<StickyNotesContext> opts) : base(opts) { }
        public DbSet<LikedNote> LikedNotes { get; set; }
        public DbSet<StickyNoteDTO> StickyNotesDTO { get; set; }
        public DbSet<UserDTO> UserDTO { get; set; }

    }
}

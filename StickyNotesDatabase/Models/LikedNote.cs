using System.ComponentModel.DataAnnotations;

namespace StickyNotesDatabase.Models;

public class LikedNote
{
    [Key] 
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    
    [Required]
    public int StickyNoteId { get; set; }
    public virtual StickyNote StickyNote { get; set; } = null!;
}
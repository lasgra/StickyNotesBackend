using System.ComponentModel.DataAnnotations;

namespace StickyNotesDatabase.Models;

public class StickyNote
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Text { get; set; } = null!;
    [Required]
    public string BgColor { get; set; } = null!;
    [Required]
    public DateTime Date { get; set; } = DateTime.Now;
    
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    
    public virtual IList<LikedNote> LikedNotes { get; set; } = null!;
}
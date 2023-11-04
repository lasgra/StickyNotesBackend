using System.ComponentModel.DataAnnotations;

namespace StickyNotesDatabase.Models;

public class StickyNoteDTO
{
    public string Class { get; set; }
    public string Title { get; set; }

    [Key]
    public string Author { get; set; }
    public string Text { get; set; }
    public string Rate { get; set; }
    public string BgColor { get; set; }
    public string Date { get; set; }
    public bool Liked { get; set; }
}

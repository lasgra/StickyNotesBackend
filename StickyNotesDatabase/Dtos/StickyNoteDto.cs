namespace StickyNotesDatabase.Dtos;

public class StickyNoteDto
{
    public string Class { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Text { get; set; }
    public string Rate { get; set; }
    public string BgColor { get; set; }
    public DateTime Date { get; set; }
    public bool Liked { get; set; } = false;
}

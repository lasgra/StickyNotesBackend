using StickyNotesDatabase.Dtos;

namespace StickyNotesDatabase.Services;

public interface INotesService
{
    List<StickyNoteDto> GetRandomNote(string? username = null);
    string GetNote(string username);
    List<StickyNoteDto> GetFrontRandomNotes(string? username);
    StickyNoteDto PostNote(StickyNoteDto note);
    List<StickyNoteDto> GetBestNotes(string? username);
    List<StickyNoteDto> GetRecentNotes(string? username);
    string AddVote(string noteAuthor, string likingUser);
}
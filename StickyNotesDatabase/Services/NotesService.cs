using StickyNotesDatabase.Dtos;
using StickyNotesDatabase.Mappers;
using StickyNotesDatabase.Models;

namespace StickyNotesDatabase.Services;


public class NotesService(StickyNotesContext ctx) : INotesService
{
    public List<StickyNoteDto> GetRandomNote(string? username = null)
    {
        var notes = ctx.StickyNotes.ProjectToDto(username).ToArray();
        var randomNumber = new Random().Next(notes.Length);
        return new List<StickyNoteDto>{notes[randomNumber]};
    }
    public string GetNote(string username)
    {
        var notes = ctx.StickyNotes.ProjectToDto(username).ToList();
        return notes.Any(u => u.Author == username) ? "Exists" : "NotExists";
    }
    public List<StickyNoteDto> GetFrontRandomNotes(string? username = null)
    {
        var newNotes = new List<StickyNoteDto>();
        var notes = ctx.StickyNotes
            .ProjectToDto(username)
            .ToList();
        var selectedIndices = new List<int>();
        var random = new Random();

        for (int i = 0; i < 3; i++)
        {
            int index;
            do
            {
                index = random.Next(notes.Count);
            } while (selectedIndices.Contains(index));
            selectedIndices.Add(index);
            StickyNoteDto selectedNote = notes[index];
            selectedNote.Class = "Note" + (i+1);
            newNotes.Add(selectedNote);
        }
        return newNotes;
    }
    public StickyNoteDto PostNote(StickyNoteDto note)
    {
        var noteExists = ctx.StickyNotes.Any(n => n.User.Username == note.Author);
        if (noteExists) throw new Exception("Note exists");
        
        var stickyNote = new StickyNote
        {
            UserId = ctx.Users.FirstOrDefault(u => u.Username == note.Author)?.Id ?? throw new Exception("User not found"),
            BgColor = note.BgColor,
            Text = note.Text,
            Title = note.Title,
            Date = DateTime.UtcNow
        };
        ctx.StickyNotes.Add(stickyNote);
        ctx.SaveChanges();
        return stickyNote.ToDto(note.Author);
    }
    public List<StickyNoteDto> GetBestNotes(string? username)
    {
        var notes = ctx.StickyNotes
            .OrderByDescending(note => note.LikedNotes.Count)
            .Take(5)
            .ProjectToDto(username)
            .ToList();
        
        return notes;
    }
    public List<StickyNoteDto> GetRecentNotes(string? username)
    {
        var notes = ctx.StickyNotes
            .OrderByDescending(note => note.Date)
            .Take(5)
            .ProjectToDto(username)
            .ToList();
        
        return notes;
    }
    public string AddVote(string noteAuthor, string likingUser)
    {
        var noteToVote = ctx.StickyNotes.FirstOrDefault(note => note.User.Username == noteAuthor) ?? throw new Exception("User not found");

        var userWhoAlreadyLiked = ctx.LikedNotes
            .FirstOrDefault(ln => ln.User.Username == likingUser && ln.StickyNote.User.Username == noteAuthor);
        
        if (userWhoAlreadyLiked == null)
        {
            ctx.LikedNotes.Add(new LikedNote()
            {
                StickyNoteId = noteToVote.Id,
                UserId = ctx.Users.FirstOrDefault(u => u.Username == likingUser)?.Id ?? throw new Exception("User not found")
            });
            ctx.SaveChanges();
            return "Vote added";
        }

        var likesToRemove = ctx.LikedNotes
            .Where(ln => ln.StickyNote.User.Username == noteAuthor && ln.User.Username == likingUser);
        ctx.LikedNotes.RemoveRange(likesToRemove);
        ctx.SaveChanges();
        return "Vote removed";
    }
}
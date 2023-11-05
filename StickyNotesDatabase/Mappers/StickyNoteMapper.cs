using StickyNotesDatabase.Dtos;
using StickyNotesDatabase.Models;

namespace StickyNotesDatabase.Mappers;

public static class StickyNoteMapper
{
    public static IQueryable<StickyNoteDto> ProjectToDto(this IQueryable<StickyNote> queryable, string? userName)
    {
        return queryable.Select(n => new StickyNoteDto()
        {
            Class = "",
            Title = n.Title,
            Author = n.User.Username,
            Text = n.Text,
            Rate = n.LikedNotes.Count.ToString(),
            BgColor = n.BgColor,
            Date = n.Date,
            Liked = userName != null && n.LikedNotes.Any(ln => ln.User.Username == userName)
        });
    }
    
    public static StickyNoteDto ToDto(this StickyNote n, string? userName)
    {
        int rate = (n.LikedNotes != null) ? n.LikedNotes.Count : 0;
        bool liked = userName != null && n.LikedNotes != null && n.LikedNotes.Any(ln => ln.User.Username == userName);
        return new StickyNoteDto()
        {
            Class = "",
            Title = n.Title,
            Author = n.User.Username,
            Text = n.Text,
            Rate = rate.ToString(),
            BgColor = n.BgColor,
            Date = n.Date,
            Liked = liked
        };
    }
}
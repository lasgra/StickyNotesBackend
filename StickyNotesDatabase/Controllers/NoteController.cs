using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StickyNotesDatabase.Models;
using static Azure.Core.HttpHeader;

namespace SticyNotesDatabase.Controllers
{
    public class NoteController : Controller
    {
        private readonly StickyNotesContext _ctx;
        public NoteController(StickyNotesContext ctx)
        {
            _ctx = ctx;
        }
        public List<StickyNoteDTO> GetRandomNote(string Username)
        {
            Random random = new Random();
            List<StickyNoteDTO> notes = _ctx.StickyNotesDTO.ToList<StickyNoteDTO>();
            int index = random.Next(notes.Count);
            StickyNoteDTO note = notes[index];
            note.Liked = Username is not null ? _ctx.LikedNotes.FirstOrDefault(n => n.Author == note.Author).Users.Split("/-=aaa=-\\").Contains(Username) : false;
            List<StickyNoteDTO> notesa = new List<StickyNoteDTO>();
            notesa.Add(note);
            return notesa;
        }
        public string GetNote(string Author)
        {
            List<StickyNoteDTO> notes = _ctx.StickyNotesDTO.ToList<StickyNoteDTO>();
            if (notes.Any(u => u.Author == Author))
            {
                return "Exists";
            }
            return "NotExists";
        }
        public List<StickyNoteDTO> GetFrontRandomNotes()
        {
            List<StickyNoteDTO> NewNotes = new List<StickyNoteDTO>();
            List<StickyNoteDTO> Notes = _ctx.StickyNotesDTO.ToList<StickyNoteDTO>();
            List<int> selectedIndices = new List<int>();
            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                int index;
                do
                {
                    index = random.Next(Notes.Count);
                } while (selectedIndices.Contains(index));
                selectedIndices.Add(index);
                StickyNoteDTO selectedNote = Notes[index];
                selectedNote.Class = "Note" + (i+1);
                NewNotes.Add(selectedNote);
            }
            return NewNotes;
        }
        public StickyNoteDTO PostNote(StickyNoteDTO note)
        {
            List<StickyNoteDTO> Notes = _ctx.StickyNotesDTO.ToList<StickyNoteDTO>();
            if (Notes.Any(u => u.Author == note.Author))
            {
                return null;
            }
            if (note.Text != null && note.Text != null && note.Title != null && note.BgColor != null)
            {
                var Note = new StickyNoteDTO
                {
                    Author = note.Author,
                    BgColor = note.BgColor,
                    Class = "",
                    Date = note.Date,
                    Rate = "0",
                    Text = note.Text,
                    Title = note.Title,
                    Liked = false,
                };
                var Liked = new LikedNote
                {
                    Author = note.Author,
                    Users = ""
                };
                _ctx.LikedNotes.Add(Liked);
                _ctx.StickyNotesDTO.Add(Note);
                _ctx.SaveChanges();
                return note;
            }
            return null;
        }
        public List<StickyNoteDTO> GetBestNotes(string? Username)
        {
            List<StickyNoteDTO> Notes = _ctx.StickyNotesDTO.ToList<StickyNoteDTO>();
            var result = Notes
                .OrderByDescending(note => int.Parse(note.Rate))
                .Take(5)
                .Select(note => new StickyNoteDTO()
                {
                    Class = note.Class,
                    Title = note.Title,
                    BgColor = note.BgColor,
                    Author = note.Author,
                    Text = note.Text,
                    Date = note.Date,
                    Rate = note.Rate,
                    Liked = Username is not null ? _ctx.LikedNotes.FirstOrDefault(n => n.Author == note.Author).Users.Split("/-=aaa=-\\").Contains(Username) : false,
                })
                .ToList();
            return result;
        }
        public List<StickyNoteDTO> GetRecentNotes(string? Username)
        {
            List<StickyNoteDTO> Notes = _ctx.StickyNotesDTO.ToList<StickyNoteDTO>();
            List<LikedNote> LikedNotes = _ctx.LikedNotes.ToList<LikedNote>();
            var result = Notes
                .OrderByDescending(note => Int128.Parse(note.Date))
                .Take(5)
                .Select(note => new StickyNoteDTO()
                {
                    Class = note.Class,
                    Title = note.Title,
                    BgColor = note.BgColor,
                    Author = note.Author,
                    Text = note.Text,
                    Date = note.Date,
                    Rate = note.Rate,
                    Liked = Username is not null ? _ctx.LikedNotes.FirstOrDefault(n => n.Author == note.Author).Users.Split("/-=aaa=-\\").Contains(Username) : false,
                })
                .ToList();
            return result;
        }
        public string AddVote(string Author, string Username)
        {
            List<StickyNoteDTO> Notes = _ctx.StickyNotesDTO.ToList<StickyNoteDTO>();
            List<LikedNote> LikedNotes = _ctx.LikedNotes.ToList<LikedNote>();
            StickyNoteDTO noteToVote = Notes.FirstOrDefault(note => note.Author == Author)!;

            if (noteToVote != null)
            {
                string[] UsersWhoLiked = LikedNotes?.FirstOrDefault(Note => Note.Author == Author).Users.Split("/-=aaa=-\\");
                string FindUsername = UsersWhoLiked?.FirstOrDefault(user => user == Username)!;
                if (FindUsername == null)
                {
                    _ctx.StickyNotesDTO.FirstOrDefault(n => n.Author == Author)!.Rate = (int.Parse(_ctx.StickyNotesDTO.FirstOrDefault(n => n.Author == Author)!.Rate) + 1).ToString();
                    _ctx.LikedNotes.FirstOrDefault(n => n.Author == Author).Users = _ctx.LikedNotes.FirstOrDefault(n => n.Author == Author).Users + "/-=aaa=-\\" + Username;
                    _ctx.SaveChanges();
                    return "Vote added";
                }
                _ctx.StickyNotesDTO.FirstOrDefault(n => n.Author == Author)!.Rate = (int.Parse(_ctx.StickyNotesDTO.FirstOrDefault(n => n.Author == Author)!.Rate) - 1).ToString();
                string newlist = "";
                foreach (var User in UsersWhoLiked)
                {
                    if (User != Username && User != "")
                    {
                        newlist = newlist + "/-=aaa=-\\" + User;
                    }
                }
                _ctx.LikedNotes.FirstOrDefault(n => n.Author == Author).Users = newlist;
                _ctx.SaveChanges();
                return "Vote removed";
            }
            return "Author doesn't exists";
        }
    }
}

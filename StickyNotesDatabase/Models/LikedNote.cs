using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StickyNotesDatabase.Models
{
    public class LikedNote
    {
        [Key]
        public string Author { get; set; }
        public string Users { get; set; }
    }
}

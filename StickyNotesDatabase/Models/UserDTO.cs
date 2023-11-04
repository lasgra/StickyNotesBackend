using System.ComponentModel.DataAnnotations;

namespace StickyNotesDatabase.Models;

public class UserDTO
{
    [Key]
    public string Username { get; set; }
    public string IP { get; set; }
}

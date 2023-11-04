using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace StickyNotesDatabase.Models;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Ip { get; set; } = null!;
}
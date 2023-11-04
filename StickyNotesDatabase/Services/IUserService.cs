using StickyNotesDatabase.Models;

namespace StickyNotesDatabase.Services;

public interface IUserService
{
    string CheckForUser(User user);
    string CreateUser(User user);
}
using StickyNotesDatabase.Models;

namespace StickyNotesDatabase.Services;

public class UserService(StickyNotesContext ctx) : IUserService
{
    public string CheckForUser(User user)
    {
        var users = ctx.Users.ToList();
        var found = users.FirstOrDefault(u => u.Ip == user.Ip || u.Username == user.Username);
        return found == null ? "Nima" : "Jest";
    }
    public string CreateUser(User user)
    {
        var users = ctx.Users.ToList<User>();
        var found = users.FirstOrDefault(u => u.Username == user.Username);
        if (found != null) return "NotAdded";
        ctx.Users.Add(user);
        ctx.SaveChanges();
        return "Added";
    }

}
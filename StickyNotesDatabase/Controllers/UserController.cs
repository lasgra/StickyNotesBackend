using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StickyNotesDatabase.Models;

namespace SticyNotesDatabase.Controllers
{
    public class UserController : Controller
    {
        private readonly StickyNotesContext _ctx;
        public UserController(StickyNotesContext ctx)
        {
            _ctx = ctx;
        }
        public string CheckForUser(UserDTO user)
        {
            List<UserDTO> Users = _ctx.UserDTO.ToList<UserDTO>();
            var Found = Users.FirstOrDefault(u => u.IP == user.IP || u.Username == user.Username);
            if (Found == null)
            {
                return "Nima";
            }
            return "Jest";
        }
        public string CreateUser(UserDTO user)
        {
            List<UserDTO> Users = _ctx.UserDTO.ToList<UserDTO>();
            var Found = Users.FirstOrDefault(u => u.Username == user.Username);
            if (Found == null)
            {
                _ctx.UserDTO.Add(user);
                _ctx.SaveChanges();
                return "Added";
            }
            return "NotAdded";
        }

    }
}

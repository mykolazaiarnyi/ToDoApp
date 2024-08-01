using System.Security.Claims;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Api.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext _context;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _context = accessor.HttpContext!;
        }

        public string UserId => _context!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}

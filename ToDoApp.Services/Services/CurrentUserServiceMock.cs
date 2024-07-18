using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services;

public class CurrentUserServiceMock : ICurrentUserService
{
    public int UserId => 1;
}

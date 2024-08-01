using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Data.Models;

public class User : IdentityUser
{
    public ICollection<ToDoItem> ToDoItems { get; set; }
}

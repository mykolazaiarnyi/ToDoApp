using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Services.Interfaces;

public interface IToDoItemService
{
    Task<List<ToDoItem>> GetAsync();

    Task CreateAsync(CreateToDoItemDto itemDto);

    Task UpdateStatusAsync(int id);
}

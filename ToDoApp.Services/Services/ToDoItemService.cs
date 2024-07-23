using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Dtos;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly ToDoContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ToDoItemService(ToDoContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<ToDoItem>> GetAsync()
    {
        return await _context.ToDoItems
            .Where(x => x.UserId == _currentUserService.UserId)
            .ToListAsync();
    }

    public async Task CreateAsync(CreateToDoItemDto itemDto)
    {
        var item = new ToDoItem
        {
            Description = itemDto.Description,
            DueDate = itemDto.DueDate,
            IsDone = false,
            UserId = _currentUserService.UserId,
        };
        _context.ToDoItems.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);

        if (item is null) 
        {
            throw new ToDoItemNotFoundException();
        }

        if (item.UserId != _currentUserService.UserId) 
        {
            throw new ToDoItemHasDifferentOwnerException();
        }

        item.IsDone = !item.IsDone;
        await _context.SaveChangesAsync();
    }
}

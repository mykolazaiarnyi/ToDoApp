using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoApp.Data.Context;
using ToDoApp.Data.Models;
using ToDoApp.Services.Exceptions;
using ToDoApp.Services.Interfaces;
using ToDoApp.Services.Services;

namespace ToDoApp.Service.Tests.Services;

public class ToDoItemServiceTests
{
    private const string UserId = "1";

    [Fact]
    public async Task UpdateStatusAsync_WithNonExistingItem_ThrowsToDoItemNotFoundException()
    {
        // Arrange
        var currentUserService = new Mock<ICurrentUserService>();
        currentUserService.Setup(x => x.UserId).Returns(UserId);

        var context = GetDbContext();
        var service = new ToDoItemService(context, currentUserService.Object);

        // Act
        Func<Task> act = async () => await service.UpdateStatusAsync(1);

        // Assert
        await Assert.ThrowsAsync<ToDoItemNotFoundException>(act);
    }

    [Fact]
    public async Task UpdateStatusAsync_TaskHasDifferentOwner_ThrowsToDoItemHasDifferentOwnerException()
    {
        // Arrange
        var currentUserService = new Mock<ICurrentUserService>();
        currentUserService.Setup(x => x.UserId).Returns(UserId);

        var context = GetDbContext();
        var item = new ToDoItem
        {
            Id = 1,
            Description = "test",
            UserId = "2",
        };
        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var service = new ToDoItemService(context, currentUserService.Object);

        // Act
        Func<Task> act = async () => await service.UpdateStatusAsync(1);

        // Assert
        await Assert.ThrowsAsync<ToDoItemHasDifferentOwnerException>(act);
    }

    [Theory]
    [InlineData(false, true)]
    [InlineData(true, false)]
    public async Task UpdateStatusAsync_TaskStatusIsToggled(bool currentIsDone, bool expectedIsDone)
    {
        // Arrange
        var currentUserService = new Mock<ICurrentUserService>();
        currentUserService.Setup(x => x.UserId).Returns(UserId);

        var context = GetDbContext();
        var item = new ToDoItem
        {
            Id = 1,
            Description = "test",
            UserId = UserId,
            IsDone = currentIsDone,
        };
        context.ToDoItems.Add(item);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var service = new ToDoItemService(context, currentUserService.Object);

        // Act
        await service.UpdateStatusAsync(1);

        // Assert
        var updatedItem = await context.ToDoItems.FindAsync(1);
        Assert.Equal(expectedIsDone, updatedItem.IsDone);
    }

    private ToDoContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ToDoContext(options);
    }
}

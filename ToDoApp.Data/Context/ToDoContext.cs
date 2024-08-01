using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Context;

public class ToDoContext : IdentityDbContext<User>
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
    {   
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<ToDoItem>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()"); 

        modelBuilder.Entity<ToDoItem>()
            .Property(x => x.Description)
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<User>()
            .HasMany(t => t.ToDoItems)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        base.OnModelCreating(modelBuilder);
    }
}

using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Context;

public class ToDoContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDoAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .LogTo(Console.WriteLine);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoItem>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<ToDoItem>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<User>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<User>()
            .HasMany(t => t.ToDoItems)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        base.OnModelCreating(modelBuilder);


        //modelBuilder.Entity<User>().HasData(
        //    new User
        //    {
        //        Id = 1,
        //        Name = "John Doe"
        //    }
        //);

        //modelBuilder.Entity<ToDoItem>().HasData(
        //    new ToDoItem
        //    {
        //        Id = 1,
        //        Description = "Buy milk",
        //        DueDate = DateTime.Now.AddDays(1),
        //        IsDone = false,
        //        UserId = 1
        //    },
        //    new ToDoItem
        //    {
        //        Id = 2,
        //        Description = "Buy bread",
        //        DueDate = DateTime.Now.AddDays(1),
        //        IsDone = false,
        //        UserId = 1
        //    }
        //);
    }
}

namespace ToDoApp.Data.Models;

public class ToDoItem
{
    public int Id { get; set; }

    public string Description { get; set; }

    public bool IsDone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime DueDate { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }
}

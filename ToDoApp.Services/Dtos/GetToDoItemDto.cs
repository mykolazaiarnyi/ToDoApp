namespace ToDoApp.Services.Dtos;

public class GetToDoItemDto
{
    public int Id { get; set; }

    public string Description { get; set; }

    public bool IsDone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime DueDate { get; set; }
}

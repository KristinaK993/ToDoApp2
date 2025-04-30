namespace ToDoApp.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation
    public ICollection<TaskItem> Tasks { get; set; }
}

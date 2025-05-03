namespace ToDoApp.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Password { get; set; }  
    public string Role { get; set; }      // För t.ex. "Admin", "User", etc.

    // Navigation
    public ICollection<TaskItem> Tasks { get; set; }
}

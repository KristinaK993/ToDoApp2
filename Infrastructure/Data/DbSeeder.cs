using Bogus;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void Seed(ToDoDbContext context)
        {
            try
            {
                // Kontrollera om data redan finns – annars hoppa över seedning
                if (context.Users.Any() || context.Tasks.Any() || context.Categories.Any())
                {
                    Console.WriteLine("Seedning avbruten: data finns redan.");
                    return;
                }

                //Lägg till kategorier
                var categories = new List<Category>
                {
                    new Category { Name = "Work" },
                    new Category { Name = "Personal" },
                    new Category { Name = "Hobby" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
                Console.WriteLine("Kategorier skapade.");

                //Lägg till användare
                var users = new List<User>
                {
                    new User
                    {
                        Username = "admin",
                        Email = "admin@example.com",
                        PasswordHash = PasswordHasher.Hash("admin123"),
                        Role = "Admin"
                    },
                    new User
                    {
                        Username = "user1",
                        Email = "user1@example.com",
                        PasswordHash = PasswordHasher.Hash("user123"),
                        Role = "User"
                    }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
                Console.WriteLine("Användare skapade.");

                // Generera tasks kopplade till befintliga kategorier och användare
                var taskFaker = new Faker<TaskItem>()
                    .RuleFor(t => t.Title, f => f.Lorem.Sentence())
                    .RuleFor(t => t.Description, f => f.Lorem.Paragraph())
                    .RuleFor(t => t.IsCompleted, f => f.Random.Bool())
                    .RuleFor(t => t.CategoryId, f => f.PickRandom(categories).Id)
                    .RuleFor(t => t.UserId, f => f.PickRandom(users).Id);

                var fakeTasks = taskFaker.Generate(10);
                context.Tasks.AddRange(fakeTasks);
                context.SaveChanges();
                Console.WriteLine("Tasks skapade.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("FEL vid seedning: " + ex.Message);
                throw; 
            }
        }
    }
}

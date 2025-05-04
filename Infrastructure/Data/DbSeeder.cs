using Bogus;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void Seed(ToDoDbContext context)
        {
            //Om det redan finns data – hoppa över
            if (context.Users.Any() || context.Tasks.Any() || context.Categories.Any())
                return;

            // Lägg till kategorier först
            var categories = new List<Category>
            {
                new Category { Name = "Work" },
                new Category { Name = "Personal" },
                new Category { Name = "Hobby" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges(); 

            // Lägg till användare
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

            // Generera tasks kopplade till riktiga kategorier och användare
            var taskFaker = new Faker<TaskItem>()
                .RuleFor(t => t.Title, f => f.Lorem.Sentence())
                .RuleFor(t => t.Description, f => f.Lorem.Paragraph())
                .RuleFor(t => t.IsCompleted, f => f.Random.Bool())
                .RuleFor(t => t.CategoryId, f => f.PickRandom(categories).Id)
                .RuleFor(t => t.UserId, f => f.PickRandom(users).Id);

            context.Tasks.AddRange(taskFaker.Generate(10));
            context.SaveChanges();
        }
    }
}

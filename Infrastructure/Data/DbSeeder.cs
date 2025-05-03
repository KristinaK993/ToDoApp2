using Bogus;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void Seed(ToDoDbContext context)
        {
            if (!context.Users.Any())
            {
                // Faker för User
                var userFaker = new Faker<User>()
                    .RuleFor(u => u.Username, f => f.Internet.UserName())
                    .RuleFor(u => u.Password, f => "password") // Alla har samma lösenord
                    .RuleFor(u => u.Role, f => "User");

                context.Users.AddRange(userFaker.Generate(5)); // Skapa 5 användare
            }

            if (!context.Categories.Any())
            {
                var catFaker = new Faker<Category>()
                    .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0]);

                context.Categories.AddRange(catFaker.Generate(3));
            }

            if (!context.Tasks.Any())
            {
                var taskFaker = new Faker<TaskItem>()
                    .RuleFor(t => t.Title, f => f.Lorem.Sentence(3))
                    .RuleFor(t => t.Description, f => f.Lorem.Paragraph())
                    .RuleFor(t => t.IsCompleted, f => f.Random.Bool())
                    .RuleFor(t => t.CategoryId, f => f.Random.Int(1, 3));

                context.Tasks.AddRange(taskFaker.Generate(10));
            }

            context.SaveChanges();
        }
    }
}

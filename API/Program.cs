using ToDoApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using ToDoApp.Application.Tasks.Commands.CreateTask;
using ToDoApp.Application.Categories.Mapping; 
using ToDoApp.Application.Tasks.Mapping;      
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;   
using System.Text;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using FluentValidation;
using ToDoApp.Application.Tasks.Behavior;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Konfigurera databas
            builder.Services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlServer("Server=DESKTOP-CLOUDR7\\SQLEXPRESS;Database=ToDoApp2;Trusted_Connection=True;TrustServerCertificate=True;"));

            // L�gg till tj�nster
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // MediatR - pekar p� ett command i Application-lagret
            builder.Services.AddMediatR(typeof(CreateTaskCommand).Assembly);

            builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
            builder.Services.AddScoped<IRepository<TaskItem>, Repository<TaskItem>>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskCommandValidator>();
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



            // AutoMapper - laddar alla profiler i projektets assemblies
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //JWT authentisering
            var jwtSettings = builder.Configuration.GetSection("Jwt"); //h�mtar inst�llningar (hela objektet)
            builder.Services.AddAuthentication(options => //aktiverar JWT som inloggningss�tt
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//vilket autentiseringssystem som ska anv�ndas i detta fall, bearer
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters //s�kerhetsregler f�r JWT
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])) //h�r skapas nyckeln
                };
            });

            var app = builder.Build();

            // HTTP pipeline
            if (app.Environment.IsDevelopment()) //i utvecklingsl�ge (s�krare att inte exp swagger i produktion)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
       
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope()) //DbSeed
            {
                var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
                DbSeeder.Seed(db);
            }

            app.Run();
        }
    }
}

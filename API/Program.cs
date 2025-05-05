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

            // Lägg till tjänster
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "ToDoApp API", Version = "v1" });

                //Lägg till JWT support i Swagger (knappen)
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Skriv 'Bearer' [mellanslag] och sedan din token.\nExempel: Bearer 12345abcdef"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });


            // MediatR - pekar på ett command i Application-lagret
            builder.Services.AddMediatR(typeof(CreateTaskCommand).Assembly);

            builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
            builder.Services.AddScoped<IRepository<TaskItem>, Repository<TaskItem>>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskCommandValidator>();
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



            // AutoMapper - laddar alla profiler i projektets assemblies
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //JWT authentisering
            var jwtSettings = builder.Configuration.GetSection("Jwt"); //hämtar inställningar (hela objektet)
            builder.Services.AddAuthentication(options => //aktiverar JWT som inloggningssätt
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//vilket autentiseringssystem som ska användas i detta fall, bearer
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters //säkerhetsregler för JWT
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])) //här skapas nyckeln
                };
            });

            var app = builder.Build();

            // HTTP pipeline
            if (app.Environment.IsDevelopment()) //i utvecklingsläge (säkrare att inte exp swagger i produktion)
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

using ToDoApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using ToDoApp.Application.Tasks.Commands.CreateTask;
using AutoMapper;
using ToDoApp.Application.Tasks.Mapping;
using AutoMapper;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseSqlServer("Server=DESKTOP-CLOUDR7\\SQLEXPRESS;Database=ToDoApp2;Trusted_Connection=True;TrustServerCertificate=True;"));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(typeof(CreateTaskCommand).Assembly);
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TaskProfile()); // Lägg till fler profiler vid behov
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

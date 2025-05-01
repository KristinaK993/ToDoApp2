using ToDoApp.Application.Tasks.DTOs;
using AutoMapper;
using ToDoApp.Application.Tasks.DTOs;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Tasks.Mapping
{
    public class TaskProfile : Profile
    {
        public TaskProfile() 
        {
            CreateMap<TaskItem, TaskDto>(); //från entity till DTO

            CreateMap<TaskDto, TaskItem>(); //från dto till entity(om man vill skapa/uppdatera)
        }
    }
}

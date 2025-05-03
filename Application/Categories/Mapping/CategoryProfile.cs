using AutoMapper;
using ToDoApp.Application.Categories.Mapping;
using ToDoApp.Application.Tasks.DTOs;
using ToDoApp.Domain.Entities;
using ToDoApp.Application.Categories.Dto;


namespace ToDoApp.Application.Categories.Mapping
{
    public class CategoryProfile : Profile //denna klass innehåller mappingsregler mellan entity och Dto
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDto>(); //Mappa från entitet till Dto - används vid hämtning

            CreateMap<CategoryDto, Category>(); //Mappa från Dto till entitet - används vid skapande
        }

    }
}

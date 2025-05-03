using MediatR;
using ToDoApp.Application.Categories.Dto;
using System.Collections.Generic;
using ToDoApp.Application.Tasks.DTOs;

namespace ToDoApp.Application.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}

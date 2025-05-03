using MediatR; 
using ToDoApp.Application.Tasks.DTOs;

namespace ToDoApp.Application.Tasks.Queries
{
    // Detta är en MediatR-query som returnerar en lista av TaskDto
    public class GetAllTasksQuery : IRequest<List<TaskDto>>
    {

    }
}

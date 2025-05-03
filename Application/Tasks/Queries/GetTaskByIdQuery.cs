using ToDoApp.Application.Tasks.DTOs;
using MediatR;

namespace ToDoApp.Application.Tasks.Queries
{
    //query som skickar med ID och returnerar en TaskDto
    public class GetTaskByIdQuery : IRequest<TaskDto>
    {
        public int Id { get; set; }
        public GetTaskByIdQuery() { }
        public GetTaskByIdQuery(int id) { Id = id; }
    }
}

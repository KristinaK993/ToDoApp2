using MediatR;
using ToDoApp.Application.Tasks.DTOs;
using ToDoApp.Application.Helpers;

namespace ToDoApp.Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<OperationResult<bool>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }


    }
}

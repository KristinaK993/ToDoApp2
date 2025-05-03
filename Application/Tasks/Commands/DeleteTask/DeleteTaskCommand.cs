using MediatR;
using ToDoApp.Application.Helpers;

namespace ToDoApp.Application.Tasks.Commands.DeleteTask
{
    //coomand so skickas när en taskitem ska tas bort 
    public class DeleteTaskCommand : IRequest<OperationResult<bool>>
    {
        public int Id { get; set; }
    }
}

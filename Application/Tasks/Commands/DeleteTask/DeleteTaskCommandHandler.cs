using MediatR;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Application.Helpers;

namespace ToDoApp.Application.Tasks.Commands.DeleteTask
{
    // Handler som hanterar DeleteTaskCommand
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, OperationResult<bool>>
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public DeleteTaskCommandHandler(IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            // Hämta uppgiften som ska tas bort
            var task = await _taskRepository.GetByIdAsync(request.Id);

            if (task == null)
                return OperationResult<bool>.Fail("Uppgiften hittades inte.");

            await _taskRepository.DeleteAsync(task);

            return OperationResult<bool>.Ok(true);
        }
    }
}

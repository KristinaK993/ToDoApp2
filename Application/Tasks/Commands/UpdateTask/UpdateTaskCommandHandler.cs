using MediatR;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Application.Helpers;

namespace ToDoApp.Application.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, OperationResult<bool>>
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public UpdateTaskCommandHandler(IRepository<TaskItem> taskrepository)
        {
            _taskRepository = taskrepository;
        }

        public async Task<OperationResult<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            // Hämta befintlig task från databasen
            var task = await _taskRepository.GetByIdAsync(request.Id);

            if (task == null)
                return OperationResult<bool>.Fail("Uppgiften hittades inte.");

            // Uppdatera värden
            task.Title = request.Title;
            task.Description = request.Description;
            task.IsCompleted = request.IsCompleted;

            await _taskRepository.UpdateAsync(task);

            return new OperationResult<bool> { Success = true };

        }
    }
}

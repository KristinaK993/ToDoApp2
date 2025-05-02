using MediatR;
using ToDoApp.Application.Helpers;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, OperationResult<int>>
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public CreateTaskCommandHandler(IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<OperationResult<int>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                IsCompleted = false
            };
            try
            {
                await _taskRepository.AddAsync(task);
                return OperationResult<int>.Ok(task.Id);
            }

            catch (Exception ex) 
            {
                return OperationResult<int>.Fail($"Fel vid skapande: {ex.Message}");
            }
        }
    }
}

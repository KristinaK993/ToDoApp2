using MediatR;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public CreateTaskCommandHandler(IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                IsCompleted = false
            };
            await _taskRepository.AddAsync(task);
            return task.Id;
        }
    }
}

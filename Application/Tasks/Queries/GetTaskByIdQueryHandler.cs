using AutoMapper;
using MediatR;
using ToDoApp.Application.Tasks.DTOs;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Tasks.Queries
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
    {
        private readonly IRepository<TaskItem> _taskItemRepository;
        private readonly IMapper _mapper;

        public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            //HÄmta task från Databas
            var task = await _taskItemRepository.GetByIdAsync(request.Id);

            if (task == null)
                return null;

            //Mappa till Dto
            return _mapper.Map<TaskDto>(task);
        }
    }
}

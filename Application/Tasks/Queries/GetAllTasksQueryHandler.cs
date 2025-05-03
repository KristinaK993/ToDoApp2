using AutoMapper; 
using MediatR; 
using ToDoApp.Application.Tasks.DTOs; 
using ToDoApp.Domain.Entities; 
using ToDoApp.Domain.Interfaces;
using ToDoApp.Application.Tasks.Queries;

namespace ToDoApp.Application.Tasks.Queries
{
    //handler som tar emot GetAllTaskQuery oxh returnera lista av taskDto
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskDto>>
    {
        private readonly IRepository<TaskItem> _taskRepository; //repository för taskItem
        private readonly IMapper _mapper; //mapper för att konvertera till Dto

        //Konstruktor med DI
        public GetAllTasksQueryHandler (IRepository<TaskItem> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        // Handler-metod som hämtar alla TaskItem, mappar till TaskDto och returnerar listan
        public async Task<List<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllAsync(); // Hämta alla från databas
            return _mapper.Map<List<TaskDto>>(tasks); // Mappa till DTO och returnera
        }
    }
}

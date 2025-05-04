using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Tasks.Commands.CreateTask;
using ToDoApp.Application.Helpers;
using ToDoApp.Application.Tasks.Queries;
using ToDoApp.Application.Tasks.Commands.UpdateTask;
using ToDoApp.Application.Tasks.Commands.DeleteTask;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<TaskItem> _taskRepository;

        public TaskController(IMediator mediator, IRepository<TaskItem> taskRepository)
        {
            _mediator = mediator;
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResult<int>>> Create(CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")] // GET /api/task/{id}
        public async Task<IActionResult> GetById(int id)
        {
            // Skapa query med det angivna id:t
            var query = new GetTaskByIdQuery(id);

            // Skicka queryn till MediatR
            var result = await _mediator.Send(query);

            // Om ingen task hittades, returnera 404
            if (result == null)
                return NotFound();

            // Returnera resultatet med status 200 OK
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTaskCommand command)
        {
            // Kontrollera att ID i URL matchar ID i body
            if (id != command.Id)
                return BadRequest("ID i URL matchar inte ID i body.");

            // Skicka kommandot via MediatR
            var result = await _mediator.Send(command);

            // Om uppdateringen lyckades
            if (result.Success)
                return Ok("Uppgiften har uppdaterats.");

            // Om något gick fel, returnera felmeddelande
            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Skicka Delete-kommandot via MediatR
            var result = await _mediator.Send(new DeleteTaskCommand { Id = id });

            // Om borttagning lyckades
            if (result.Success)
                return Ok("Uppgiften har tagits bort.");

            // Annars skicka felmeddelande
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks() //hämta alla tasks
        {
            var tasks = await _taskRepository.GetAllAsync(); 
            return Ok(tasks);
        }

    }
}

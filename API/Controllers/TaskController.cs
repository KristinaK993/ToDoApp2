using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Tasks.Commands.CreateTask;
using ToDoApp.Application.Helpers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResult<int>>> Create(CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}

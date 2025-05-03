using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Categories.Dto;
using ToDoApp.Application.Categories.Queries;
using ToDoApp.Application.Tasks.DTOs;
using ToDoApp.Application.Categories.Commands.UpdateCategory;
using ToDoApp.Application.Categories.Commands;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator) // Konstruktor – tar emot IMediator via dependency injection
        {
            _mediator = mediator; //Injicerar MediatR så controller kan skicka queries
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            // Skicka query till MediatR som hanteras av GetAllCategoriesQueryHandler
            var result = await _mediator.Send(new GetAllCategoriesQuery()); //skickar query till handler 

            return Ok(result); //returnerar listan med CategoryDTo från databasen
        }

        [HttpGet("{id}")] // Anger att detta är en GET med en parameter i URL:en
        public async Task<IActionResult> GetById(int id)
        {
            // Skapar en query med det angivna id:t
            var query = new GetCategoryByIdQuery { Id = id };

            // Skickar queryn via MediatR och väntar på resultatet
            var result = await _mediator.Send(query);

            // Om inget hittades, returnera 404
            if (result == null)
                return NotFound();

            // Annars returnera resultatet med status 200 OK
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryCommand command)
        {
            // Kontrollera att ID:t i URL:en matchar kommandots ID
            if (id != command.Id) //säkerställer att användaren inte skickar felaktig data
                return BadRequest("ID i URL matchar inte med ID i body.");

            //skicka kommando till mediatR
            var result = await _mediator.Send(command);

            //om uppdateringen lyckades
            if (result.Success)
            return Ok("Kategorin har uppdaterats.");

            //om något gick fel skicka felmeddelande
            return BadRequest(result.ErrorMessage);
            
        }

        //  POST-metod som nås via /api/category
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            // Skickar kommandot till MediatR, som skickar det vidare till rätt handler
            var result = await _mediator.Send(command);

            // Om resultatet lyckades, returnera status 201 Created + ID:t
            if (result.Success)
                return CreatedAtAction(nameof(GetById), new { id = result.Data }, result.Data);

            // Om något gick fel, skicka tillbaka 400 Bad Request med felmeddelande
            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand { Id = id });

            if (result.Success)
                return Ok("Kategorin har tagits bort.");

            return BadRequest(result.ErrorMessage);
        }

    }
}

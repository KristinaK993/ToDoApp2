using MediatR;
using ToDoApp.Application.Helpers;

namespace ToDoApp.Application.Categories.Commands
{
    //Detta kommando skickas via MediatR från controllern
    public class CreateCategoryCommand : IRequest<OperationResult<int>>
    {
        public string Name { get; set; } 
    }
}

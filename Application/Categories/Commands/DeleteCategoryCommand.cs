using MediatR;
using ToDoApp.Application.Helpers;

namespace ToDoApp.Application.Categories.Commands
{
    //Kommandoklass som bara innehåller IDt för kategorin som ska tas bort 
    public class DeleteCategoryCommand : IRequest<OperationResult<bool>>
    {
        public int Id { get; set; }
    }
}

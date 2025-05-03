using MediatR;
using ToDoApp.Application.Helpers;

namespace ToDoApp.Application.Categories.Commands.UpdateCategory
{
    //Command som skickas med ny info för att uppdatera en kategori
    public class UpdateCategoryCommand : IRequest<OperationResult<bool>>
    {
      public int Id { get; set; } //Id på kategorin som ska uppdateras
        public string Name { get; set; } //det nya namnet

    }
}

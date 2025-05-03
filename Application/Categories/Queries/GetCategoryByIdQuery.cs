using MediatR;
using ToDoApp.Application.Categories.Dto;

namespace ToDoApp.Application.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }

        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }

        public GetCategoryByIdQuery() { }
    }
}

//"En används för att sätta värdet direkt, och den parameterlösa krävs för objektinitialisering som MediatR använder ibland."

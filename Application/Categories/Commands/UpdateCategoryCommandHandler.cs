using MediatR;
using ToDoApp.Application.Helpers;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Application.Categories.Commands.UpdateCategory;

namespace Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, OperationResult<bool>>
    {
        private readonly IRepository<Category> _categoryRepository;

        public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            //hämta befintlig kategoru från databasen
            var category = await _categoryRepository.GetByIdAsync(request.Id);

            if (category == null)
            return OperationResult<bool>.Fail("Kategorin hittades inte.");

            //uppdatera fält
            category.Name = request.Name;

            //spara ändringar
            await _categoryRepository.UpdateAsync(category);
            return OperationResult<bool>.Ok(true);
        }
    }
}

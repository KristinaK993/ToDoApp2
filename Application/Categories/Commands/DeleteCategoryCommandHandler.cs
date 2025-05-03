using MediatR;
using ToDoApp.Application.Helpers;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Categories.Commands
{
    // Handler som hanterar DeleteCategoryCommand och tar bort en kategori
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, OperationResult<bool>>
    {
        private readonly IRepository<Category> _categoryRepository;

        public DeleteCategoryCommandHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            //hämta kategori från databas
            var category = await _categoryRepository.GetByIdAsync(request.Id);

            //kontrollera om den finns
            if (category == null)
                return OperationResult<bool>.Fail("Kategorin hittades inte");

            //ta bort kategorin
            await _categoryRepository.DeleteAsync(category);
            return OperationResult<bool>.Ok(true);
        }
    }
}

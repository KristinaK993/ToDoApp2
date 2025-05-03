using ToDoApp.Domain.Entities;                         
using ToDoApp.Domain.Interfaces;                       
using AutoMapper;                                      
using MediatR;                                         
using ToDoApp.Application.Categories.Commands;        
using ToDoApp.Application.Helpers;                     

namespace Application.Categories.Commands
{
    // Denna klass hanterar en CreateCategoryCommand som skickas via MediatR
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, OperationResult<int>>
    {
        private readonly IRepository<Category> _categoryRepository;//repository för att spara kategorier i databas
        private readonly IMapper _mapper;// AutoMapper-instans som används för att mappa DTO till entitet

        // Konstruktor där repository och mapper kommer in via dependency injection
        public CreateCategoryCommandHandler(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository; //sparar i repo
            _mapper = mapper;//sparar mapper
        }

        // Hanterar logiken när ett CreateCategoryCommand tas emot
        public async Task<OperationResult<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            //Mappa komandot till en riktig Category entitet (konverterar)
            var category = _mapper.Map<Category>(request);

            //lägg till i databasen
            await _categoryRepository.AddAsync(category);

            //returnera resultatet med nya IDt
            return OperationResult<int>.Ok(category.Id);
        }
    }
}

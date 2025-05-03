using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Application.Categories.Dto;
using ToDoApp.Application.Tasks.DTOs;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Categories.Queries
{
    //Handler som hanterar query GACQ och ret en lista av CategoryDto
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly IRepository<Category> _categoryRepository; //repo för att hämta alla kategorier från databasen
        private readonly IMapper _mapper; //automapper instans för att mappa category till categoryDto 

        // Konstruktor – tar in repository och mapper via dependency injection
        public GetAllCategoriesQueryHandler(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // Handle-metoden körs automatiskt när denna query anropas via MediatR
        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(); //hämta alla kategorier från databasen
            return _mapper.Map<List<CategoryDto>>(categories); //mappa varje category till en categoryDto
        }
    }
}

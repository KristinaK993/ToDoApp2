using AutoMapper;
using MediatR;
using ToDoApp.Application.Categories.Dto;
using ToDoApp.Application.Categories.Queries;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ToDoApp.Application.Categories.Queries
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly IRepository<Category> _categoryRepository; //interface för databas access

        private readonly IMapper _mapper;//automapper-instans


        // Konstruktor – dependency injection av repository och mapper
        public GetCategoryByIdQueryHandler(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        //hanterar själva logiken
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            //hämta kategori från databas baserat på ID
            var category = await _categoryRepository.GetByIdAsync(request.Id);

            //om den inte finns returnera null
            if (category == null)
                return null;

            //mappa från category  till categoryDto och returnera
            return _mapper.Map<CategoryDto>(category);
        }

    }
}

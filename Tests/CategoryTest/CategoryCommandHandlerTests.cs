//using NUnit.Framework;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using AutoMapper;
//using ToDoApp.Infrastructure.Data;
//using ToDoApp.Domain.Entities;
//using ToDoApp.Domain.Interfaces;
//using ToDoApp.Application.Categories.Commands;
//using ToDoApp.Application.Categories.Mapping;
//using Application.Categories.Commands;

//namespace ToDoApp.Tests.CategoryTest
//{
//    [TestFixture]
//    public class CreateCategoryCommandHandlerTests
//    {
//        private ToDoDbContext _context;
//        private Repository<Category> _repository;
//        private IMapper _mapper;

//        [SetUp]
//        public void Setup()
//        {
//            //Skapa in-memory databas
//            var options = new DbContextOptionsBuilder<ToDoDbContext>()
//                .UseInMemoryDatabase("TestDb_" + System.Guid.NewGuid())
//                .Options;

//            _context = new ToDoDbContext(options);
//            _repository = new Repository<Category>(_context);

//            //Konfigurera AutoMapper med rätt profil
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new CategoryProfile());
//            });
//            _mapper = config.CreateMapper();

//        }

//        [TearDown]
//        public void Cleanup()
//        {
//            _context.Dispose();
//        }

//        [Test]
//        public async Task Should_Create_Category_Successfully()
//        {
//            // Arrange
//            var handler = new CreateCategoryCommandHandler(_repository, _mapper);
//            var command = new CreateCategoryCommand { Name = "Testkategori" };

//            // Act
//            var result = await handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.IsTrue(result.Success);
//            Assert.That(result.Data, Is.GreaterThan(0));

//            var created = await _context.Categories.FindAsync(result.Data);
//            Assert.IsNotNull(created);
//            Assert.AreEqual("Testkategori", created.Name);
//        }
//    }
//}

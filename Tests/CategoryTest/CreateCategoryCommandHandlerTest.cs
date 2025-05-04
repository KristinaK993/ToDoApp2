using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ToDoApp.Application.Categories.Commands;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using AutoMapper;
using Moq;
using Application.Categories.Commands;

namespace ToDoApp.Tests.CategoryTest
{
    public class CreateCategoryCommandHandlerTest
    {
        [TestFixture]
        public class CreateCategoryCommandHandlerTests
        {
            private Mock<IRepository<Category>> _mockRepo;
            private Mock<IMapper> _mockMapper;
            private CreateCategoryCommandHandler _handler;

            [SetUp]
            public void SetUp()
            {
                // Skapar mockade versioner av repository och mapper
                _mockRepo = new Mock<IRepository<Category>>();
                _mockMapper = new Mock<IMapper>();

                // Injecta mockarna i handlern
                _handler = new CreateCategoryCommandHandler(_mockRepo.Object, _mockMapper.Object);
            }

            [Test]
            public async Task Handle_Should_Create_Category_Successfully()
            {
                // Arrange – skapa kommando och fake-mappad entitet
                var command = new CreateCategoryCommand { Name = "Testkategori" };
                var mappedCategory = new Category { Id = 1, Name = "Testkategori" };

                _mockMapper.Setup(m => m.Map<Category>(command)).Returns(mappedCategory);
                _mockRepo.Setup(r => r.AddAsync(mappedCategory)).Returns(Task.CompletedTask);

                // Act – kör handlern
                var result = await _handler.Handle(command, CancellationToken.None);

                // Assert – verifiera resultatet
                Assert.IsTrue(result.Success);
                Assert.AreEqual(1, result.Data);
                _mockRepo.Verify(r => r.AddAsync(mappedCategory), Times.Once);
            }
        }
    }
}

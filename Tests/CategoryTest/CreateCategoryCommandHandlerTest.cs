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
            // Mock-objekt som ersätter riktiga implementationer i testet
            private Mock<IRepository<Category>> _mockRepo;
            private Mock<IMapper> _mockMapper;
            private CreateCategoryCommandHandler _handler;

            [SetUp]  // Körs före varje testmetod – förbereder gemensamma resurser
            public void SetUp()
            {
                // Skapar mockade(fejkade) versioner av repository och mapper
                _mockRepo = new Mock<IRepository<Category>>();
                _mockMapper = new Mock<IMapper>();

                //skapar och injectar mockarna i handlern
                _handler = new CreateCategoryCommandHandler(_mockRepo.Object, _mockMapper.Object);
            }

            [Test] //testmetod
            public async Task Handle_Should_Create_Category_Successfully()
            {
                // Arrange – förbered testdata och konfigurera mocks
                var command = new CreateCategoryCommand { Name = "Testkategori" }; //kommando som skickas in
                var mappedCategory = new Category { Id = 1, Name = "Testkategori" }; //det objekt som mappern ska returnera

                // När handlern anropar _mapper.Map<.>, returnera det som bestämts
                _mockMapper.Setup(m => m.Map<Category>(command)).Returns(mappedCategory);

                // När handlern anropar AddAsync, gör "ingenting" (det ska bara gå igenom)
                _mockRepo.Setup(r => r.AddAsync(mappedCategory)).Returns(Task.CompletedTask);

                // Act – kör handlern
                var result = await _handler.Handle(command, CancellationToken.None);

                // Assert – kontrollera att det blev rätt
                Assert.IsTrue(result.Success); //resultatet ska vara lyckat
                Assert.AreEqual(1, result.Data); //IDt som returneras ska vara 1 

                // Kontrollera att AddAsync anropades exakt en gång
                _mockRepo.Verify(r => r.AddAsync(mappedCategory), Times.Once);
            }
        }
    }
}

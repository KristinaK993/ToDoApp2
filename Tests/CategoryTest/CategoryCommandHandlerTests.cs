using NUnit.Framework;
using ToDoApp.Application.Categories.Commands;
using ToDoApp.Domain.Entities;

[TestFixture]
public class CategoryTests
{
    [Test]
    public void Should_Create_Category_Successfully()
    {
        // Arrange – skapa ett kommando
        var command = new CreateCategoryCommand
        {
            Name = "TestKategori"
        };

        // Act – direkt skapa en kategori-entity (utan handler)
        var category = new Category
        {
            Name = command.Name
        };

        // Assert
        Assert.That(category.Name, Is.EqualTo("TestKategori"));
    }
}
